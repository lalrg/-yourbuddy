using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.UseCases.Commands.Users.UpdateProperties;

public class UpdatePropertiesHandler : IRequestHandler<UpdatePropertiesCommand, bool>
{
    private readonly IUserRepository _userRepository;
    public UpdatePropertiesHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(UpdatePropertiesCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserPropertiesByUsername(request.Email);
        var userRoles = user.Roles.Select(Role.Instanciate).ToList();

        var domainUser = User.Instanciate(
            user.Id,
            user.Name,
            user.LastName,
            user.Email,
            user.IsDeleted,
            userRoles);

        domainUser.UpdateProperties(user.Name, user.LastName, user.Email);

        return await _userRepository.UpdateUserProperties(domainUser);
    }
}
