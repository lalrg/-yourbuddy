using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Shared.ValueObjects;
using YourBuddyPull.Domain.TrainingSessions;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.RemoveExerciseFromTrainingSession;

public class RemoveExerciseFromTrainingSessionHandler : IRequestHandler<RemoveExerciseFromTrainingSessionCommand, bool>
{
    private readonly ITrainingSessionRepository _trainingSessionRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveExerciseFromTrainingSessionHandler(
        ITrainingSessionRepository trainingSessionRepository,
        IExerciseRepository exerciseRepository,
        IUnitOfWork unitOfWork)
    {
        _trainingSessionRepository = trainingSessionRepository;
        _exerciseRepository = exerciseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(RemoveExerciseFromTrainingSessionCommand request, CancellationToken cancellationToken)
    {
        var persistenceSession = await _trainingSessionRepository.GetById(request.sessionId);
        var domainExecutedExercises = persistenceSession
            .Exercises
            .Select(
                e => ExecutedExercise.Instanciate(
                    e.ExerciseId,
                    e.Name,
                    e.Reps,
                    e.Sets,
                    e.Load,
                    new ExerciseType(TypeOfExercise.MeasuredByTime)
                )
            ).ToList();

        var domainSession = TrainingSession.Instanciate(
            persistenceSession.Id,
            CreatedBy.Instanciate(persistenceSession.CreatedBy, persistenceSession.CreatedByName),
            persistenceSession.StartTime,
            persistenceSession.EndTime,
            domainExecutedExercises);

        var persistenceExercise = await _exerciseRepository.GetExerciseInformationById(request.exerciseId);
        domainSession.RemoveExercise(request.exerciseId);

        var result = await _trainingSessionRepository.UpdateExercises(domainSession);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
