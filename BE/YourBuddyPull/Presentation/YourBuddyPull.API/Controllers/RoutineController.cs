using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourBuddyPull.API.ViewModels.Common;
using YourBuddyPull.API.ViewModels.Routine;
using YourBuddyPull.Application.UseCases.Commands.Routines.AddExerciseToRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.AssignUserToRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.CreateRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.DeactivateRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.DuplicateRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.RemoveExerciseFromRoutine;
using YourBuddyPull.Application.UseCases.Commands.Routines.UnassignUserToRoutine;
using YourBuddyPull.Application.UseCases.Queries.Routines.GetAllRoutines;
using YourBuddyPull.Application.UseCases.Queries.Routines.GetRoutinesForUser;

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
        public async Task<IActionResult> Get([FromQuery]PaginationInfo pagination)
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

        [HttpGet("GetByUserId/{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id, [FromQuery] PaginationInfo pagination)
        {
            if (pagination.CurrentPage < 1)
                pagination.CurrentPage = 1;
            if (pagination.PageSize < 5)
                pagination.PageSize = 5;

            var result = _mediator.Send(
                new GetRoutinesForUserQuery()
                {
                    CurrentPage = pagination.CurrentPage,
                    PageSize = pagination.PageSize,
                    userId = id
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
                        CreatedById = vm.CreatedById,
                        createdByName = vm.createdByName,
                        Name = vm.Name
                    }
                );
            if(!result)
            {
                return BadRequest("Ocurrio un error al intentar crear la rutina");
            }

            return Ok("Rutina creada correctamente");
        }

        [HttpPost("Duplicate")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post(Guid id)
        {
            var currentUserId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "id").Value ?? "";
            var result = await _mediator.Send(
                    new DuplicateRoutineCommand()
                    {
                        CreatedBy = Guid.Parse(currentUserId),
                        Id = id,
                    }
                );

            return Ok(new
            {
                message= "Rutina creada exitosamente",
                guid= result
            });
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

        [HttpPost("Unassign")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Unassign([FromBody]Guid id)
        {
            var result = await _mediator.Send(
                    new UnassignUserToRoutineCommand()
                    {
                        RoutineId = id
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("La runtina se encuentra ahora sin asignar");
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
    }
}
