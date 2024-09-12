using System.ComponentModel.DataAnnotations;

namespace JobBoard.ViewModels
{
    public class JobPostingViewModel
    {
        [Required(ErrorMessage = "Please enter job title.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be atleast 3 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter job description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter company name.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Company name must be atleast 3 characters long.")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Please enter job location.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Location must be atleast 3 characters long.")]
        public string Location { get; set; }
    }
}
