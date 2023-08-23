using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.RemoveExerciseFromTrainingSession;

public class RemoveExerciseFromTrainingSessionCommand: IRequest<bool>
{
    public Guid exerciseId { get; set; }
    public Guid sessionId { get; set; }
}
