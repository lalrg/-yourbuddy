using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Shared.BaseClasses;
using YourBuddyPull.Domain.Shared.Exceptions;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Domain.Routines;

public sealed class Routine: BaseEntity
{
    private Routine(Guid id, string name, CreatedBy createdBy, DateTime createdDate, bool isEnabled)
    {
        Id = id;
        Name = name;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
        IsEnabled = isEnabled;
    }

    public static Routine Create(string name, CreatedBy createdBy)
    {
        return new Routine(
            Guid.NewGuid(),
            name,
            createdBy,
            DateTime.Now,
            true
            );
    }

    public static Routine Instanciate(Guid Id ,string name, CreatedBy createdBy, bool isEnabled)
    {
        return new Routine(
            Id,
            name,
            createdBy,
            DateTime.Now,
            isEnabled
            );
    }

    public void AssignToUser(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new DomainValidationException("The userId cannot be empty");

        AssignedTo = userId;
    }

    public void AddExercise(PlannedExercise exercise)
    {
        if (_plannedExercises.Any(x => x == exercise))
            throw new DomainValidationException("This exercise has already been added to this routine");
        
        _plannedExercises.Add(exercise);
    }

    public void RemoveExercise(PlannedExercise exercise)
    {
        if (!_plannedExercises.Any())
            throw new DomainValidationException("There are no exercises in this routine.");

        if (!_plannedExercises.Any(x=> x.ExerciseId == exercise.ExerciseId))
            throw new DomainValidationException("There are no exercises in this routine.");

        _plannedExercises.Remove(exercise);
    }

    public void Deactivate()
    {
        IsEnabled = false;
        AssignedTo = Guid.Empty;
    }

    public string Name { get; private set; }
    public bool IsEnabled { get; private set; }
    public CreatedBy CreatedBy { get; private set; }
    public Guid AssignedTo { get; private set; }
    
    private List<PlannedExercise> _plannedExercises = new();

    public IReadOnlyList<PlannedExercise> PlannedExercises { get => _plannedExercises; }
    public DateTime CreatedDate { get; private set; }
}
