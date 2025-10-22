using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Textarea : PageModel
{
    public TestModel Model { get; set; } =
        new TestModel { Comments = "These are some comments..." };

    public void OnGet() { }

    public class TestModel
    {
        [Display(Name = "Some comments", Description = "Enter up to 250 characters of comments")]
        public string Comments { get; set; }
    }
}
