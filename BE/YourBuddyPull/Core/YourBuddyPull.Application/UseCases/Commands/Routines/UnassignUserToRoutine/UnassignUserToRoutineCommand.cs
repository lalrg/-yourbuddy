using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.UnassignUserToRoutine;

public class UnassignUserToRoutineCommand: IRequest<bool>
{
    public Guid RoutineId { get; set; }
}
