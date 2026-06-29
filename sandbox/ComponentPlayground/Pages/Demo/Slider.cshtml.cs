using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComponentPlayground.Pages.Demo;

public class Slider : PageModel
{
    [BindProperty]
    public SliderModel Model { get; set; } = new SliderModel();

    public bool Posted { get; set; }

    public void OnGet() { }

    public void OnPost()
    {
        Posted = true;
    }

    public class SliderModel
    {
        [Display(Name = "Volume")]
        public int Volume { get; set; } = 40;

        [Display(Name = "Price range")]
        public int[] PriceRange { get; set; } = [200, 800];
    }
}
