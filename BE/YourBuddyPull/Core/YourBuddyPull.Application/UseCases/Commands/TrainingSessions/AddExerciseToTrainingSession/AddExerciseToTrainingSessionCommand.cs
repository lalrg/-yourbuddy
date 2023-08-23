using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.AddExerciseToTrainingSession;

public class AddExerciseToTrainingSessionCommand : IRequest<bool>
{
    public Guid exerciseId { get; set; }
    public Guid sessionId { get; set; }
    public int reps { get; set; }
    public int sets { get; set; }
    public int load { get; set; }
}
