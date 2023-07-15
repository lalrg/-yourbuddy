using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Shared.ValueObjects;
using YourBuddyPull.Domain.TrainingSessions;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.UpdateTrainingSession;

public class UpdateTrainingSessionHandler : IRequestHandler<UpdateTrainingSessionCommand, bool>
{
    private readonly ITrainingSessionRepository _trainingSessionRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateTrainingSessionHandler(ITrainingSessionRepository trainingSessionRepository, IUnitOfWork unitOfWork)
    {
        _trainingSessionRepository = trainingSessionRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateTrainingSessionCommand request, CancellationToken cancellationToken)
    {
        var persistanceTrainingSession = await _trainingSessionRepository.GetById(request.SessionId);

        var domainCreatedBy = CreatedBy.Instanciate(persistanceTrainingSession.CreatedById, persistanceTrainingSession.CreatedByName);
        var domainTrainingSession = TrainingSession.Instanciate(request.SessionId, domainCreatedBy, persistanceTrainingSession.StartTime, persistanceTrainingSession.EndTime);

        domainTrainingSession.UpdateProperties(request.startTime, request.endTime);

        _unitOfWork.OpenTransaction();
        var result = await _trainingSessionRepository.Create(domainTrainingSession);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
