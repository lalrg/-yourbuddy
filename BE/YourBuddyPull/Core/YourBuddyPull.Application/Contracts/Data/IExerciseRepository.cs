﻿using YourBuddyPull.Application.DTOs.Exercise;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Domain.Exercises;

namespace YourBuddyPull.Application.Contracts.Data;

public interface IExerciseRepository
{
    Task<bool> Create(Exercise exercise);
    Task<bool> Update(Exercise exercise);
    Task<bool> Delete(Exercise exercise);
    Task<ExerciseDTO> GetExerciseInformationById(Guid id);
    Task<List<ExerciseDTO>> GetAll();
    Task<PaginationResultDTO<ExerciseDTO>> GetAllPaged(PaginationDTO pagination);
}
