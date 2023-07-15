using MediatR;
using YourBuddyPull.Application.DTOs.Exercise;
using YourBuddyPull.Application.DTOs.Shared;

namespace YourBuddyPull.Application.UseCases.Queries.Exercises.GetSingleExercise;

public class GetSingleExerciseQuery
    : IRequest<ExerciseDTO>
{
    public Guid ExerciseId { get; set; }
}
