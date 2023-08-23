namespace YourBuddyPull.API.ViewModels.TrainingSession;

public class AddExerciseVM
{
    public Guid exerciseId { get; set; }
    public Guid sessionId { get; set; }
    public int reps { get; set; }
    public int sets { get; set; }
    public int load { get; set; }
}
