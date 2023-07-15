namespace YourBuddyPull.Application.DTOs.TrainingSession
{
    public struct TrainingSessionDTO
    {
        public Guid Id;
        public DateTime StartTime;
        public DateTime EndTime;
        public Guid CreatedById;
        public string CreatedByName;
    }
}
