using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.AddExerciseToRoutine;

public class AddExerciseToRoutineHandler : IRequestHandler<AddExerciseToRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddExerciseToRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork, IExerciseRepository exerciseRepository)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
    }
    public async Task<bool> Handle(AddExerciseToRoutineCommand request, CancellationToken cancellationToken)
    {
        var persistanceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
        var domainRoutine = Routine.Instanciate(
            persistanceRoutine.Id, 
            persistanceRoutine.CreatedByName, 
            CreatedBy.Instanciate(persistanceRoutine.CreatedBy, persistanceRoutine.CreatedByName),
            persistanceRoutine.isEnabled);
        
        var persistanceExercise = await _exerciseRepository.GetExerciseInformationById(request.ExerciseId);

        var domainExercise = PlannedExercise.Create(persistanceExercise.ExerciseName, request.reps, request.sets, request.load, persistanceExercise.ExerciseType);

        domainRoutine.AddExercise(domainExercise);
        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.UpdateExercisesForRoutine(domainRoutine);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
