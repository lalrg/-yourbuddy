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
        var persistenceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
        var domainRoutine = Routine.Instanciate(
            persistenceRoutine.Id, 
            persistenceRoutine.CreatedByName, 
            CreatedBy.Instanciate(persistenceRoutine.CreatedBy, persistenceRoutine.CreatedByName),
            persistenceRoutine.isEnabled);
        
        var persistenceExercise = await _exerciseRepository.GetExerciseInformationById(request.ExerciseId);

        var domainExercise = PlannedExercise.Create(
            persistenceExercise.Name, 
            request.reps, 
            request.sets, 
            request.load, 
            new ExerciseType(MapExerciseType(persistenceExercise.Type)));

        domainRoutine.AddExercise(domainExercise);
        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.UpdateExercisesForRoutine(domainRoutine);
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
