using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.EmailSender;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Application.UseCases.Commands.Users.ResetPassword;

public class ResetPasswordHandler: IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    public ResetPasswordHandler(
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

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserPropertiesByGuid(request.UserId);
        if (user.Id == Guid.Empty)
            return false;

        var (newPassword, newSalt) = _authenticationProvider.GenerateNewRandomPasswordAndSalt();

        _unitOfWork.OpenTransaction();
        var result = await _authenticationRepository.UpdatePassword(request.UserId, newPassword, newSalt);
        
        if (result)
        {
            await _unitOfWork.CommitTransaction();
            await _emailSender.SendResetPasswordEmail(user.Email, newPassword);
        }

        return result;
    }
}
