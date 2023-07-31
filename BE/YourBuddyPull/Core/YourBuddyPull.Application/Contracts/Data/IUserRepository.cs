using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.User;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.Contracts.Data;

public interface IUserRepository
{
    Task<bool> CreateUser(User user);
    Task<bool> UpdateRoles(User user);
    Task<bool> UpdateUserProperties(User user);
    Task<bool> DeactivateUser(User user);
    Task<UserInformationDTO> GetUserPropertiesByGuid(Guid userId);
    Task<UserInformationDTO> GetUserPropertiesByUsername(string username);
    Task<UserInformationDTO?> TryGetUserPropertiesByUsername(string username);
    Task<List<UserInformationDTO>> GetAllUsers();
    Task<PaginationResultDTO<UserInformationDTO>> GetAllUsersPaged(PaginationDTO pagination);
}
