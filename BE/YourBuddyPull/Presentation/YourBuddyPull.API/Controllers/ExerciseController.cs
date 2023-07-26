using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourBuddyPull.API.ViewModels.Common;
using YourBuddyPull.API.ViewModels.Exercise;
using YourBuddyPull.Application.UseCases.Commands.Exercises.AddExercise;
using YourBuddyPull.Application.UseCases.Commands.Exercises.EditExercise;
using YourBuddyPull.Application.UseCases.Queries.Exercises.GetExercisesList;
using YourBuddyPull.Application.UseCases.Queries.Exercises.GetSingleExercise;

namespace YourBuddyPull.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExerciseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExerciseController(IMediator mediator)
    {
        _mediator = mediator;   
    }

    [HttpPost]
    [Authorize(Roles ="admin")]
    public async Task<IActionResult> Post(AddExerciseVM vm)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos invalidos");

        var result = await _mediator.Send(new AddExerciseCommand()
        {
            ExerciseDescription = vm.ExerciseDescription,
            ExerciseName = vm.ExerciseName,
            ExerciseType = vm.ExerciseType,
            ImageUrl = vm.ImageUrl,
            VideoUrl = vm.VideoUrl
        });

        if (!result)
            return BadRequest("La operacion no pudo ser completada");
        
        return Ok("Ejercicio creado con exito");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> EditExercise(Guid id, EditExerciseVM vm)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos invalidos");

        var result = await _mediator.Send(new EditExerciseCommand()
        {
            Description = vm.ExerciseDescription,
            Id = id,
            ImageUrl = vm.ImageUrl,
            Name = vm.ExerciseName,
            Type = vm.ExerciseType,
            VideoUrl = vm.VideoUrl
        });

        if (!result)
            return BadRequest("La operacion no pudo ser completada");

        return Ok("Ejercicio editado con exito");
        
    }

    [HttpGet]
    [Authorize(Roles ="admin")]
    public async Task<IActionResult> Get([FromQuery]PaginationInfo pagination)
    {
        if (pagination.CurrentPage < 1)
            pagination.CurrentPage = 1;
        if(pagination.PageSize < 5) 
            pagination.PageSize = 5;

        var result = await _mediator.Send(new GetExercisesListQuery()
        {
            CurrentPage = pagination.CurrentPage,
            PageSize = pagination.PageSize
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Solicitud no valida");

        var result = _mediator.Send(new GetSingleExerciseQuery()
        {
            ExerciseId = id
        });

        if (result == null)
            return BadRequest("Ha ocurrido un error con la solicitud");

        return Ok(result);
    }
}
