using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.DeleteRoutine;

public class DeleteRoutineCommand: IRequest<bool>
{
    public Guid Id { get; set; }
}
