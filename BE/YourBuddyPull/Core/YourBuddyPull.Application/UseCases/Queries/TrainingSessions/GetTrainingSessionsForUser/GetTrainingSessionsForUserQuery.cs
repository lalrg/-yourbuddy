using MediatR;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.TrainingSession;

namespace YourBuddyPull.Application.UseCases.Queries.TrainingSessions.GetTrainingSessionsForUser;

public class GetTrainingSessionsForUserQuery: IRequest<PaginationResultDTO<TrainingSessionDTO>>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public Guid UserId { get; set; }
}
