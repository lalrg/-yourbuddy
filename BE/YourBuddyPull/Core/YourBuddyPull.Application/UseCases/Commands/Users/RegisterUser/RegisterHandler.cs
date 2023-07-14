using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.UseCases.Commands.Users.RegisterUser;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;

    }
    public Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var roles = request.Roles.Select(Role.Instanciate).ToList();
        var user = User.Create(
            request.Name, 
            request.LastName, 
            request.Email, 
            roles);

        _unitOfWork.OpenTransaction();
        var result = _userRepository.CreateUser(user);
        _unitOfWork.CommitTransaction();
        
        return result;
    }
}
