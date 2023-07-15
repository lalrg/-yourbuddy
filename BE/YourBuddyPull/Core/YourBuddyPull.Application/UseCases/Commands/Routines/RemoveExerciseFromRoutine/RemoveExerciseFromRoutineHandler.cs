using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.UseCases.Commands.Routines.AddExerciseToRoutine;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.RemoveExerciseFromRoutine;

public class RemoveExerciseFromRoutineHandler : IRequestHandler<RemoveExerciseFromRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveExerciseFromRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork, IExerciseRepository exerciseRepository)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
    }
    public async Task<bool> Handle(RemoveExerciseFromRoutineCommand request, CancellationToken cancellationToken)
    {
        var persistanceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
        var domainRoutine = Routine.Instanciate(persistanceRoutine.Id, persistanceRoutine.CreatedByName, CreatedBy.Instanciate(persistanceRoutine.CreatedBy, persistanceRoutine.CreatedByName), persistanceRoutine.isEnabled);

        var persistanceExercise = await _exerciseRepository.GetExerciseInformationById(request.ExerciseId);
        var plannedExercise = domainRoutine.PlannedExercises.Single(e => e.ExerciseId == request.ExerciseId);

        var domainExercise = PlannedExercise.Instanciate(
            plannedExercise.ExerciseId,
            plannedExercise.ExerciseName,
            plannedExercise.Reps,
            plannedExercise.Sets,
            plannedExercise.Load,
            plannedExercise.ExerciseType);

        domainRoutine.RemoveExercise(domainExercise);

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.UpdateExercisesForRoutine(domainRoutine);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
