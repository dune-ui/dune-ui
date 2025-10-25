using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Checkbox : PageModel
{
    public CheckboxModel Model { get; set; }

    public void OnGet()
    {
        ModelState.AddModelError("Model.AcceptTerms", "You must accept the terms and conditions");
        Model = new CheckboxModel { BooleanValue = true, AcceptTerms = false };
    }

    public class CheckboxModel
    {
        [Display(Name = "Bound value", Description = "This is a value bound to a checkbox")]
        public bool BooleanValue { get; set; }

        [Display(Name = "Accept terms")]
        public bool AcceptTerms { get; set; }
    }
}
