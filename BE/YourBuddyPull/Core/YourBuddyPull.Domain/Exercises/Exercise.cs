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
        get => _exerciseName; private set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the name of the exercise cannot be null");
            _exerciseName = value;
        }
    }
    private string _exerciseName { get; set; }
    public string ExerciseDescription
    {
        get => _exerciseDescription; private set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the description of the exercise cannot be null");
            _exerciseDescription = value;
        }
    }
    private string _exerciseDescription { get; set; }
    public string VideoURL
    {
        get => _videoURL; private set
        {
            _videoURL = value;
        }
    }
    private string _videoURL { get; set; }
    public string ImageUrl
    {
        get => _imageUrl; private set
        {
            _imageUrl = value;
        }
    }
    private string _imageUrl { get; set; }
    public ExerciseType ExerciseType { get; private set; }
}
