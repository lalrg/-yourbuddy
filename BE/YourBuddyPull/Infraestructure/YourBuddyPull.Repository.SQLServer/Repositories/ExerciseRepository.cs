using Microsoft.EntityFrameworkCore;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Exercise;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Domain.Exercises;
using YourBuddyPull.Repository.SQLServer.DatabaseModels;

namespace YourBuddyPull.Repository.SQLServer.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly ProyectoLuisRContext _context;
    public ExerciseRepository(ProyectoLuisRContext context)
    {
        _context = context;
    }
    public async Task<bool> Create(Domain.Exercises.Exercise exercise)
    {
        var exerciseType = await _context.ExerciseTypes.SingleAsync(x=> x.Name.ToLower() == exercise.ExerciseType.TypeOfExerciseName.ToLower());
        var persistenceExercise = new DatabaseModels.Exercise()
        {
            Id = exercise.ExerciseId,
            Name = exercise.ExerciseName,
            Description = exercise.ExerciseDescription,
            ImageUrl = exercise.ImageUrl,
            TypeId = exerciseType.Id,
            VideoUrl = exercise.VideoURL
        };
        _context.Exercises.Add(persistenceExercise);

        return true;
    }

    public async Task<bool> Delete(Domain.Exercises.Exercise exercise)
    {
        var persistenceExercise = await _context.Exercises.AsNoTracking().SingleAsync(x=> x.Id == exercise.ExerciseId);
        _context.Exercises.Remove(persistenceExercise);
        
        return true;
    }

    public Task<List<ExerciseDTO>> GetAll()
    {
        // not used for now
        throw new NotImplementedException();
    }

    public async Task<PaginationResultDTO<ExerciseDTO>> GetAllPaged(PaginationDTO pagination)
    {
        var itemsToSkip = (pagination.CurrentPage - 1) * pagination.PageSize;
        var items = await _context.Exercises.Skip(itemsToSkip).Take(pagination.PageSize).Include(x=> x.Type).AsNoTracking().ToListAsync();
        var count = await _context.Exercises.CountAsync();

        var mappedItems = items.Select(x => new ExerciseDTO()
        {
            Description = x.Description,
            ExerciseId = x.Id,
            ImageUrl = x.ImageUrl,
            Name = x.Name,
            VideoUrl = x.VideoUrl,
            Type = x.Type.Name
        }).ToList();

        return new PaginationResultDTO<ExerciseDTO>()
        {
            CurrentPage = pagination.CurrentPage,
            PageSize = pagination.PageSize,
            Items = mappedItems,
            TotalCount = count
        };
    }

    public async Task<ExerciseDTO> GetExerciseInformationById(Guid id)
    {
        var persistenceExercise = await _context.Exercises.Include(x=>x.Type).AsNoTracking().SingleAsync(e=>e.Id == id);
        return new ExerciseDTO()
        {
            Description = persistenceExercise.Description,
            ExerciseId= persistenceExercise.Id,
            ImageUrl = persistenceExercise.ImageUrl,
            Name = persistenceExercise.Name,
            Type = persistenceExercise.Type.Name,
            VideoUrl = persistenceExercise.VideoUrl
        };
    }

    public async Task<bool> Update(Domain.Exercises.Exercise exercise)
    {
        var persistenceExercise = await _context.Exercises.SingleAsync(x=>x.Id == exercise.ExerciseId);
        
        persistenceExercise.VideoUrl = exercise.VideoURL;
        persistenceExercise.Description = exercise.ExerciseDescription;
        persistenceExercise.ImageUrl = exercise.ImageUrl;
        persistenceExercise.Name = exercise.ExerciseName;

        _context.Entry(persistenceExercise).State = EntityState.Modified;

        return true;
    }
}
