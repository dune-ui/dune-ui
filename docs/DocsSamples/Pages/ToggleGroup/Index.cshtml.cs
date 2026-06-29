using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.ToggleGroup;

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
