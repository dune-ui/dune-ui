using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Radio : PageModel
{
    public RadioModel Model { get; set; } =
        new RadioModel { Layout = "comfortable", ComputeEnvironment = "vm" };

    public void OnGet() { }

    public class RadioModel
    {
        public string? ComputeEnvironment { get; set; }
        public string? Layout { get; set; }
    }
}
