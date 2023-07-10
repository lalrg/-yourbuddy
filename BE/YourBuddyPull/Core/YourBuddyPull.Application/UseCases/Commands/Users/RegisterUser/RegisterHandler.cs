using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.UseCases.Commands.Users.RegisterUser;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserRepository _userRepository;
    public RegisterHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var roles = request.Roles.Select(Role.Instanciate).ToList();
        var user = User.Create(
            request.Name, 
            request.LastName, 
            request.Email, 
            roles);

        return _userRepository.CreateUser(user);
    }
}
