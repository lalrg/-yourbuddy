using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourBuddyPull.API.ViewModels.Authentication;
using YourBuddyPull.Application.UseCases.Commands.Users.LoginUser;
using YourBuddyPull.Application.UseCases.Commands.Users.ResetPassword;

namespace YourBuddyPull.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Datos invalidos");
        }

        var result = await _mediator.Send(new LoginUserCommand()
        {
            Email = vm.Email,
            Password = vm.Password
        });

        if (result == null)
            return Unauthorized("Credenciales invalidos");
        
        return Ok(new
        {
            Token= result
        });
    }

    [Authorize(Roles = "admin")]
    [HttpPost("resetpasssword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Datos invalidos");
        }

        var result = await _mediator.Send(new ResetPasswordCommand()
        {
            UserId = vm.UserId,
        });

        if(!result)
            return BadRequest("Ocurrio un error al resetear la contraseña");

        return Ok("Contraseña reseteada");
    }
}
