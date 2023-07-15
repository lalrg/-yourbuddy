using YourBuddyPull.Domain.Shared.Exceptions;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Domain.Exercises;

public sealed class Exercise
{
    private Exercise(Guid exerciseId, string exerciseName, string imageUrl, string videoUrl, ExerciseType exerciseType, string exerciseDescription)
    {
        ExerciseId = exerciseId;
        ExerciseType = exerciseType;
        ExerciseName = exerciseName;
        ExerciseDescription = exerciseDescription;
        VideoURL = videoUrl;
        ImageUrl = imageUrl;
    }
    public static Exercise Create(string exerciseName, ExerciseType exerciseType, string exerciseDescription, string imageUrl, string videoUrl)
    {
        return new Exercise(Guid.NewGuid(), exerciseName, imageUrl, videoUrl, exerciseType, exerciseDescription);
    }
    public static Exercise Instanciate(Guid exerciseId, string exerciseName, ExerciseType exerciseType, string exerciseDescription, string imageUrl, string videoUrl)
    {
        return new Exercise(exerciseId, exerciseName, imageUrl, videoUrl, exerciseType, exerciseDescription);
    }

    public void UpdateProperties(string exerciseName, string exerciseDescription, string videoURL, string imageUrl)
    {
        ExerciseName = exerciseName;
        ExerciseDescription = exerciseDescription;
        VideoURL = videoURL;
        ImageUrl = imageUrl;
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
    public ExerciseType ExerciseType { get; private set; }
}
