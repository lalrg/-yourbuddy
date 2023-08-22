﻿using YourBuddyPull.Domain.Shared.Exceptions;
using YourBuddyPull.Domain.Shared.ValueObjects;

namespace YourBuddyPull.Domain.Routines;

public sealed class PlannedExercise
{
    private PlannedExercise(Guid exerciseId, string exerciseName, int reps, int sets, int load, ExerciseType exerciseType)
    {
        ExerciseId = exerciseId;
        Reps = reps;
        Sets = sets;
        Load = load;
        ExerciseType = exerciseType;
        ExerciseName = exerciseName;
    }
    public static PlannedExercise Instanciate(Guid Id,string exerciseName, int reps, int sets, int load, ExerciseType exerciseType)
    {
        return new PlannedExercise(Id, exerciseName, reps, sets, load, exerciseType);
    }
    public Guid ExerciseId
    {
        get => _exerciseId; set
        {
            if (value == Guid.Empty)
                throw new DomainValidationException("The id of the exercise cannot be null cannot be null");
            _exerciseId = value;
        }
    }
    private Guid _exerciseId { get; set; }
    public string ExerciseName
    {
        get => _exerciseName; set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidationException("the name of the exercise cannot be null");
        }
    }
    private string _exerciseName { get; set; } = string.Empty;
    public int Reps { get; set; }
    public int Sets
    {
        get => _sets; set
        {
            if (value < 1)
                throw new DomainValidationException("The sets must be more than 0");
            _sets = value;
        }
    }
    private int _sets { get; set; } = 0;
    public int Load
    {
        get => _load; set
        {
            _load = value;
        }
    }
    private int _load { get; set; }
    public string WorkDescription
    {
        get
        {
            switch (ExerciseType.TypeOfExercise)
            {
                case TypeOfExercise.MeasuredByTime:
                    return $"{Sets} series de {Load} {ExerciseType.MeasurementUnit}";

                case TypeOfExercise.MeasuredByWeight:
                    return $"{Sets} series de {Reps} reps con {Load} {ExerciseType.MeasurementUnit}";

                default:
                    return string.Empty; // this case will never happen
            }
        }
    }
    public ExerciseType ExerciseType { get; private set; }
}
