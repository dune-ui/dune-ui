using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class SelectImplicit : PageModel
{
    public SelectImplicitModel Model { get; set; } = new SelectImplicitModel();

    public void OnGet() { }
}

public class SelectImplicitModel
{
    [Display(Name = "Department", Description = "Select your department or area of work.")]
    public string? Department { get; set; }
}
