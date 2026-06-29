using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class InputOTP : PageModel
{
    [BindProperty]
    public InputOtpModel Model { get; set; } = new InputOtpModel();

    public bool Posted { get; set; }

    public void OnGet()
    {
        // Pre-seed a validation error so the destructive styling is visible on first load.
        ModelState.AddModelError("Model.VerificationCode", "That code is incorrect");
    }

    public void OnPost()
    {
        Posted = true;
    }

    public class InputOtpModel
    {
        [Display(Name = "Verification code")]
        public string VerificationCode { get; set; } = string.Empty;
    }
}
