using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.UpdateTrainingSession;

public class UpdateTrainingSessionCommand: IRequest<bool>
{
    public Guid SessionId;
    public DateTime startTime;
    public DateTime? endTime;
}
