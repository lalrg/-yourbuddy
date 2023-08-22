using MediatR;
using YourBuddyPull.Application.DTOs.Exercise;

namespace YourBuddyPull.Application.UseCases.Queries.Exercises.GetAllExercises;

public class GetAllExercisesQuery: IRequest<List<ExerciseDTO>>
{
}
