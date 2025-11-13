using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class FieldsetImplicit : PageModel
{
    public FieldsetImplicitModel Model { get; set; } = new FieldsetImplicitModel();

    public void OnGet() { }
}

public class FieldsetImplicitModel
{
    [Display(Name = "Street Address", Prompt = "123 Main St")]
    public string? Street { get; set; }

    [Display(Name = "City", Prompt = "New York")]
    public string? City { get; set; }

    [Display(Name = "Postal Code", Prompt = "90502")]
    public string? PostalCode { get; set; }
}
