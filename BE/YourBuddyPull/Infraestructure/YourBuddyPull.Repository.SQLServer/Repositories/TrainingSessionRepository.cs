using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.TrainingSession;
using YourBuddyPull.Domain.Shared.ValueObjects;
using YourBuddyPull.Domain.TrainingSessions;
using YourBuddyPull.Repository.SQLServer.DatabaseModels;

namespace YourBuddyPull.Repository.SQLServer.Repositories;

public class TrainingSessionRepository : ITrainingSessionRepository
{
    private readonly ProyectoLuisRContext _context;
    public TrainingSessionRepository(ProyectoLuisRContext context)
    {
        _context = context;
    }
    public Task<bool> Create(TrainingSession trainingSession)
    {
        Session persistanceTrainingSession = new()
        {
            CreatedBy = trainingSession.CreatedBy.CreatedById,
            EndTime = trainingSession.EndTime,
            StartTime = trainingSession.StartTime,
            Id = trainingSession.Id,
        };

        _context.Sessions.Add(persistanceTrainingSession);
        return Task.FromResult(true);
    }

    public async Task<List<TrainingSessionDTO>> GetAllForUser(Guid UserId)
    {
        // not used for now
        throw new NotImplementedException();
    }

    public Task<PaginationResultDTO<TrainingSessionDTO>> GetAllPagedForUser(PaginationDTO pagination, Guid UserId)
    {
        throw new NotImplementedException();
    }

    public Task<TrainingSessionDetailDTO> GetById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(TrainingSession trainingSession)
    {
        throw new NotImplementedException();
    }

    private TrainingSessionDTO MapToDTO(Session persistanceSession)
    {
        return new()
        {
            CreatedById = (Guid)persistanceSession.CreatedBy,
            CreatedByName = persistanceSession.CreatedByNavigation.Name,
            EndTime = (DateTime)persistanceSession.EndTime,
            StartTime = (DateTime)persistanceSession.StartTime,
            Id = persistanceSession.Id,
        };
    }

    private TrainingSessionDetailDTO MapToDetailDTO(Session persistanceSession)
    {
        var MappedExercises = persistanceSession.SessionExercises.Select(
            x=> new ExerciseTrainingSessionInformationDTO() {
                Description = x.Exercise.Description,
                ExerciseId = x.Exercise.Id,
                ImageUrl = x.Exercise.ImageUrl,
                Load = (int)x.Load,
                Name = x.Exercise.Name,
                Reps = (int)x.Reps,
                Sets = (int)x.Sets,
                SetsDescription = GetExerciseDescription(x),
                VideoUrl = x.Exercise.VideoUrl
            });

        return new()
        {
            CreatedBy = (Guid)persistanceSession.CreatedBy,
            CreatedByName = persistanceSession.CreatedByNavigation.Name,
            EndTime = (DateTime)persistanceSession.EndTime,
            StartTime = (DateTime)persistanceSession.StartTime,
            Id = persistanceSession.Id,
            Exercises = MappedExercises.ToList()
        };
    }

    private string GetExerciseDescription(SessionExercise exercise)
    {
        switch (exercise.Exercise.Type.Name.ToLower())
        {
            case "time":
                return $"{exercise.Sets} series of {exercise.Load} {exercise.Exercise.Type.Measurement}";

            case "weight":
                return $"{exercise.Sets} series of {exercise.Reps} reps with {exercise.Load} {exercise.Exercise.Type.Measurement.Name}";

            default:
                return string.Empty; // this case will never happen
        }
    }
}
