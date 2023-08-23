using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YourBuddyPull.API.ViewModels.Common;
using YourBuddyPull.API.ViewModels.TrainingSession;
using YourBuddyPull.Application.UseCases.Commands.Exercises.RemoveExercise;
using YourBuddyPull.Application.UseCases.Commands.TrainingSessions.AddExerciseToTrainingSession;
using YourBuddyPull.Application.UseCases.Commands.TrainingSessions.AddTrainingSession;
using YourBuddyPull.Application.UseCases.Commands.TrainingSessions.RemoveExerciseFromTrainingSession;
using YourBuddyPull.Application.UseCases.Commands.TrainingSessions.UpdateTrainingSession;
using YourBuddyPull.Application.UseCases.Queries.TrainingSessions.GetSingleTrainingSession;
using YourBuddyPull.Application.UseCases.Queries.TrainingSessions.GetTrainingSessionsForUser;

namespace YourBuddyPull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSessionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrainingSessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateTrainingSessionVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new AddTrainingSessionCommand()
                    {
                        CreatedBy = vm.CreatedBy,
                        CreatedByName = "",
                        startTime = vm.startTime,
                        endTime = vm.endTime,
                        RoutineFrom = vm.RoutineFrom
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("Sesion creada correctamente");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] PaginationInfo pagination)
        {
            if (pagination.CurrentPage < 1)
                pagination.CurrentPage = 1;
            if (pagination.PageSize < 5)
                pagination.PageSize = 5;

            var result = await _mediator.Send(
                    new GetTrainingSessionsForUserQuery()
                    {
                        CurrentPage = pagination.CurrentPage,
                        PageSize = pagination.PageSize,
                        UserId = Guid.Parse(User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value)
                    }
                );
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(
                new GetSingleTrainingSessionQuery()
                {
                    TrainingSessionId = id
                });

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(UpdateTrainingSessionVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new UpdateTrainingSessionCommand()
                    {
                        endTime = vm.endTime,
                        SessionId = vm.SessionId,
                        startTime = vm.startTime
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("Sesion actualizada correctamente");
        }

        [HttpPost("addExercise")]
        [Authorize]
        public async Task<IActionResult> AddExercise(AddExerciseVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new AddExerciseToTrainingSessionCommand()
                    {
                        exerciseId = vm.exerciseId,
                        load = vm.load,
                        reps = vm.reps,
                        sessionId = vm.sessionId,
                        sets = vm.sets                        
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("Sesion actualizada correctamente");
        }

        [HttpPost("removeExercise")]
        [Authorize]
        public async Task<IActionResult> removeExercise(RemoveExerciseVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos");

            var result = await _mediator.Send(
                    new RemoveExerciseFromTrainingSessionCommand()
                    {
                        sessionId = vm.sessionId,
                        exerciseId = vm.exerciseId
                    }
                );

            if (!result)
                return BadRequest("Ha ocurrido un error");

            return Ok("Sesion actualizada correctamente");
        }
    }
}
