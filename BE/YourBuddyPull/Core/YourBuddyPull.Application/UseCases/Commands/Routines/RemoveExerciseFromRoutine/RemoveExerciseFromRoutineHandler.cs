﻿using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.UseCases.Commands.Routines.AddExerciseToRoutine;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Application.UseCases.Commands.Routines.RemoveExerciseFromRoutine;

public class RemoveExerciseFromRoutineHandler : IRequestHandler<RemoveExerciseFromRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveExerciseFromRoutineHandler(IRoutineRepository routineRepository, IUnitOfWork unitOfWork, IExerciseRepository exerciseRepository)
    {
        _routineRepository = routineRepository;
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
    }
    public async Task<bool> Handle(RemoveExerciseFromRoutineCommand request, CancellationToken cancellationToken)
    {
        var persistenceRoutine = await _routineRepository.GetRoutinePropertiesByGuid(request.RoutineId);
        var domainRoutine = Routine.Instanciate(
            persistenceRoutine.Id, 
            persistenceRoutine.CreatedByName, 
            CreatedBy.Instanciate(persistenceRoutine.CreatedBy, 
            persistenceRoutine.CreatedByName), 
            persistenceRoutine.isEnabled,
            persistenceRoutine.Exercises.Select(e => PlannedExercise.Instanciate(
                    e.ExerciseId,
                    e.Name,
                    e.Reps,
                    e.Sets,
                    e.Load,
                    new ExerciseType(MapExerciseType(e.Type))
            )).ToList());

        var persistenceExercise = await _exerciseRepository.GetExerciseInformationById(request.ExerciseId);
        var plannedExercise = domainRoutine.PlannedExercises.Single(e => e.ExerciseId == request.ExerciseId);

        domainRoutine.RemoveExercise(plannedExercise);

        _unitOfWork.OpenTransaction();
        var result = await _routineRepository.UpdateExercisesForRoutine(domainRoutine);
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
