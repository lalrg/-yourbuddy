using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.CreateRoutine;

public class CreateRoutineCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public Guid CreatedById { get; set; }
    public string createdByName { get; set; }
}
