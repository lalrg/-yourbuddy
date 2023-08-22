using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.AssignUserToRoutine;

public class AssignRoutineHandler : IRequestHandler<AssignUserToRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AssignRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<bool> Handle(AssignUserToRoutineCommand request, CancellationToken cancellationToken)
    {
        var persistenceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
        var domainRoutine = Routine.Instanciate(
            persistenceRoutine.Id,
            persistenceRoutine.Name,
            CreatedBy.Instanciate(persistenceRoutine.CreatedBy, persistenceRoutine.CreatedByName), 
            persistenceRoutine.isEnabled,
            persistenceRoutine.Exercises.Select(e => PlannedExercise.Instanciate(
                    e.ExerciseId,
                    e.Name,
                    e.Reps,
                    e.Sets,
                    e.Load,
                    new ExerciseType(MapExerciseType(e.Type))
            )).ToList());

        domainRoutine.AssignToUser(request.UserId);

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.AssignToUser(domainRoutine);
        await _unitOfWork.CommitTransaction();

        return result;
    }

    private TypeOfExercise MapExerciseType(string exerciseType)
    {
        switch (exerciseType.ToLower())
        {
            case "time":
                return TypeOfExercise.MeasuredByTime;
            case "weight":
                return TypeOfExercise.MeasuredByWeight;
            default:
                return TypeOfExercise.MeasuredByWeight;
        }
    }
}
