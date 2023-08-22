namespace YourBuddyPull.Application.DTOs.Routine;

public struct RoutineInformationDTO
{
    public Guid Id;
    public Guid CreatedBy;
    public Guid AssignedTo;
    public string AssignedToName;
    public string CreatedByName;
    public string Name;
    public bool isEnabled;
    public List<ExerciseRoutineInformationDTO> Exercises;
}
