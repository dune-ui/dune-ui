using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class Implicit : PageModel
{
    public ImplicitModel Model { get; set; } = new ImplicitModel();

    public void OnGet() { }
}

public class ImplicitModel
{
    [Display(
        Name = "Card Number",
        Description = "Enter your 16-digit card number",
        Prompt = "1234 5678 9012 3456"
    )]
    public string? CardNumber { get; set; }

    [Display(Name = "Comments", Prompt = "Add any additional comments")]
    public string? Comments { get; set; }

    [Display(Name = "CVV")]
    public string? Cvv { get; set; }

    [Display(Name = "Same as shipping address")]
    public bool IsBillingAddressSame { get; set; }

    public string? Month { get; set; }

    [Display(Name = "Name on Card", Prompt = "Ibn Battuta")]
    public string? Name { get; set; }

    public string? Year { get; set; }
}
