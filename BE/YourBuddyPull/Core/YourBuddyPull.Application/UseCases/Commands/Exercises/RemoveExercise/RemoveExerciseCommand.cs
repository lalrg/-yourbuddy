using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Exercises.RemoveExercise;

public class RemoveExerciseCommand: IRequest<bool>
{
    public Guid Id;
}
