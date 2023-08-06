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
        var user = await _userRepository.GetUserPropertiesByGuid(request.Id);
        var userRoles = user.Roles.Select(Role.Instanciate).ToList();

        var domainUser = User.Instanciate(
            user.Id,
            user.Name,
            user.LastName,
            user.Email,
            user.IsDeleted,
            userRoles);

        _unitOfWork.OpenTransaction();
        domainUser.UpdateProperties(request.Name, request.LastName, request.Email);
        var existingRole = domainUser.Roles.First();

        if (existingRole.Name != request.Role) {
            domainUser.AddRole(Role.Instanciate(request.Role));
            domainUser.RemoveRole(existingRole);
        }
        var result = await _userRepository.UpdateUserProperties(domainUser);
        await _unitOfWork.CommitTransaction();

        return result;
    }
}
