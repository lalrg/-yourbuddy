using YourBuddyPull.Application.DTOs.Routine;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.User;
using YourBuddyPull.Domain.Routines;

namespace YourBuddyPull.Application.Contracts.Data;

public interface IRoutineRepository
{
    Task<bool> AssignToUser(Routine routine);
    Task<bool> Create(Routine routine);
    Task<bool> CopyExercisesFrom(Routine oldRoutine, Routine newRoutine);
    Task<bool> UpdateExercisesForRoutine(Routine routine);
    Task<bool> DisableRoutine(Routine routine);
    Task<bool> UpdateNameForRoutine(Routine routine, string name);
    Task<RoutineInformationDTO> GetRoutinePropertiesByGuid(Guid routineId);
    Task<List<RoutineInformationDTO>> GetAllRoutines();
    Task<PaginationResultDTO<RoutineInformationDTO>> GetAllRoutinesPaged(PaginationDTO pagination);
    Task<List<RoutineInformationDTO>> GetAllRoutinesForUser(Guid userId);
    Task<PaginationResultDTO<RoutineInformationDTO>> GetAllRoutinesPagedForuser(PaginationDTO pagination, Guid userId);
}
