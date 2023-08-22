using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YourBuddyPull.API.ViewModels.Common;
using YourBuddyPull.API.ViewModels.Routine;
using YourBuddyPull.Application.UseCases.Commands.Routines.AddExerciseToRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.AssignUserToRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.ChangeNameToRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.CreateRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.DeactivateRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.DuplicateRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.RemoveExerciseFromRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.UnassignUserToRoutine;
using YourBuddyPull.Application.UseCases.Queries.Routines.GetAllRoutines;
using YourBuddyPull.Application.UseCases.Queries.Routines.GetRoutinesForUser;
using YourBuddyPull.Application.UseCases.Queries.Routines.GetSingleRoutine;

namespace YourBuddyPull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoutineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get([FromQuery] PaginationInfo pagination)
        {
            if (pagination.CurrentPage < 1)
                pagination.CurrentPage = 1;
            if (pagination.PageSize < 5)
                pagination.PageSize = 5;

            var result = await _mediator.Send(
                new GetRoutinesListQuery()
                {
                    CurrentPage = pagination.CurrentPage,
                    PageSize = pagination.PageSize,
                }
                );

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get(Guid id)
        {

            var result = await _mediator.Send(
                new GetSingleRoutineQuery()
                {
                    RoutineId = id
                }
                );

            return Ok(result);
        }

        [HttpGet("GetByUserId")]
        [Authorize]
        public async Task<IActionResult> GetByuserId([FromQuery] PaginationInfo pagination)
        {
            if (pagination.CurrentPage < 1)
                pagination.CurrentPage = 1;
            if (pagination.PageSize < 5)
                pagination.PageSize = 5;

            var result = await _mediator.Send(
                new GetRoutinesForUserQuery()
                {
                    CurrentPage = pagination.CurrentPage,
                    PageSize = pagination.PageSize,
                    userId = Guid.Parse(User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value)
                });

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Post(AddRoutineVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new CreateRoutineCommand()
                    {
                        CreatedById = Guid.Parse(User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value),
                        createdByName = User.Claims.First(u => u.Type == ClaimTypes.Name).Value,
                        Name = vm.Name
                    }
                );
            if(result == Guid.Empty)
            {
                return BadRequest("Ocurrio un error al intentar crear la rutina");
            }

            return Ok(result);
        }

        [HttpPost("Duplicate")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody]DuplicateRoutineVM vm)
        {
            var currentUserId = Guid.Parse(User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _mediator.Send(
                    new DuplicateRoutineCommand()
                    {
                        CreatedBy = currentUserId,
                        Id = vm.id,
                    }
                );

            return Ok(result);
        }

        [HttpPost("AddExercise")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddExercise(AddExerciseVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new AddExerciseToRoutineCommand()
                    {
                        ExerciseId = vm.ExerciseId,
                        load = vm.load,
                        reps = vm.reps,
                        RoutineId = vm.RoutineId,
                        sets = vm.sets
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");
            
            return Ok("Ejercicio agregado correctamente a la rutina");
        }

        [HttpPost("AssignToUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignToUser(AssignToUserVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new AssignUserToRoutineCommand()
                    {
                        RoutineId = vm.routineId,
                        UserId = vm.userId
                    }
                );
            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("Ejercicio asignado correctamente");
        }

        [HttpPost("RemoveExercise")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveExercise(RemoveExerciseVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new RemoveExerciseFromRoutineCommand()
                    {
                        ExerciseId = vm.exerciseId,
                        RoutineId = vm.routineId
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("Ejercicio removido correctamente");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(
                    new DeactivateRoutineCommand()
                    {
                        RoutineId = id
                    });

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("La rutina ha sido desactivada correctamente");
        }

        [HttpPost("updateName")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateName(UpdateNameToRoutineVM vm)
        {
            var result = await _mediator.Send(
                    new ChangeNameToRoutineCommand()
                    {
                        Id = vm.Id,
                        Name = vm.Name,
                    });

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("La rutina ha sido actualizada correctamente");
        }
    }
}
