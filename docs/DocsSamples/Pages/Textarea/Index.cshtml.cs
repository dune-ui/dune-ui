using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Textarea;

public class Index : PageModel
{
    public IndexModel Model { get; set; }

    public void OnGet()
    {
        ModelState.AddModelError(
            "Model.ReviewValidation.Review",
            "Please leave a review for other travelers"
        );
    }

    public class IndexModel
    {
        public ReviewModel Review { get; set; } = new ReviewModel();

        public ReviewValidationModel ReviewValidation { get; set; } = new ReviewValidationModel();
    }

    public class ReviewModel
    {
        [Display(
            Name = "Describe your experience",
            Description = "Your feedback helps other travelers make better choices. Be as descriptive as possible!",
            Prompt = "The view from the balcony was breathtaking, but the breakfast service was a bit slow..."
        )]
        public string? Review { get; set; }
    }

    public class ReviewValidationModel
    {
        [Display(
            Name = "Describe your experience",
            Description = "Your feedback helps other travelers make better choices. Be as descriptive as possible!",
            Prompt = "The view from the balcony was breathtaking, but the breakfast service was a bit slow..."
        )]
        [Required(ErrorMessage = "Please leave a review for other travelers")]
        public string? Review { get; set; }
    }
}
