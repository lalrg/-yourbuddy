using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.AddExerciseToRoutine;

public class AddExerciseToRoutineCommand: IRequest<bool>
{
    public Guid ExerciseId { get; set; }
    public Guid RoutineId { get; set; }
}
