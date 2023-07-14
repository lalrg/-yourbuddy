using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.UseCases.Commands.Users.UpdateProperties;

public class UpdatePropertiesHandler : IRequestHandler<UpdatePropertiesCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdatePropertiesHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
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

        _unitOfWork.OpenTransaction();
        domainUser.UpdateProperties(user.Name, user.LastName, user.Email);
        _unitOfWork.CommitTransaction();

        return await _userRepository.UpdateUserProperties(domainUser);
    }
}
