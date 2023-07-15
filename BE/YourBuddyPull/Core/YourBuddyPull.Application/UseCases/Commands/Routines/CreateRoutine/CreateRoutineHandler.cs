using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.CreateRoutine;

public class CreateRoutineHandler : IRequestHandler<CreateRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<bool> Handle(CreateRoutineCommand request, CancellationToken cancellationToken)
    {
        var createdByValue = CreatedBy.Instanciate(request.CreatedById, request.createdByName);
        var domainRoutine = Routine.Create(request.Name, createdByValue);

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.Create(domainRoutine);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
