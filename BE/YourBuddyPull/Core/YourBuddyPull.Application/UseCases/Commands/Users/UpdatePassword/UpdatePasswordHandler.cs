using MediatR;
using System.Text;
using XSystem.Security.Cryptography;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.EmailSender;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Application.UseCases.Commands.Users.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, bool>
{
    private readonly IAuthenticationProvider _authProvider;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;
    public UpdatePasswordHandler(
        IAuthenticationProvider authenticationProvider, 
        IAuthenticationRepository authenticationRepository, 
        IUserRepository userRepository,
        IEmailSender emailSender,
        IUnitOfWork unitOfWork)
    {
        _authProvider = authenticationProvider;
        _authenticationRepository = authenticationRepository;
        _userRepository = userRepository;
        _emailSender = emailSender;
        _unitOfWork = unitOfWork;

    }
    public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserPropertiesByGuid(request.UserId);
        var (hash, salt) = await _authenticationRepository.GetHashAndSalt(user.Id);

        var current = GenerateHash(request.OldPassword, salt);
        
        var (_, newSalt) = _authProvider.GenerateNewRandomPasswordAndSalt();

        var newHash = GenerateHash(request.NewPassword, newSalt);

        var result = current == hash;
        if (result)
        {
            result = await _authenticationRepository.UpdatePassword(request.UserId, newHash, newSalt);
        }
        if (result)
        {
            await _unitOfWork.CommitTransaction();
            await _emailSender.SendResetPasswordEmail(user.Email, request.NewPassword);
        }

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
