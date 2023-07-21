using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.DuplicateRoutine;

public class DuplicateRoutineCommand: IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
}
