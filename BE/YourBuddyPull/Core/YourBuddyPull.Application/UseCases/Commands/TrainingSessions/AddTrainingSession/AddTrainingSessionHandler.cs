using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Shared.ValueObjects;
using YourBuddyPull.Domain.TrainingSessions;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.AddTrainingSession;

public class AddTrainingSessionHandler : IRequestHandler<AddTrainingSessionCommand, bool>
{
    private readonly ITrainingSessionRepository _trainingSessionRepository;
    private readonly IRoutineRepository _routineRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddTrainingSessionHandler(ITrainingSessionRepository trainingSessionRepository, IUnitOfWork unitOfWork, IRoutineRepository routineRepository)
    {
        _trainingSessionRepository = trainingSessionRepository;
        _unitOfWork = unitOfWork;
        _routineRepository = routineRepository;

    }
    public async Task<bool> Handle(AddTrainingSessionCommand request, CancellationToken cancellationToken)
    {
        var domainCreatedBy = CreatedBy.Instanciate(request.CreatedBy, request.CreatedByName);
        var domainTrainingSession = TrainingSession.Create(domainCreatedBy, request.startTime, request.endTime);

        _unitOfWork.OpenTransaction();
        var result = await _trainingSessionRepository.Create(domainTrainingSession);
        var routineFrom = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineFrom);

        routineFrom.Execises.ForEach(
            e=>
            {
                domainTrainingSession.AddExercise(
                    ExecutedExercise.Instanciate(
                        e.Name, 1, 1, 1, new ExerciseType(MapExerciseType(e.Type))
                    )
                );
            });

        result = result && await _trainingSessionRepository.UpdateExercises(domainTrainingSession);
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
