using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Slider;

public class Index : PageModel
{
    public IndexModel Model { get; set; } = new IndexModel();

    public void OnGet() { }

    public class IndexModel
    {
        public ModelBindingModel ModelBinding { get; set; } = new ModelBindingModel();
    }

    public class ModelBindingModel
    {
        [Display(
            Name = "Maximum distance from center",
            Description = "Only show stays within this many km of the city center."
        )]
        public int MaximumDistanceFromCenter { get; set; } = 40;

        [Display(Name = "Price per night", Description = "Filter stays to a nightly price range.")]
        public int[] PricePerNight { get; set; } = [200, 800];

        [Display(
            Name = "Guest rating bands",
            Description = "Boundaries for the low, medium, and high guest-rating bands."
        )]
        public int[] GuestRatingBands { get; set; } = [25, 50, 75];
    }
}
