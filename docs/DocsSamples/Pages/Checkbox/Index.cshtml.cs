using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Checkbox;

public class Index : PageModel
{
    public IndexModel Model { get; set; } = new IndexModel();

    public void OnGet()
    {
        ModelState.AddModelError(
            "Model.Validation.AcceptTerms",
            "You must accept the terms and conditions"
        );
    }

    public class IndexModel
    {
        public ModelBindingModel ModelBinding { get; set; } = new ModelBindingModel();

        public ValidationModel Validation { get; set; } = new ValidationModel();

        public GroupModel Group { get; set; } = new GroupModel();
    }

    public class GroupModel
    {
        [Display(Name = "Landmark & Attractions")]
        public bool Landmarks { get; set; } = true;

        [Display(Name = "Restaurants & Bars")]
        public bool Restaurants { get; set; } = true;

        [Display(Name = "Transit Stations")]
        public bool Stations { get; set; }

        [Display(Name = "Local Tour Guides")]
        public bool LocalGuides { get; set; }
    }

    public class ModelBindingModel
    {
        [Display(
            Name = "Accept terms and conditions",
            Description = "By clicking this checkbox, you agree to the terms and conditions."
        )]
        public bool AcceptTerms { get; set; } = true;
    }

    public class ValidationModel
    {
        [Display(
            Name = "Accept terms and conditions",
            Description = "By clicking this checkbox, you agree to the terms and conditions."
        )]
        [Required(ErrorMessage = "You must accept the terms and conditions")]
        public bool AcceptTerms { get; set; }
    }
}
