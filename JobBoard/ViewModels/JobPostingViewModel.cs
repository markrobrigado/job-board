using System.ComponentModel.DataAnnotations;

namespace JobBoard.ViewModels
{
    public class JobPostingViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
