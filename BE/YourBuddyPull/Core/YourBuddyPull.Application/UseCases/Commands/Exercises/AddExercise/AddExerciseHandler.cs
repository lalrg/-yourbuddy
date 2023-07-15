using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Exercises.AddExercise;

public class AddExerciseHandler : IRequestHandler<AddExerciseCommand, bool>
{
    private readonly IExerciseRepository _execiseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddExerciseHandler(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork)
    {
        _execiseRepository = exerciseRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<bool> Handle(AddExerciseCommand request, CancellationToken cancellationToken)
    {
        var domainExercise = Exercise.Create(
            request.ExerciseName,
            new ExerciseType(MapExerciseType(request.ExerciseType)),
            request.ExerciseDescription,
            request.ImageUrl,
            request.VideoUrl
            );

        _unitOfWork.OpenTransaction();
        var result = await _execiseRepository.Create(domainExercise);
        await _unitOfWork.CommitTransaction();
        
        return result;
    }

    private TypeOfExercise MapExerciseType(string exerciseType)
    {
        switch (exerciseType.ToLower())
        {
            case "time":
                return TypeOfExercise.MeasuredByTime;
            case "weight":
                return TypeOfExercise.MeasuredByWeight;
            default:
                return TypeOfExercise.MeasuredByWeight;
        }
    }
}
