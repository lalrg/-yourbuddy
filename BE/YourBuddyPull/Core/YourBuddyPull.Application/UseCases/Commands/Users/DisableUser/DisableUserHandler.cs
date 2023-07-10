using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.UseCases.Commands.Users.DisableUser;

public class DisableUserHandler : IRequestHandler<DisableUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    public DisableUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(DisableUserCommand request, CancellationToken cancellationToken)
    {
        var userFromStorage = await _userRepository.GetUserPropertiesByGuid(request.userId);
        var roles = userFromStorage.Roles.Select(Role.Instanciate).ToList();

        if(userFromStorage.IsDeleted)
            return false;

        var domainUser = User.Instanciate(
            request.userId, 
            userFromStorage.Name, 
            userFromStorage.LastName, 
            userFromStorage.Email, 
            userFromStorage.IsDeleted,
            roles);

        domainUser.SetAsDeleted();
        return true;
    }
}
