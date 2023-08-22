using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.DuplicateRoutine;

public class DuplicateRoutineHadler : IRequestHandler<DuplicateRoutineCommand, Guid>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DuplicateRoutineHadler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;

    }
    public async Task<Guid> Handle(DuplicateRoutineCommand request, CancellationToken cancellationToken)
    {
        var existingRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.Id);
        var createdBy = await _userRepository.GetUserPropertiesByGuid(request.CreatedBy);
        var existingRoutineDomain = Routine.Instanciate(
            existingRoutine.Id, 
            existingRoutine.Name, 
            CreatedBy.Instanciate(existingRoutine.CreatedBy, existingRoutine.CreatedByName),
            existingRoutine.isEnabled);

        var newRoutine = Routine.Create(
            $"{existingRoutine.Name}_copy", 
            CreatedBy.Instanciate(createdBy.Id, $"{createdBy.Name} {createdBy.LastName}"));
        
        _unitOfWork.OpenTransaction();
        
        await _routineRepository.Create(newRoutine);
        await _unitOfWork.CommitTransaction();

        if (existingRoutineDomain.AssignedTo != Guid.Empty) {
            var assignedTo = await _userRepository.GetUserPropertiesByGuid(existingRoutineDomain.AssignedTo);
            newRoutine.AssignToUser(assignedTo.Id);
            await _routineRepository.AssignToUser(newRoutine);
        }

        await _routineRepository.CopyExercisesFrom(existingRoutineDomain, newRoutine);

        await _unitOfWork.CommitTransaction();

        return newRoutine.Id;
    }
}
