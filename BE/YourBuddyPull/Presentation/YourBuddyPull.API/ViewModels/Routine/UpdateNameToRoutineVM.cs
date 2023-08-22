using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine
{
    public class UpdateNameToRoutineVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid Id { get; set; }
    }
}
