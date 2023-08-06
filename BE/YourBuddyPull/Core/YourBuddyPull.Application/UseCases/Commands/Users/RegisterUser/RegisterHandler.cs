using MediatR;
using System.Text;
using XSystem.Security.Cryptography;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.EmailSender;
using YourBuddyPull.Application.Contracts.Security;
using YourBuddyPull.Domain.Users;

namespace YourBuddyPull.Application.UseCases.Commands.Users.RegisterUser;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly IEmailSender _emailSender;
    public RegisterHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        IAuthenticationProvider authenticationProvider,
        IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authenticationProvider = authenticationProvider;
        _emailSender = emailSender;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {

        if (await _userRepository.UserExists(request.Email))
            return false;

        var roles = new List<Role>() { Role.Instanciate(request.Role) };
        var user = User.Create(
            request.Name, 
            request.LastName, 
            request.Email, 
            roles);

        var (password, salt) = _authenticationProvider.GenerateNewRandomPasswordAndSalt();

        _unitOfWork.OpenTransaction();
        var result = await _userRepository.CreateUser(
            user,
            GenerateHash(password, salt), 
            salt);

        await _unitOfWork.CommitTransaction();
        await _emailSender.SendAccountCreated(user.Email, password);

        return result;
    }

    private string GenerateHash(string input, string salt)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
        SHA256Managed sHA256ManagedString = new SHA256Managed();
        byte[] hash = sHA256ManagedString.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
