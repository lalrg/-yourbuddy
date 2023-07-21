using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Exercises.RemoveExercise;

public class RemoveExerciseCommandHandler : IRequestHandler<RemoveExerciseCommand, bool>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveExerciseCommandHandler(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork)
    {
        _exerciseRepository = exerciseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(RemoveExerciseCommand request, CancellationToken cancellationToken)
    {
        var persistenceExercise = await _exerciseRepository.GetExerciseInformationById(request.Id);
        var domainExercise = Exercise.Instanciate(
            persistenceExercise.ExerciseId,
            persistenceExercise.Name,
            new ExerciseType(MapTypeOfExercise(persistenceExercise.Type)),
            persistenceExercise.Description,
            persistenceExercise.ImageUrl,
            persistenceExercise.VideoUrl);

        _unitOfWork.OpenTransaction();
        var result = await _exerciseRepository.Delete(domainExercise); 
        await _unitOfWork.CommitTransaction();
        
        return result;
    }

    private static TypeOfExercise MapTypeOfExercise(string type)
    {
        switch (type.ToLower()) {
            case "time":
                return TypeOfExercise.MeasuredByTime;
            case "weight":
                return TypeOfExercise.MeasuredByWeight;
            default:
                return TypeOfExercise.MeasuredByWeight;
        }
    }
}
