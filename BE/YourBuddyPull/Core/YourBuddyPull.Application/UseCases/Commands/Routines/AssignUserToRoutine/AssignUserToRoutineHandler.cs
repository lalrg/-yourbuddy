using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.AssignUserToRoutine;

public class UnassignRoutineHandler : IRequestHandler<UnassignRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UnassignRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<bool> Handle(UnassignRoutineCommand request, CancellationToken cancellationToken)
    {
        var persistenceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
        var domainRoutine = Routine.Instanciate(
            persistenceRoutine.Id,
            persistenceRoutine.Name,
            CreatedBy.Instanciate(persistenceRoutine.CreatedBy, persistenceRoutine.CreatedByName), persistenceRoutine.isEnabled);

        domainRoutine.AssignToUser(request.UserId);

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.AssignToUser(domainRoutine);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
