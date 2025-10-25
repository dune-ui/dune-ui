using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Input : PageModel
{
    public InputModel Model { get; set; } =
        new InputModel
        {
            BooleanValue = true,
            DateOnlyValue = DateOnly.FromDateTime(DateTime.Now),
            DateTimeValue = DateTime.Now,
            DoubleValue = 123.45,
            IntValue = 123,
            PasswordValue = "password",
            StringValue = "This is a string value",
            TimeOnlyValue = TimeOnly.FromDateTime(DateTime.Now),
        };

    public void OnGet() { }

    public class InputModel
    {
        [Display(Name = "Boolean value", Description = "This is a boolean value")]
        public bool BooleanValue { get; set; }

        [Display(Name = "Date value", Description = "This is a date value")]
        public required DateOnly DateOnlyValue { get; set; }

        [Display(Name = "Date/time value", Description = "This is a date/time value")]
        public required DateTime DateTimeValue { get; set; }

        [Display(Name = "Double value", Description = "This is a double value")]
        public required double DoubleValue { get; set; }

        [Display(Name = "File value", Description = "This is a file value")]
        public IFormFile? FileValue { get; set; }

        [Display(Name = "Int value", Description = "This is a int value")]
        public required int IntValue { get; set; }

        [DataType(DataType.Password)]
        [Display(
            Name = "Password value",
            Description = "This is a password value as indicated with the DataType attribute"
        )]
        public required string PasswordValue { get; set; }

        [Display(Name = "String value", Description = "This is a string value")]
        public required string StringValue { get; set; }

        [Display(Name = "Time value", Description = "This is a time value")]
        public required TimeOnly TimeOnlyValue { get; set; }
    }
}
