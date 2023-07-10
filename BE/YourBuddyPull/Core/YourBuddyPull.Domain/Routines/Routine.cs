using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Shared.BaseClasses;
using YourBuddyPull.Domain.Shared.Exceptions;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Domain.Routines;

public sealed class Routine: BaseEntity
{
    private Routine(Guid id, string name, CreatedBy createdBy, DateTime createdDate)
    {
        Id = id;
        Name = name;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
    }

    public static Routine Create(string name, CreatedBy createdBy)
    {
        return new Routine(
            Guid.NewGuid(),
            name,
            createdBy,
            DateTime.Now
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
    }

    public string Name { get; private set; }
    public CreatedBy CreatedBy { get; private set; }
    public Guid AssignedTo { get; private set; }
    
    private List<PlannedExercise> _plannedExercises = new();

    public IReadOnlyList<PlannedExercise> PlannedExercises { get => _plannedExercises; }
    public DateTime CreatedDate { get; private set; }
}
