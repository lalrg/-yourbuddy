using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourBuddyPull.API.ViewModels.Common;
using YourBuddyPull.API.ViewModels.TrainingSession;
using YourBuddyPull.Application.UseCases.Commands.TrainingSessions.AddTrainingSession;
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
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PaginationInfo pagination, [FromQuery]Guid userId)
        {
            if (pagination.CurrentPage < 1)
                pagination.CurrentPage = 1;
            if (pagination.PageSize < 5)
                pagination.PageSize = 5;

            var result = _mediator.Send(
                    new GetTrainingSessionsForUserQuery()
                    {
                        CurrentPage = pagination.CurrentPage,
                        PageSize = pagination.PageSize,
                        UserId = userId
                    }
                );

            return Ok(result);
        }

        [HttpPost]
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
        public IActionResult GetByUserId(int userId)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(int id)
        {
            return Ok();
        }
    }
}
