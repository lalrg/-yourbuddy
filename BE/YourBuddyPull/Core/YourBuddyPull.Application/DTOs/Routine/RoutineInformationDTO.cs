namespace YourBuddyPull.Application.DTOs.Routine;

public struct RoutineInformationDTO
{
    public Guid Id;
    public Guid CreatedBy;
    public string CreatedByName;
    public string Name;
    public List<ExerciseRoutineInformationDTO> Execises;
}
