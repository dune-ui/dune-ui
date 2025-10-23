using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComponentPlayground.Pages.Demo;

public class Select : PageModel
{
    [BindProperty]
    public SelectModel Model { get; set; }

    public void OnGet()
    {
        Model = new SelectModel { Category = "SPORT", CategoryEnum = ProductCategory.Jewelry };
    }
}

public class SelectModel
{
    public List<SelectListItem> Categories =>
        new List<SelectListItem>
        {
            new SelectListItem { Text = "All", Value = "ALL" },
            new SelectListItem { Text = "Electronics", Value = "ELECT" },
            new SelectListItem { Text = "Clothing", Value = "CLOTH" },
            new SelectListItem { Text = "Home & Garden", Value = "HOME" },
            new SelectListItem { Text = "Books", Value = "BOOKS" },
            new SelectListItem { Text = "Beauty & Personal Care", Value = "BEAUT" },
            new SelectListItem { Text = "Sports & Outdoors", Value = "SPORT" },
            new SelectListItem { Text = "Toys & Games", Value = "TOYS" },
            new SelectListItem { Text = "Automotive", Value = "AUTOS" },
            new SelectListItem { Text = "Jewelry", Value = "JEWEL" },
            new SelectListItem { Text = "Health & Wellness", Value = "HEALT" },
        };

    [Display(Name = "Category", Description = "Select a product category")]
    public string Category { get; set; } = default!;

    [Display(Name = "Category", Description = "Select a product category")]
    public ProductCategory CategoryEnum { get; set; }
}

public enum ProductCategory
{
    [Display(Name = "All")]
    All,

    [Display(Name = "Electronics")]
    Electronics,

    [Display(Name = "Clothing")]
    Clothing,

    [Display(Name = "Home & Garden")]
    HomeAndGarden,

    [Display(Name = "Books")]
    Books,

    [Display(Name = "Beauty & Personal Care")]
    BeautyAndPersonalCare,

    [Display(Name = "Sports & Outdoors")]
    SportsAndOutdoors,

    [Display(Name = "Toys & Games")]
    ToysAndGames,

    [Display(Name = "Automotive")]
    Automotive,

    [Display(Name = "Jewelry")]
    Jewelry,

    [Display(Name = "Health & Wellness")]
    HealthAndWellness,
}
