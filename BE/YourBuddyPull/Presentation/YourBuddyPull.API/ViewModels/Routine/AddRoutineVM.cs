using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine
{
    public class AddRoutineVM
    {
        [Required]
        public string Name { get; set; }
    }
}
