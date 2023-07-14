using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.User;

namespace YourBuddyPull.Application.UseCases.Queries.Users.GetSingleUser;

public class GetSingleUserHandler : IRequestHandler<GetSingleUserQuery, UserInformationDTO>
{
    private readonly IUserRepository _userRepository;
    public GetSingleUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserInformationDTO> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserPropertiesByGuid(request.userId);
    }
}
