using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.ChangeNameToRoutine;

public class ChangeNameToRoutineHandler : IRequestHandler<ChangeNameToRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ChangeNameToRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(ChangeNameToRoutineCommand request, CancellationToken cancellationToken)
    {
        var persistenceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.Id);

        if(persistenceRoutine.Id == Guid.Empty)
        {
            return false;
        }

        var domainRoutine = Routine.Instanciate(
            persistenceRoutine.Id,
            persistenceRoutine.Name,
            CreatedBy.Instanciate(persistenceRoutine.CreatedBy, persistenceRoutine.CreatedByName),
            persistenceRoutine.isEnabled);

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.UpdateNameForRoutine(domainRoutine, request.Name);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
