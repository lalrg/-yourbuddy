﻿using YourBuddyPull.Domain.Shared.BaseClasses;
using YourBuddyPull.Domain.Shared.Exceptions;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Domain.TrainingSessions;

public sealed class TrainingSession : BaseEntity
{
    private TrainingSession(Guid id, CreatedBy createdBy, DateTime startTime, DateTime? endTime, List<ExecutedExercise> exercises)
    {
        Id = id;
        CreatedBy = createdBy;
        StartTime = startTime;
        EndTime = endTime;
        _executedExercise = exercises;
    }

    public static TrainingSession Create(CreatedBy createdBy, DateTime startTime, DateTime? endTime, List<ExecutedExercise> exercises)
    {
        if (startTime > endTime && endTime is not null)
            throw new DomainValidationException("End time cannot happen before start time");

        return new TrainingSession(Guid.NewGuid(), createdBy, startTime, endTime, exercises);
    }

    public static TrainingSession Instanciate(Guid id, CreatedBy createdBy, DateTime startTime, DateTime? endTime, List<ExecutedExercise> exercises)
    {
        if (startTime > endTime && endTime is not null)
            throw new DomainValidationException("End time cannot happen before start time");

        return new TrainingSession(id, createdBy, startTime, endTime, exercises);
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
    public CreatedBy CreatedBy { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public IReadOnlyCollection<ExecutedExercise> ExecutedExercise { get => _executedExercise; }
    private List<ExecutedExercise> _executedExercise { get; set; } = new();
}
