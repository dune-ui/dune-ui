using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Toggle : PageModel
{
    [BindProperty]
    public ToggleModel Model { get; set; } = new ToggleModel();

    public bool Posted { get; set; }

    public void OnGet() { }

    public void OnPost()
    {
        Posted = true;
    }

    public class ToggleModel
    {
        [Display(Name = "Breakfast included")]
        public bool BreakfastIncluded { get; set; } = true;

        [Display(Name = "Results view")]
        public string ResultsView { get; set; } = "grid";

        [Display(Name = "Amenities")]
        public string[] Amenities { get; set; } = ["wifi", "pool"];

        [Display(Name = "Sort results by")]
        public ResultsSort Sort { get; set; } = ResultsSort.Recommended;

        [Display(Name = "Trip styles")]
        public TripStyle[] TripStyles { get; set; } = [TripStyle.Beach, TripStyle.City];
    }

    public enum ResultsSort
    {
        Recommended,
        Price,
        Rating,
    }

    public enum TripStyle
    {
        Beach,
        City,
        Camping,
    }
}
