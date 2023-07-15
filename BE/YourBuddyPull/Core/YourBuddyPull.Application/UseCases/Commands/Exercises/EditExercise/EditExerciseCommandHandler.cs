﻿using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Exercises.EditExercise;

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
        var persistanceExercise = await _exerciseRepository.GetExerciseInformationById(request.Id);
        var domainExercise = Exercise.Instanciate(
            persistanceExercise.ExerciseId,
            persistanceExercise.Name,
            new ExerciseType(MapTypeOfExercise(persistanceExercise.Type)),
            persistanceExercise.Description,
            persistanceExercise.ImageUrl,
            persistanceExercise.VideoUrl);

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
