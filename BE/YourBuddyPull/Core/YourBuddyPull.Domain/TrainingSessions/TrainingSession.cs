using YourBuddyPull.Domain.Shared;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.TrainingSessions;

public sealed class TrainingSession : BaseEntity
{
    private TrainingSession(Guid id, SessionCreatedBy createdBy, DateTime startTime, DateTime? endTime)
    {
        Id = id;
        CreatedBy = createdBy;
        StartTime = startTime;
        EndTime = endTime;
    }

    public static TrainingSession Create(SessionCreatedBy createdBy, DateTime startTime, DateTime? endTime)
    {
        if (startTime > endTime && endTime is not null)
            throw new DomainValidationException("End time cannot happen before start time");

        return new TrainingSession(Guid.NewGuid(), createdBy, startTime, endTime);
    }

    public void UpdateProperties(DateTime startTime, DateTime? endTime)
    {
        if (startTime > endTime && endTime is not null)
            throw new DomainValidationException("End time cannot happen before start time");

        StartTime = startTime;
        EndTime = endTime;
    }
    public void AddExercise(ExecutedExercise executedExercise)
    {
        _executedExercise.Add(executedExercise);
    }
    public void RemoveExercise(Guid exerciseId)
    {
        var exercise = _executedExercise.Find(e=> e.ExerciseId == exerciseId);
        if (exercise == null)
            throw new DomainValidationException("The exercise that you are trying to remove is not present in this session");
        
        _executedExercise.Remove(exercise);
    }
    public SessionCreatedBy CreatedBy { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public IReadOnlyCollection<ExecutedExercise> ExecutedExercise { get => _executedExercise; }
    private List<ExecutedExercise> _executedExercise { get; set; } = new();
}
