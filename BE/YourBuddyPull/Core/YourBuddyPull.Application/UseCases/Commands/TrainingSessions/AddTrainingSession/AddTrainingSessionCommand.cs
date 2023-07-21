using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.AddTrainingSession;

public class AddTrainingSessionCommand: IRequest<bool>
{
    public Guid CreatedBy;
    public Guid RoutineFrom;
    public string CreatedByName;
    public DateTime startTime;
    public DateTime? endTime;
}
