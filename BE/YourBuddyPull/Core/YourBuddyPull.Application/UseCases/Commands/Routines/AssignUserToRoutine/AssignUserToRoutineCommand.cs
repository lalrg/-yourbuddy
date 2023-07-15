using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.AssignUserToRoutine;

public class UnassignRoutineCommand: IRequest<bool>
{
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
}
