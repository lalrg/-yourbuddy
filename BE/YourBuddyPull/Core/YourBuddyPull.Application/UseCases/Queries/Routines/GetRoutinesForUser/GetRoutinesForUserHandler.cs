using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Routine;
using YourBuddyPull.Application.DTOs.Shared;

namespace YourBuddyPull.Application.UseCases.Queries.Routines.GetRoutinesForUser;

public class GetRoutinesForUserHandler : IRequestHandler<GetRoutinesForUserQuery, PaginationResultDTO<RoutineInformationDTO>>
{
    private readonly IRoutineRepository _routineRepository;

    public GetRoutinesForUserHandler(IRoutineRepository routineRepository)
    {
        _routineRepository = routineRepository;
    }
    public async Task<PaginationResultDTO<RoutineInformationDTO>> Handle(GetRoutinesForUserQuery request, CancellationToken cancellationToken)
    {
        var paginationData = new PaginationDTO()
        {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize
        };

        return await _routineRepository.GetAllRoutinesPagedForuser(paginationData, request.userId);
    }
}
