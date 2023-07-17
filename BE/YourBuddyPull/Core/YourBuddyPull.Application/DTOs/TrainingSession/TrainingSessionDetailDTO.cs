namespace YourBuddyPull.Application.DTOs.TrainingSession
{
    public struct TrainingSessionDetailDTO
    {
        public Guid Id;
        public DateTime StartTime;
        public DateTime EndTime;
        public Guid CreatedBy;
        public string CreatedByName;
        public List<ExerciseTrainingSessionInformationDTO> Exercises;
    }
}
