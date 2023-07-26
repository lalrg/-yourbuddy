using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YourBuddyPull.API.ViewModels.Common;
using YourBuddyPull.API.ViewModels.User;
using YourBuddyPull.Application.UseCases.Commands.Users.DisableUser;
using YourBuddyPull.Application.UseCases.Commands.Users.RegisterUser;
using YourBuddyPull.Application.UseCases.Commands.Users.UpdateProperties;
using YourBuddyPull.Application.UseCases.Queries.Users.GetSingleUser;
using YourBuddyPull.Application.UseCases.Queries.Users.GetUsersList;

namespace YourBuddyPull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(
                    new GetSingleUserQuery()
                    {
                        userId = id
                    }
                );

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get([FromQuery]PaginationInfo pagination)
        {
            if (pagination.CurrentPage < 1)
                pagination.CurrentPage = 1;
            if (pagination.PageSize < 5)
                pagination.PageSize = 5;

            var result = await _mediator.Send(
                    new GetUsersListQuery()
                    {
                        CurrentPage = pagination.CurrentPage,
                        PageSize = pagination.PageSize
                    }
                );

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post(CreateUserVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new RegisterCommand()
                    {
                        Email = vm.Email,
                        LastName = vm.LastName,
                        Name = vm.Name,
                        Roles = vm.Roles
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(
                    new DisableUserCommand()
                    {
                        userId = id
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");
            
            return Ok("Usuario deshabilitado correctamente");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(UpdateUserVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new UpdatePropertiesCommand()
                    {
                        Email = vm.Email,
                        LastName = vm.LastName,
                        Name = vm.Name,
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("Usuario actualizado correctamente");
        }

    }
}
