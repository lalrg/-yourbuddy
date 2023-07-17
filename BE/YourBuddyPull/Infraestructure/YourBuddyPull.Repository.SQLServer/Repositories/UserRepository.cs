using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.User;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Repository.SQLServer.Repositories;

public class UserRepository : IUserRepository
{
    public Task<bool> CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeactivateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserInformationDTO>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<PaginationResultDTO<UserInformationDTO>> GetAllUsersPaged(PaginationDTO pagination)
    {
        throw new NotImplementedException();
    }

    public Task<UserInformationDTO> GetUserPropertiesByGuid(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserInformationDTO> GetUserPropertiesByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateRoles(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserProperties(User user)
    {
        throw new NotImplementedException();
    }
}
