using MediatR;
using YourBuddyPull.Application.DTOs.TrainingSession;

namespace YourBuddyPull.Application.UseCases.Queries.TrainingSessions.GetSingleTrainingSession;

public class GetSingleTrainingSessionQuery: IRequest<TrainingSessionDetailDTO>
{
    public Guid TrainingSessionId { get; set; }
}
