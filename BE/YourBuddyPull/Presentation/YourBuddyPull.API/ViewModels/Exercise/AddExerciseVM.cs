using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Exercise;

public class AddExerciseVM
{
    [Required]
    public string ExerciseName { get; set; }
    [Required, RegularExpression("time|weight", ErrorMessage = "Tipo de ejercisio no valido")]
    public string ExerciseType { get; set; }
    [Required]
    public string ExerciseDescription { get; set; }
    public string ImageUrl { get; set; }
    public string VideoUrl { get; set; }
}
