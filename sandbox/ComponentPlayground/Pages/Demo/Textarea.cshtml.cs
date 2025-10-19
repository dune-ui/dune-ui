using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Textarea : PageModel
{
    public TestModel Model { get; set; } =
        new TestModel { Comments = "These are some comments..." };

    public void OnGet() { }

    public class TestModel
    {
        public string Comments { get; set; }
    }
}
