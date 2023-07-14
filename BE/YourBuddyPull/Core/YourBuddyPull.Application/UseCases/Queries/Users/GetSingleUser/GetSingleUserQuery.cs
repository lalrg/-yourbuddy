using MediatR;
using YourBuddyPull.Application.DTOs.User;

namespace YourBuddyPull.Application.UseCases.Queries.Users.GetSingleUser;

public class GetSingleUserQuery: IRequest<UserInformationDTO>
{
    public Guid userId { get; set; }
}
