using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.AddTrainingSession;

public class UpdateTrainingSessionCommand: IRequest<bool>
{
    public Guid CreatedBy;
    public string CreatedByName;
    public DateTime startTime;
    public DateTime? endTime;
}
