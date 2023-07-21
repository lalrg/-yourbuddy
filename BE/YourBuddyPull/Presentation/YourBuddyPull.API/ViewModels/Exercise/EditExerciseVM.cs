using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Exercise;

public class EditExerciseVM
{
    [Required]
    public string ExerciseName { get; set; }
    [Required, RegularExpression("time|weight", ErrorMessage = "Tipo de ejercicio no valido")]
    public string ExerciseType { get; set; }
    [Required]
    public string ExerciseDescription { get; set; }
    public string ImageUrl { get; set; }
    public string VideoUrl { get; set; }
}
