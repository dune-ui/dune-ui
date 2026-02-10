using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Select;

public class Index : PageModel
{
    public IndexModel Model { get; set; } = new IndexModel();

    public void OnGet()
    {
        ModelState.AddModelError(
            "Model.BookingFormValidation.CabinClass",
            "Please select a valid cabin class"
        );
    }

    public class IndexModel
    {
        public BookingFormModel BookingForm { get; set; } =
            new BookingFormModel { CabinClass = CabinClass.PremiumEconomy };

        public BookingFormValidationModel BookingFormValidation { get; set; } =
            new BookingFormValidationModel();
    }

    public class BookingFormModel
    {
        [Display(
            Name = "Cabin Class",
            Description = "Higher classes offer more legroom, priority boarding, and flexible refund policies."
        )]
        public CabinClass CabinClass { get; set; }
    }

    public class BookingFormValidationModel
    {
        [Display(
            Name = "Cabin Class",
            Description = "Higher classes offer more legroom, priority boarding, and flexible refund policies."
        )]
        [Required(ErrorMessage = "Please select a valid cabin class")]
        public CabinClass? CabinClass { get; set; }
    }
}
