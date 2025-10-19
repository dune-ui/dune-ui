using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Label : PageModel
{
    public PersonModel Model { get; set; } = new PersonModel();

    public void OnGet() { }

    public class PersonModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }
    }
}
