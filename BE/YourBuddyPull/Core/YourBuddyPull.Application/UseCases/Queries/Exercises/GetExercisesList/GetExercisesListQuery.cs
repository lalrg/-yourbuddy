using MediatR;
using YourBuddyPull.Application.DTOs.Exercise;
using YourBuddyPull.Application.DTOs.Shared;

namespace YourBuddyPull.Application.UseCases.Queries.Exercises.GetExercisesList;

public class GetExercisesListQuery
    : IRequest<PaginationResultDTO<ExerciseDTO>>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}
