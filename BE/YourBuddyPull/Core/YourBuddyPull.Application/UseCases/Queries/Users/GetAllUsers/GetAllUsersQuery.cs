using MediatR;
using YourBuddyPull.Application.DTOs.User;

namespace YourBuddyPull.Application.UseCases.Queries.Users.GetAllUsers;

public class GetAllUsersQuery: IRequest<List<UserInformationDTO>>
{
}
