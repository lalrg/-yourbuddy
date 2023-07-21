using MediatR;
using YourBuddyPull.Application.DTOs.Routine;
using YourBuddyPull.Application.DTOs.Shared;

namespace YourBuddyPull.Application.UseCases.Queries.Routines.GetAllRoutines;

public class GetRoutinesListQuery: IRequest<PaginationResultDTO<RoutineInformationDTO>>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

}
