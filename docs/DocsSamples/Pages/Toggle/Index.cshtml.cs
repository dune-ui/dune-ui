using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Toggle;

public class Index : PageModel
{
    public IndexModel Model { get; set; } = new IndexModel();

    public void OnGet() { }

    public class IndexModel
    {
        public ModelBindingModel ModelBinding { get; set; } = new ModelBindingModel();
    }

    public class ModelBindingModel
    {
        [Display(
            Name = "Breakfast included",
            Description = "Only show stays that serve breakfast."
        )]
        public bool BreakfastIncluded { get; set; } = true;
    }
}
