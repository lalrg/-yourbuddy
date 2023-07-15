using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Routine;

namespace YourBuddyPull.Application.UseCases.Queries.Routines.GetSingleRoutine;

public class GetSingleRoutineHandler : IRequestHandler<GetSingleRoutineQuery, RoutineInformationDTO>
{
    private readonly IRoutineRepository _routineRepository;
    public GetSingleRoutineHandler(IRoutineRepository routineRepository)
    {
        _routineRepository = routineRepository;    
    }
    public async Task<RoutineInformationDTO> Handle(GetSingleRoutineQuery request, CancellationToken cancellationToken)
    {
        return await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
    }
}
