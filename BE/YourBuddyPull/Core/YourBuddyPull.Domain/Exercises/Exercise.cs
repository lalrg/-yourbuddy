using YourBuddyPull.Domain.Shared.Exceptions;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Domain.Exercises;

public sealed class Exercise
{
    private Exercise(Guid exerciseId, string exerciseName, int reps, int sets, int load, string imageUrl, string videoUrl, ExerciseType exerciseType, string exerciseDescription)
    {
        ExerciseId = exerciseId;
        Reps = reps;
        Sets = sets;
        Load = load;
        ExerciseType = exerciseType;
        ExerciseName = exerciseName;
        ExerciseDescription = exerciseDescription;
        VideoURL = videoUrl;
        ImageUrl = imageUrl;
    }
    public static Exercise Create(string exerciseName, int reps, int sets, int load, ExerciseType exerciseType, string exerciseDescription, string imageUrl, string videoUrl)
    {
        return new Exercise(Guid.NewGuid(), exerciseName, reps, sets, load, imageUrl, videoUrl, exerciseType, exerciseDescription);
    }
    public static Exercise Instanciate(Guid exerciseId, string exerciseName, int reps, int sets, int load, ExerciseType exerciseType, string exerciseDescription, string imageUrl, string videoUrl)
    {
        return new Exercise(exerciseId, exerciseName, reps, sets, load, imageUrl, videoUrl, exerciseType, exerciseDescription);
    }
    public Guid ExerciseId
    {
        get => _exerciseId; set
        {
            if (value == Guid.Empty)
                throw new DomainValidationException("The id of the exercise cannot be null cannot be null");
            _exerciseId = value;
        }
    }
    private Guid _exerciseId { get; set; }
    public string ExerciseName
    {
        get => _exerciseName; set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the name of the exercise cannot be null");
        }
    }
    private string _exerciseName { get; set; } = string.Empty;
    public string ExerciseDescription
    {
        get => _exerciseDescription; set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the description of the exercise cannot be null");
        }
    }
    private string _exerciseDescription { get; set; } = string.Empty;
    public string VideoURL
    {
        get => _videoURL; set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the videoURL of the exercise cannot be null");
        }
    }
    private string _videoURL { get; set; } = string.Empty;
    public string ImageUrl
    {
        get => _imageUrl; set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the description of the exercise cannot be null");
        }
    }
    private string _imageUrl { get; set; } = string.Empty;
    public int Reps { get; set; }
    public int Sets
    {
        get => _sets; set
        {
            if (value < 1)
                throw new DomainValidationException("The sets must be more than 0");
            _sets = value;
        }
    }
    private int _sets { get; set; } = 0;
    public int Load
    {
        get => _load; set
        {
            if (value < 1)
                throw new DomainValidationException("The value must be more than 0");
            _load = value;
        }
    }
    private int _load { get; set; }
    public string WorkDescription
    {
        get
        {
            switch (ExerciseType.TypeOfExercise)
            {
                case TypeOfExercise.MeasuredByTime:
                    return $"{Sets} series of {Load} {ExerciseType.MeasurementUnit}";

                case TypeOfExercise.MeasuredByWeight:
                    return $"{Sets} series of {Reps} reps with {Load} {ExerciseType.MeasurementUnit}";

                default:
                    return string.Empty; // this case will never happen
            }
        }
    }
    public ExerciseType ExerciseType { get; private set; }
}
