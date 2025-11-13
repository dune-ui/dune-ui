using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Input;

public class InputTypeDataBinding : PageModel
{
    public InputTypeDataBindingModel Model { get; set; } = new InputTypeDataBindingModel();

    public void OnGet() { }
}

public class InputTypeDataBindingModel
{
    public bool Checkbox { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Date { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? Radio { get; set; }

    public string? Text { get; set; }
}
