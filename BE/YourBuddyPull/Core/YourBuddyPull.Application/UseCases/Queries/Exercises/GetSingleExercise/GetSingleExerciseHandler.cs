using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Exercise;
using YourBuddyPull.Application.DTOs.Shared;

namespace YourBuddyPull.Application.UseCases.Queries.Exercises.GetSingleExercise;

public class GetSingleExerciseHandler : IRequestHandler<GetSingleExerciseQuery, ExerciseDTO>
{
    private readonly IExerciseRepository _exerciseRepository;
    public GetSingleExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;    
    }
    public async Task<ExerciseDTO> Handle(GetSingleExerciseQuery request, CancellationToken cancellationToken)
    {
        return await _exerciseRepository.GetExerciseInformationById(request.ExerciseId);
    }
}
