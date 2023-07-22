using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.DTOs.Exercise;
using YourBuddyPull.Application.DTOs.Shared;

namespace YourBuddyPull.Application.UseCases.Queries.Exercises.GetExercisesList;

public class GetExercisesListHandler : IRequestHandler<GetExercisesListQuery, PaginationResultDTO<ExerciseDTO>>
{
    private readonly IExerciseRepository _exerciseRepository;
    public GetExercisesListHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;    
    }
    public async Task<PaginationResultDTO<ExerciseDTO>> Handle(GetExercisesListQuery request, CancellationToken cancellationToken)
    {
        var paginationInfo = new PaginationDTO() { CurrentPage = request.CurrentPage, PageSize = request.PageSize };
        return await _exerciseRepository.GetAllPaged(paginationInfo);
    }
}
