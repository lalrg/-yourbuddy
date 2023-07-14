using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.RemoveExerciseFromRoutine;

public class RemoveExerciseFromRoutineCommand: IRequest<bool>
{
    public Guid ExerciseId { get; set; }
    public Guid RoutineId { get; set; }
}
