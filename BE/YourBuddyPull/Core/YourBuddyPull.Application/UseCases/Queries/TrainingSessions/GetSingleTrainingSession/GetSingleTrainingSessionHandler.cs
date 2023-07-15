using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.TrainingSession;

namespace YourBuddyPull.Application.UseCases.Queries.TrainingSessions.GetSingleTrainingSession;

public class GetSingleTrainingSessionHandler : IRequestHandler<GetSingleTrainingSessionQuery, TrainingSessionDetailDTO>
{
    private readonly ITrainingSessionRepository _trainingSessionRepository;

    public GetSingleTrainingSessionHandler(ITrainingSessionRepository trainingSessionRepository)
    {
        _trainingSessionRepository = trainingSessionRepository;
    }
    public async Task<TrainingSessionDetailDTO> Handle(GetSingleTrainingSessionQuery request, CancellationToken cancellationToken)
    {
        return await _trainingSessionRepository.GetById(request.TrainingSessionId);
    }
}
