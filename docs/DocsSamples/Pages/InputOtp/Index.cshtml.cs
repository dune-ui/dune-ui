using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.InputOtp;

public class Index : PageModel
{
    public IndexModel Model { get; set; } = new IndexModel();

    public void OnGet()
    {
        ModelState.AddModelError(
            "Model.Validation.OneTimePassword",
            "Enter the complete 6-digit code"
        );
    }

    public class IndexModel
    {
        public ModelBindingModel ModelBinding { get; set; } = new ModelBindingModel();

        public ValidationModel Validation { get; set; } = new ValidationModel();
    }

    public class ModelBindingModel
    {
        [Display(
            Name = "One-time password",
            Description = "Enter the 6-digit code we sent to your phone."
        )]
        public string OneTimePassword { get; set; } = "123";
    }

    public class ValidationModel
    {
        [Display(
            Name = "One-time password",
            Description = "Enter the 6-digit code we sent to your phone."
        )]
        [Required(ErrorMessage = "Enter the complete 6-digit code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Enter the complete 6-digit code")]
        public string OneTimePassword { get; set; } = "123";
    }
}
