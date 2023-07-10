namespace YourBuddyPull.Application.DTOs.TrainingSession
{
    public struct TrainingSessionDetailDTO
    {
        public Guid Id;
        public DateTime StartTime;
        public DateTime EndTime;
        public List<ExerciseTrainingSessionInformationDTO> Exercises;
    }
}
