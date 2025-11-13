using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class TextareaImplicit : PageModel
{
    public TextareaImplicitModel Model { get; set; } = new TextareaImplicitModel();

    public void OnGet() { }
}

public class TextareaImplicitModel
{
    [Display(
        Name = "Feedback",
        Description = "Share your thoughts about our service.",
        Prompt = "Your feedback helps us improve..."
    )]
    public string? Feedback { get; set; }
}
