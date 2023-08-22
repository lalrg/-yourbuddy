using MediatR;
using System.Text;
using XSystem.Security.Cryptography;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.EmailSender;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Application.UseCases.Commands.Users.ResetPasswordByEmail;

public class ResetPasswordByEmailHandler: IRequestHandler<ResetPasswordByEmailCommand, bool>
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    public ResetPasswordByEmailHandler(
        IAuthenticationProvider authenticationProvider, 
        IUserRepository userRepository, 
        IAuthenticationRepository authenticationRepository,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender)
    {
        _authenticationProvider = authenticationProvider;
        _userRepository = userRepository;
        _authenticationRepository = authenticationRepository;
        _unitOfWork= unitOfWork;
        _emailSender= emailSender;
    }

    public async Task<bool> Handle(ResetPasswordByEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserPropertiesByUsername(request.UserEmail);
        if (user.Id == Guid.Empty)
            return false;

        var (newPassword, newSalt) = _authenticationProvider.GenerateNewRandomPasswordAndSalt();

        var newHash = GenerateHash(newPassword, newSalt);

        _unitOfWork.OpenTransaction();
        var result = await _authenticationRepository.UpdatePassword(user.Id, newHash, newSalt);
        
        if (result)
        {
            await _unitOfWork.CommitTransaction();
            await _emailSender.SendResetPasswordEmail(user.Email, newPassword);
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
