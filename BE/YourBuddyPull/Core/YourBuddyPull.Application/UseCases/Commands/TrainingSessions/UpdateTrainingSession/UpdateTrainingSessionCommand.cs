using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.TrainingSessions.UpdateTrainingSession;

public class UpdateTrainingSessionCommand: IRequest<bool>
{
    public Guid SessionId;
    public DateTime startTime;
    public DateTime? endTime;
    public List<UpdatedExercise> exercises;
}

public class UpdatedExercise
{
    public Guid exerciseId;
    public int reps;
    public int sets;
    public int load;
}
