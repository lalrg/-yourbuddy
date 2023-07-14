using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.User;

namespace YourBuddyPull.Application.UseCases.Queries.Users.GetUsersList;

public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, PaginationResultDTO<UserInformationDTO>>
{
    private readonly IUserRepository _userRepository;
    public GetUsersListHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<PaginationResultDTO<UserInformationDTO>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
        var paginationInfo = new PaginationDTO()
        {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
        };

        return await _userRepository.GetAllUsersPaged(paginationInfo);
    }
}
