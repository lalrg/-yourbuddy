using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Exercises.AddExercise;

public class AddExerciseCommand: IRequest<bool>
{
    public string ExerciseName { get; set; }
    public string ExerciseType { get; set; }
    public string ExerciseDescription { get; set; }
    public string ImageUrl { get; set; }
    public string VideoUrl { get; set; }
}
