using Microsoft.EntityFrameworkCore;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Routine;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Domain.Routines;
using YourBuddyPull.Repository.SQLServer.DatabaseModels;

namespace YourBuddyPull.Repository.SQLServer.Repositories;

public class RoutineRepository : IRoutineRepository
{
    private readonly ProyectoLuisRContext _context;
    public RoutineRepository(ProyectoLuisRContext context)
    {
        _context = context;
    }
    public async Task<bool> AssignToUser(Domain.Routines.Routine routine)
    {
        var persistenceRoutine = await _context.Routines.SingleAsync(x=>x.Id == routine.Id);

        persistenceRoutine.UserAssignedId = routine.AssignedTo;
        _context.Entry(persistenceRoutine).State = EntityState.Modified;

        return true;
    }

    public async Task<bool> CopyExercisesFrom(Domain.Routines.Routine oldRoutine, Domain.Routines.Routine newRoutine)
    {
        var persistenceFrom = await _context.Routines.Include(x => x.ExerciseRoutines).AsNoTracking().SingleAsync(x=> x.Id == oldRoutine.Id);
        var persistenceTo = await _context.Routines.SingleAsync(x=> x.Id == newRoutine.Id);

        persistenceTo.ExerciseRoutines = persistenceFrom.ExerciseRoutines.Select(x => 
            {
                x.RoutineId = persistenceTo.Id; 
                return x;
            }).ToList();

        _context.Entry(persistenceTo).State = EntityState.Modified;

        return true;
    }

    public Task<bool> Create(Domain.Routines.Routine routine)
    {
        DatabaseModels.Routine persistenceRoutine = new() {
            Id = routine.Id,
            Name = routine.Name,
            CreatedBy = routine.CreatedBy.CreatedById,
            IsEnabled = true
        };

        _context.Routines.Add(persistenceRoutine);

        return Task.FromResult(true);
    }

    public async Task<bool> DisableRoutine(Domain.Routines.Routine routine)
    {
        var persistenceRoutine = await _context.Routines.SingleAsync(x=> x.Id == routine.Id);

        persistenceRoutine.IsEnabled = false;
        _context.Entry(persistenceRoutine).State = EntityState.Modified;

        return true;
    }

    public Task<List<RoutineInformationDTO>> GetAllRoutines()
    {
        // not used for now
        throw new NotImplementedException();
    }

    public Task<List<RoutineInformationDTO>> GetAllRoutinesForUser(Guid userId)
    {
        // not used for now
        throw new NotImplementedException();
    }

    public async Task<PaginationResultDTO<RoutineInformationDTO>> GetAllRoutinesPaged(PaginationDTO pagination)
    {
        var itemsToSkip = (pagination.CurrentPage - 1) * pagination.PageSize;
        var items = await _context
            .Routines
            .Include(x => x.CreatedByNavigation)
            .Include(x => x.ExerciseRoutines)
            .AsNoTracking()
            .Skip(itemsToSkip)
            .Take(pagination.PageSize)
            .ToListAsync();
        var totalCount = _context.Routines.Count();

        var mappedItems = items.Select(x => new RoutineInformationDTO()
        {
            CreatedBy = (Guid)x.CreatedBy,
            CreatedByName = $"{x.CreatedByNavigation.Name} {x.CreatedByNavigation.LastName}",
            Execises = x.ExerciseRoutines.Select(MapExercises).ToList()
        }).ToList();

        return new PaginationResultDTO<RoutineInformationDTO>() { 
            PageSize = pagination.PageSize,
            CurrentPage = pagination.CurrentPage,
            Items = mappedItems,
            TotalCount = totalCount
        };

    }

    public async Task<PaginationResultDTO<RoutineInformationDTO>> GetAllRoutinesPagedForuser(PaginationDTO pagination, Guid userId)
    {
        var itemsToSkip = (pagination.CurrentPage - 1) * pagination.PageSize;
        var items = await _context.Routines
            .Include(x => x.CreatedByNavigation)
            .Where(x=> x.UserAssignedId == userId)
            .AsNoTracking()
            .Skip(itemsToSkip)
            .Take(pagination.PageSize)
            .ToListAsync();
        var totalCount = _context.Routines.Count();

        var mappedItems = items.Select(x => new RoutineInformationDTO()
        {
            CreatedBy = (Guid)x.CreatedBy,
            CreatedByName = $"{x.CreatedByNavigation.Name} {x.CreatedByNavigation.LastName}",
            Execises = x.ExerciseRoutines.Select(MapExercises).ToList()
        }).ToList();


        return new PaginationResultDTO<RoutineInformationDTO>()
        {
            PageSize = pagination.PageSize,
            CurrentPage = pagination.CurrentPage,
            Items = mappedItems,
            TotalCount = totalCount
        };

    }

    public async Task<RoutineInformationDTO> GetRoutinePropertiesByGuid(Guid routineId)
    {
        var persistenceRoutine = await _context
            .Routines
            .AsNoTracking()
            .Include(x=>x.CreatedByNavigation)
            .Include(x=>x.ExerciseRoutines)
            .SingleOrDefaultAsync(x=> x.Id == routineId);

        var createdByName = $"{persistenceRoutine.CreatedByNavigation.Name} {persistenceRoutine.CreatedByNavigation.LastName}";

        return new()
        {
            CreatedBy = (Guid)persistenceRoutine.CreatedBy,
            CreatedByName = createdByName,
            Name = persistenceRoutine.Name,
            Id = routineId,
            isEnabled = (bool)persistenceRoutine.IsEnabled,
            Execises = persistenceRoutine.ExerciseRoutines.Select(MapExercises).ToList()
        };
    }

    public async Task<bool> UpdateExercisesForRoutine(Domain.Routines.Routine routine)
    {
        var persistenceRoutine = await _context.Routines.Include(x=>x.ExerciseRoutines).SingleAsync(x => x.Id == routine.Id);

        var exercisesToRemove = persistenceRoutine.ExerciseRoutines
            .Where(x=> x.RoutineId == routine.Id && !routine.PlannedExercises.Any(y=>y.ExerciseId == x.ExerciseId)).ToList();

        var exercisesToAdd = routine.PlannedExercises.Where(x=> !persistenceRoutine.ExerciseRoutines.Any(y=> y.ExerciseId ==x.ExerciseId)).ToList();

        exercisesToRemove.ForEach(
            x => persistenceRoutine.ExerciseRoutines.Remove(x));

        exercisesToAdd.ForEach(
            x => persistenceRoutine.ExerciseRoutines.Add(new()
            {
                ExerciseId = x.ExerciseId,
                RoutineId = routine.Id
            })
            );

        _context.Entry(persistenceRoutine).State = EntityState.Modified;


        return true;
    }

    private ExerciseRoutineInformationDTO MapExercises (ExerciseRoutine exercise)
    {
        return new()
        {
            Description = exercise.Exercise.Description,
            ExerciseId = exercise.ExerciseId,
            ImageUrl = exercise.Exercise.ImageUrl,
            Load = (int)exercise.Load,
            Reps = (int)exercise.Reps,
            Name = exercise.Exercise.Name,
            Sets = (int)exercise.Sets,
            VideoUrl = exercise.Exercise.VideoUrl,
            Type = exercise.Exercise.Type.Name,
            MeasurementUnit = exercise.Exercise.Type.Measurement.Name
        };
    }
}
