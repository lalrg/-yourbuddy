using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.ChangeNameToRoutine;

public class ChangeNameToRoutineCommand: IRequest<bool>
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}
