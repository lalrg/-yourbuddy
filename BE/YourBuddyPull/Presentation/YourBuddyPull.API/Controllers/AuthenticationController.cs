using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourBuddyPull.API.ViewModels.Authentication;
using YourBuddyPull.Application.UseCases.Commands.Users.LoginUser;
using YourBuddyPull.Application.UseCases.Commands.Users.ResetPassword;
using YourBuddyPull.Application.UseCases.Commands.Users.ResetPasswordByEmail;
using YourBuddyPull.Application.UseCases.Commands.Users.UpdatePassword;

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
    public async Task<IActionResult> Login([FromBody]LoginVM vm)
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

    [HttpPost("updatePassword")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordVM vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Datos invalidos");
        }

        var result = await _mediator.Send(new UpdatePasswordCommand()
        {
            NewPassword = vm.NewPassword,
            OldPassword = vm.OldPassword,
            UserId = Guid.Parse(User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value)
        });

        if (!result)
            return BadRequest("Ocurrio un error al actualizar la contraseña");

        return Ok("Contraseña actualizada");
    }

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Datos invalidos");
        }

        var result = await _mediator.Send(new ResetPasswordByEmailCommand()
        {
             UserEmail = vm.Email,
        });

        if (!result)
            return BadRequest("Ocurrio un error al resetear la contraseña");

        return Ok("Contraseña reseteada");
    }
}
