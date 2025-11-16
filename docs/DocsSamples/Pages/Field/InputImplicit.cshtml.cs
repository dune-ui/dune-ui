using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class InputImplicit : PageModel
{
    public InputImplicitModel Model { get; set; } = new InputImplicitModel();

    public void OnGet() { }
}

public class InputImplicitModel
{
    [Display(
        Name = "Email",
        Description = "Your email address",
        Prompt = "e.g. ibn.battuta@rihlah.travel"
    )]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(
        Name = "Password",
        Description = "Must be at least 8 characters long.",
        Prompt = "••••••••"
    )]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
