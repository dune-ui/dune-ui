using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Input;

public class Index : PageModel
{
    public IndexModel Model { get; set; } = new IndexModel();

    public void OnGet()
    {
        ModelState.AddModelError("Model.Validation.Email", "Enter your email address");
    }

    public class IndexModel
    {
        public InputTypesModelBindingModel InputTypesModelBinding { get; set; } =
            new InputTypesModelBindingModel();

        public ModelBindingModel ModelBinding { get; set; } = new ModelBindingModel();

        public ValidationModel Validation { get; set; } = new ValidationModel();
    }

    public class ModelBindingModel
    {
        [Display(
            Name = "Email address",
            Description = "The email address where you want to receive your booking confirmation",
            Prompt = "e.g. ibn.battuta@rihlah.travel"
        )]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }

    public class ValidationModel
    {
        [Display(
            Name = "Email address",
            Description = "The email address where you want to receive your booking confirmation",
            Prompt = "e.g. ibn.battuta@rihlah.travel"
        )]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter your email address")]
        public string? Email { get; set; }
    }

    public class InputTypesModelBindingModel
    {
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public IFormFile? File { get; set; }

        [Display(Prompt = "123")]
        public int? Number { get; set; }

        [DataType(DataType.Password)]
        [Display(Prompt = "Password")]
        public string? Password { get; set; }

        [Display(Prompt = "Enter a search term...")]
        public string? Search { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Prompt = "+66 (12) 345 6789")]
        public string? Tel { get; set; }

        public string? Text { get; set; }

        [DataType(DataType.Time)]
        public string? Time { get; set; }

        [DataType(DataType.Url)]
        [Display(Prompt = "https://wwww.duneui.com")]
        public string? Url { get; set; }
    }
}
