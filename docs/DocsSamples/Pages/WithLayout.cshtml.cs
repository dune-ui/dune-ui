using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages;

public class WithLayout : PageModel
{
    public string PartialName { get; set; }

    public void OnGet(string name)
    {
        PartialName = name;
    }
}
