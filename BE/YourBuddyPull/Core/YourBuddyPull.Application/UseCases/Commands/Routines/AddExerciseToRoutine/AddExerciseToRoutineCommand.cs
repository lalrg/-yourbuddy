using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.AddExerciseToRoutine;

public class AddExerciseToRoutineCommand: IRequest<bool>
{
    public Guid ExerciseId { get; set; }
    public Guid RoutineId { get; set; }
    public int reps { get; set; }
    public int load { get; set; }
    public int sets { get; set; }
}
