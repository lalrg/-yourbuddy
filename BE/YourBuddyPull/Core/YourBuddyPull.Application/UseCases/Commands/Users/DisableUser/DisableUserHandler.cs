using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.UseCases.Commands.Users.DisableUser;

public class DisableUserHandler : IRequestHandler<DisableUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DisableUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;

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

        _unitOfWork.OpenTransaction();
        domainUser.SetAsDeleted();
        await _userRepository.DeactivateUser(domainUser);
        await _unitOfWork.CommitTransaction();

        return true;
    }
}
