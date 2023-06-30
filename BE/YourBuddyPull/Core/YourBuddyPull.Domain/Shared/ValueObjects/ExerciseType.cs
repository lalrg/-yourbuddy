using YourBuddyPull.Domain.Shared.BaseClasses;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.Shared.ValueObjects;

public enum TypeOfExercise
{
    MeasuredByTime,
    MeasuredByWeight
}

public sealed class ExerciseType : ValueObject
{
    public ExerciseType(TypeOfExercise typeOfExercise)
    {
        TypeOfExercise = typeOfExercise;
    }
    public TypeOfExercise TypeOfExercise { get; private set; }
    public string TypeOfExerciseName { 
        get { 
            switch (TypeOfExercise)
            {
                case TypeOfExercise.MeasuredByWeight:
                    return "Measured by weight";
                case TypeOfExercise.MeasuredByTime:
                    return "Measured by time";
                default:
                    return string.Empty; //this case will never happen
            }
        } 
    }
    public string MeasurementUnit
    {
        get
        {
            switch (TypeOfExercise)
            {
                case TypeOfExercise.MeasuredByWeight:
                    return "pounds";
                case TypeOfExercise.MeasuredByTime:
                    return "seconds";
                default:
                    return string.Empty; //this case will never happen
            }
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TypeOfExercise;
    }

}
