using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Radio : PageModel
{
    public RadioModel Model { get; set; }

    public void OnGet()
    {
        Model = new RadioModel { Layout = "comfortable", ComputeEnvironment = "vm" };
    }

    public class RadioModel
    {
        public string ComputeEnvironment { get; set; }
        public string Layout { get; set; }
    }
}
