using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.DeleteRoutine;

public class DeleteRoutineHandler : IRequestHandler<DeleteRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<bool> Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        var routineFromData = await _routineRepository.GetRoutinePropertiesByGuid(request.Id);
        var routine = Routine.Instanciate(
            routineFromData.Id,
            routineFromData.Name,
            CreatedBy.Instanciate(routineFromData.CreatedBy, routineFromData.CreatedByName),
            routineFromData.isEnabled
            );

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.DisableRoutine(routine);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
