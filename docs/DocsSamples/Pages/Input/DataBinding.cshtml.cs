using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Input;

public class DataBinding : PageModel
{
    public DataBindingModel Model { get; set; } = new DataBindingModel();

    public void OnGet() { }
}

public class DataBindingModel
{
    [Display(
        Name = "Email address",
        Description = "The email address where you want to receive your booking confirmation",
        Prompt = "e.g. ibn.battuta@rihlah.travel"
    )]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
}
