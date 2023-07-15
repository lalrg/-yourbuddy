using MediatR;
using YourBuddyPull.Application.DTOs.Routine;

namespace YourBuddyPull.Application.UseCases.Queries.Routines.GetSingleRoutine;

public class GetSingleRoutineQuery: IRequest<RoutineInformationDTO>
{
    public Guid RoutineId { get; set; }
}
