using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.DeactivateRoutine;

public class DeactivateRoutineHandler: IRequestHandler<DeactivateRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeactivateRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeactivateRoutineCommand request, CancellationToken cancellationToken)
    {
        var persistenceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
        var domainRoutine = Routine.Instanciate(
            persistenceRoutine.Id, 
            persistenceRoutine.Name,
            CreatedBy.Instanciate(
                persistenceRoutine.CreatedBy, 
                persistenceRoutine.CreatedByName),
            persistenceRoutine.isEnabled);

        domainRoutine.Deactivate();

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.DisableRoutine(domainRoutine);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
