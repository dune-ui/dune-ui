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
        public string? Password { get; set; }
    }
}
