using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Field : PageModel
{
    public InputModel Model { get; set; } = new InputModel();

    public void OnGet()
    {
        ModelState.AddModelError(
            $"Model.{nameof(InputModel.FirstName)}",
            "This is a super-dooper error"
        );
    }

    public class InputModel
    {
        [Display(Name = "First Name", Description = "First name as displayed on card")]
        public string? FirstName { get; set; }
    }
}
