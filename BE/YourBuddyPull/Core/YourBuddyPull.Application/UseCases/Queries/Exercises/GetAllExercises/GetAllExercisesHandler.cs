using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Exercise;

namespace YourBuddyPull.Application.UseCases.Queries.Exercises.GetAllExercises;

public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, List<ExerciseDTO>>
{
    private readonly IExerciseRepository _exerciseRepository;
    public GetAllExercisesHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }
    public async Task<List<ExerciseDTO>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        return await _exerciseRepository.GetAll();
    }
}
