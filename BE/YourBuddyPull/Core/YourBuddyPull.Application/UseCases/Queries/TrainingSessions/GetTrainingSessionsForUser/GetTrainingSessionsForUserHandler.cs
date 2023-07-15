using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.TrainingSession;

namespace YourBuddyPull.Application.UseCases.Queries.TrainingSessions.GetTrainingSessionsForUser;

public class GetTrainingSessionsForUserHandler : IRequestHandler<GetTrainingSessionsForUserQuery, PaginationResultDTO<TrainingSessionDTO>>
{
    private readonly ITrainingSessionRepository _trainingSessionRepository;
    public GetTrainingSessionsForUserHandler(ITrainingSessionRepository trainingSessionRepository)
    {
        _trainingSessionRepository = trainingSessionRepository;
    }
    public async Task<PaginationResultDTO<TrainingSessionDTO>> Handle(GetTrainingSessionsForUserQuery request, CancellationToken cancellationToken)
    {
        var paginationData = new PaginationDTO()
        {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
        };

        return await _trainingSessionRepository.GetAllPagedForUser(paginationData, request.UserId);
    }
}
