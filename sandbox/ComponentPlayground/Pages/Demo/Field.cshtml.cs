using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Field : PageModel
{
    public InputModel Model { get; set; } = default!;

    public void OnGet()
    {
        Model = new InputModel();
    }

    public class InputModel
    {
        [Display(Name = "First Name", Description = "First name as displayed on card")]
        public string? FirstName { get; set; }
    }
}
