using Microsoft.EntityFrameworkCore;
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
        Session persistenceTrainingSession = new()
        {
            CreatedBy = trainingSession.CreatedBy.CreatedById,
            EndTime = trainingSession.EndTime,
            StartTime = trainingSession.StartTime,
            Id = trainingSession.Id,
            
        };
        _context.Sessions.Add(persistenceTrainingSession);
        return Task.FromResult(true);
    }

    public async Task<PaginationResultDTO<TrainingSessionDTO>> GetAllPagedForUser(PaginationDTO pagination, Guid UserId)
    {
        var itemsToSkip = (pagination.CurrentPage - 1) * pagination.PageSize;
        var baseQuery = _context.Sessions
            .Where(x => x.CreatedBy == UserId)
            .Include(x => x.SessionExercises)
            .ThenInclude(x => x.Exercise)
            .ThenInclude(x => x.Type)
            .ThenInclude(x => x.Measurement);

        var totalCount = baseQuery.Count();

        var items = await baseQuery
            .Skip(itemsToSkip)
            .Take(pagination.PageSize)
            .AsNoTracking()
            .Select(x=> new TrainingSessionDTO()
            {
                CreatedById = (Guid)x.CreatedBy,
                CreatedByName = x.CreatedByNavigation.Name,
                EndTime = (DateTime)x.EndTime,
                Id = x.Id,
                StartTime = (DateTime)x.StartTime,
                ExerciseTrainingSessions = MapSessionExercisesToDTO(x.SessionExercises.ToList())
            }).ToListAsync();

        return new PaginationResultDTO<TrainingSessionDTO>()
        {
            PageSize = pagination.PageSize,
            CurrentPage = pagination.CurrentPage,
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<TrainingSessionDetailDTO> GetById(Guid Id)
    {
        var persistenceSession = await _context.Sessions.SingleAsync(x => x.Id == Id);
        return new()
        {
            Id = persistenceSession.Id,
            CreatedBy = (Guid)persistenceSession.CreatedBy,
            CreatedByName = $"{persistenceSession.CreatedByNavigation.Name} {persistenceSession.CreatedByNavigation.LastName}",
            EndTime = (DateTime)persistenceSession.EndTime,
            StartTime = (DateTime)persistenceSession.StartTime,
            Exercises = MapSessionExercisesToDTO(persistenceSession.SessionExercises.ToList())
        };
    }

    public async Task<bool> Update(TrainingSession trainingSession)
    {
        var persistenceSession = await _context.Sessions.SingleAsync(x=> x.Id == trainingSession.Id);
        persistenceSession.StartTime = trainingSession.StartTime;
        persistenceSession.EndTime = trainingSession.EndTime;

        _context.Entry(persistenceSession).State = EntityState.Modified;
        return true;

    }

    public Task<bool> UpdateExercises(TrainingSession trainingSession)
    {
        // sessionexercises
        var mappedExercises = trainingSession.ExecutedExercise.Select(
            e => new SessionExercise()
            {
                ExerciseId = e.ExerciseId,
                Load = e.Load,
                Reps = e.Reps,
                SessionId = trainingSession.Id,
                Sets = e.Sets
            }
            ).ToList();

        Session persistenceTrainingSession = new()
        {
            CreatedBy = trainingSession.CreatedBy.CreatedById,
            EndTime = trainingSession.EndTime,
            StartTime = trainingSession.StartTime,
            Id = trainingSession.Id,
            SessionExercises = mappedExercises
        };

        _context.Entry(persistenceTrainingSession).State = EntityState.Modified;

        return Task.FromResult(true);
    }

    private static List<ExerciseTrainingSessionInformationDTO> MapSessionExercisesToDTO(List<SessionExercise> sessionExercises)
    {
        return sessionExercises.Select(
            x=> new ExerciseTrainingSessionInformationDTO()
            {
                Description = x.Exercise.Description,
                ExerciseId = x.Exercise.Id,
                Load = (int)x.Load,
                Name = x.Exercise.Name,
                Reps = (int)x.Reps,
                Sets = (int)x.Sets,
                SetsDescription = GetExerciseDescription(x),
            }).ToList();
    }

    private static TrainingSessionDetailDTO MapToDetailDTO(Session persistenceSession)
    {
        var MappedExercises = persistenceSession.SessionExercises.Select(
            x=> new ExerciseTrainingSessionInformationDTO() {
                Description = x.Exercise.Description,
                ExerciseId = x.Exercise.Id,
                Load = (int)x.Load,
                Name = x.Exercise.Name,
                Reps = (int)x.Reps,
                Sets = (int)x.Sets,
                SetsDescription = GetExerciseDescription(x),
            });

        return new()
        {
            CreatedBy = (Guid)persistenceSession.CreatedBy,
            CreatedByName = persistenceSession.CreatedByNavigation.Name,
            EndTime = (DateTime)persistenceSession.EndTime,
            StartTime = (DateTime)persistenceSession.StartTime,
            Id = persistenceSession.Id,
            Exercises = MappedExercises.ToList()
        };
    }

    private static string GetExerciseDescription(SessionExercise exercise)
    {
        switch (exercise.Exercise.Type.Name.ToLower())
        {
            case "time":
                return $"{exercise.Sets} series de {exercise.Load} {exercise.Exercise.Type.Measurement}";

            case "weight":
                return $"{exercise.Sets} series de {exercise.Reps} reps con {exercise.Load} {exercise.Exercise.Type.Measurement.Name}";

            default:
                return string.Empty; // this case will never happen
        }
    }
}
