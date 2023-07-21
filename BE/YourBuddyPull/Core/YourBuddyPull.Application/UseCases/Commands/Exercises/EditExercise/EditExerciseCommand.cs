using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Exercises.EditExercise;

public class EditExerciseCommand: IRequest<bool>
{
    public Guid Id;
    public string Name;
    public string Description;
    public string ImageUrl;
    public string VideoUrl;
    public string Type;
}
