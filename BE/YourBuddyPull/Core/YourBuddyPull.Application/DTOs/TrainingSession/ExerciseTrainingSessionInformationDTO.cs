namespace YourBuddyPull.Application.DTOs.TrainingSession;

public struct ExerciseTrainingSessionInformationDTO
{
    public Guid ExerciseId;
    public string Name;
    public string Description;
    public string SetsDescription;
    public int Load;
    public int Sets;
    public int Reps;
}
