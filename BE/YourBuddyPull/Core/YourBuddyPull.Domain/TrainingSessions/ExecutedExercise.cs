using YourBuddyPull.Domain.Shared;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.TrainingSessions;

public sealed class ExecutedExercise
{
    private ExecutedExercise(Guid exerciseId, string exerciseName, int reps, int sets, int load, ExerciseTypes exerciseType) {
        ExerciseId = exerciseId;
        Reps = reps;
        Sets = sets;
        Load = load;
        ExerciseType = exerciseType;
        ExerciseName = exerciseName;
    }
    public static ExecutedExercise Create(Guid exerciseId, string exerciseName, int reps, int sets, int load, ExerciseTypes exerciseType)
    {
        return new ExecutedExercise(exerciseId, exerciseName,reps, sets, load, exerciseType);
    }
    public Guid ExerciseId { get => _exerciseId; set { 
            if(value == Guid.Empty)
                throw new DomainValidationException("The id of the exercise cannot be null cannot be null");
            _exerciseId = value;
        }
    }
    private Guid _exerciseId { get; set; }
    public string ExerciseName { get => _exerciseName; set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the name of the exercise cannot be null");
        }
    }
    private string _exerciseName { get; set; } = string.Empty;
    public int Reps { get; set; }
    public int Sets { get => _sets; set
        {
            if (value < 1)
                throw new DomainValidationException("The sets must be more than 0");
            _sets = value;
        } 
    }
    private int _sets { get; set; } = 0;
    public int Load { get => _load; set {
            if (value < 1)
                throw new DomainValidationException("The value must be more than 0");
            _load = value;
        }
    }
    private int _load { get; set; }
    public ExerciseTypes ExerciseType { get; private set; }
}
