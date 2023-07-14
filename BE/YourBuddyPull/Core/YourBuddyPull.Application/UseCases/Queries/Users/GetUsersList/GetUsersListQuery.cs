using MediatR;
using YourBuddyPull.Application.DTOs.Shared;
using YourBuddyPull.Application.DTOs.User;

namespace YourBuddyPull.Application.UseCases.Queries.Users.GetUsersList;

public class GetUsersListQuery: IRequest<PaginationResultDTO<UserInformationDTO>>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}
