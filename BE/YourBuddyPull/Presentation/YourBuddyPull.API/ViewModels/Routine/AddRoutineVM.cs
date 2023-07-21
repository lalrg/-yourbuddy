using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine
{
    public class AddRoutineVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CreatedById { get; set; }
        public string createdByName { get; set; }
    }
}
