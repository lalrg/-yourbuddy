﻿using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Shared.ValueObjects;
using YourBuddyPull.Domain.TrainingSessions;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.UpdateTrainingSession;

public class UpdateTrainingSessionHandler : IRequestHandler<UpdateTrainingSessionCommand, bool>
{
    private readonly ITrainingSessionRepository _trainingSessionRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateTrainingSessionHandler(ITrainingSessionRepository trainingSessionRepository, IUnitOfWork unitOfWork, IExerciseRepository exerciseRepository)
    {
        _trainingSessionRepository = trainingSessionRepository;
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;

    }
    public async Task<bool> Handle(UpdateTrainingSessionCommand request, CancellationToken cancellationToken)
    {
        var persistenceTrainingSession = await _trainingSessionRepository.GetById(request.SessionId);

        var domainCreatedBy = CreatedBy.Instanciate(persistenceTrainingSession.CreatedBy, persistenceTrainingSession.CreatedByName);
        var domainTrainingSession = TrainingSession.Instanciate(request.SessionId, domainCreatedBy, persistenceTrainingSession.StartTime, persistenceTrainingSession.EndTime, new());

        domainTrainingSession.UpdateProperties(request.startTime, request.endTime);

        _unitOfWork.OpenTransaction();
        var result = await _trainingSessionRepository.Update(domainTrainingSession);
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
