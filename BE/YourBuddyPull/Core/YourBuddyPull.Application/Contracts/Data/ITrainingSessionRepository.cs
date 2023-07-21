using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.TrainingSession;
using YourBuddyPull.Domain.TrainingSessions;

namespace YourBuddyPull.Application.Contracts.Data;

public interface ITrainingSessionRepository
{
    Task<bool> Create(TrainingSession trainingSession);
    Task<bool> Update(TrainingSession trainingSession);
    Task<bool> UpdateExercises(TrainingSession trainingSession);

    Task<TrainingSessionDetailDTO> GetById(Guid Id);
    Task<List<TrainingSessionDTO>> GetAllForUser(Guid UserId);
    Task<PaginationResultDTO<TrainingSessionDTO>> GetAllPagedForUser(PaginationDTO pagination, Guid UserId);

}
