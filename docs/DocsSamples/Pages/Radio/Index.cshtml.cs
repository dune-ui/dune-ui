using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Radio;

public class Index : PageModel
{
    public IndexModel Model { get; set; } = new IndexModel();

    public void OnGet()
    {
        ModelState.AddModelError("Model.Validation.BedType", "Please select a bed type");
    }

    public class IndexModel
    {
        public ModelBindingModel ModelBinding { get; set; } = new ModelBindingModel();

        public ValidationModel Validation { get; set; } = new ValidationModel();
    }

    public class ModelBindingModel
    {
        public string? BedType { get; set; } = "single";
    }

    public class ValidationModel
    {
        [Required(ErrorMessage = "Please select a bed type")]
        public string? BedType { get; set; }
    }
}
