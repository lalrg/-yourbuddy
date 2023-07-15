namespace YourBuddyPull.Application.DTOs.Routine;

public struct ExerciseRoutineInformationDTO
{
    public Guid ExerciseId;
    public string Name;
    public string Description;
    public string ImageUrl;
    public string VideoUrl;
    public string SetsDescription;
    public int Load;
    public int Sets;
    public int Reps;
}
