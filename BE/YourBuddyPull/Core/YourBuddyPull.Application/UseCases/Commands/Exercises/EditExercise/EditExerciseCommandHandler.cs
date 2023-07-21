using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Exercises.EditExercise;

public class EditExerciseCommandHandler : IRequestHandler<EditExerciseCommand, bool>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditExerciseCommandHandler(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork)
    {
        _exerciseRepository = exerciseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(EditExerciseCommand request, CancellationToken cancellationToken)
    {
        var persistenceExercise = await _exerciseRepository.GetExerciseInformationById(request.Id);
        var domainExercise = Exercise.Instanciate(
            persistenceExercise.ExerciseId,
            persistenceExercise.Name,
            new ExerciseType(MapTypeOfExercise(persistenceExercise.Type)),
            persistenceExercise.Description,
            persistenceExercise.ImageUrl,
            persistenceExercise.VideoUrl);

        domainExercise.UpdateProperties(request.Name, request.Description, request.VideoUrl, request.ImageUrl);
        _unitOfWork.OpenTransaction();
        var result = await _exerciseRepository.Update(domainExercise); 
        await _unitOfWork.CommitTransaction();
        
        return result;
    }

    private TypeOfExercise MapTypeOfExercise(string type)
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
