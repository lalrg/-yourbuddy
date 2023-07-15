using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.DeactivateRoutine;

public class DeactivateRoutineCommand: IRequest<bool>
{
    public Guid RoutineId { get; set; }
}
