using Microsoft.EntityFrameworkCore;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.User;
using YourBuddyPull.Domain.Users;
using YourBuddyPull.Repository.SQLServer.DatabaseModels;

namespace YourBuddyPull.Repository.SQLServer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProyectoLuisRContext _context;
    public UserRepository(ProyectoLuisRContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateUser(Domain.Users.User user)
    {
        var persistenceUser = new DatabaseModels.User()
        {
            Id = user.Id,
            Email = user.Email,
            IsDeleted = false,
            LastName = user.LastName,
            Name = user.Name,
            PasswordHash = null,
            PasswordSalt = null,
            Roles = await _context.Roles.Where(x=> user.Roles.Any(r => r.Name == x.Name)).ToListAsync()
        };

        _context.Users.Add(persistenceUser);

        return true;
    }

    public async Task<bool> DeactivateUser(Domain.Users.User user)
    {
        var persistenceUser = await _context.Users.SingleAsync(x => x.Id == user.Id);
        persistenceUser.IsDeleted = true;
        _context.Entry(persistenceUser).State = EntityState.Modified;

        return true;
    }

    public Task<List<UserInformationDTO>> GetAllUsers()
    {
        // not used for now
        throw new NotImplementedException();
    }

    public async Task<PaginationResultDTO<UserInformationDTO>> GetAllUsersPaged(PaginationDTO pagination)
    {
        var itemsToSkip = (pagination.CurrentPage - 1) * pagination.PageSize;
        var baseQuery = _context.Users.Where(u => !(u.IsDeleted ?? false)).Include(u=>u.Roles).AsNoTracking();

        var count = await baseQuery.CountAsync();

        var items = await baseQuery.Skip(itemsToSkip).Take(pagination.PageSize)
            .Select(x=>MapToUserInfoDTO(x)).ToListAsync();

        return new()
        {
            CurrentPage = pagination.CurrentPage,
            Items = items,
            PageSize = pagination.PageSize,
            TotalCount = count
        };
    }

    public async Task<UserInformationDTO> GetUserPropertiesByGuid(Guid userId)
    {
        return MapToUserInfoDTO( await _context.Users.Include(u=>u.Roles).SingleAsync(x=> x.Id == userId) );
    }

    public async Task<UserInformationDTO> GetUserPropertiesByUsername(string username)
    {
        return MapToUserInfoDTO(await _context.Users.Include(u => u.Roles).SingleAsync(x => x.Email == username));
    }

    public async Task<bool> UpdateRoles(Domain.Users.User user)
    {
        var persistanceUser = await _context.Users.Include(u => u.Roles).SingleAsync(x=> x.Id == user.Id);
        persistanceUser.Roles = await _context.Roles.Where(x => user.Roles.Any(r => r.Name == x.Name)).ToListAsync();

        _context.Entry(persistanceUser).State = EntityState.Modified;
        return true;
    }

    public async Task<bool> UpdateUserProperties(Domain.Users.User user)
    {
        var persistenceUser = await _context.Users.SingleAsync(x=>x.Id == user.Id);
        persistenceUser.Email = user.Email;
        persistenceUser.LastName = user.LastName;
        persistenceUser.Name = user.Name;
        
        _context.Entry(persistenceUser).State = EntityState.Modified;
        return true;

    }
    private static UserInformationDTO MapToUserInfoDTO(DatabaseModels.User user)
    {
        return new()
        {
            Email = user.Email,
            Id = user.Id,
            IsDeleted = (bool)user.IsDeleted,
            LastName = user.LastName,
            Name = user.Name,
            Roles = user.Roles.Select(x=>x.Name).ToList()
        };
    }

    public async Task<UserInformationDTO?> TryGetUserPropertiesByUsername(string username)
    {
        var user = await _context.Users.Include(u => u.Roles).SingleOrDefaultAsync(x => x.Email == username);

        if(user == null)
        {
            return null;
        }
        return MapToUserInfoDTO(user);
    }
}
