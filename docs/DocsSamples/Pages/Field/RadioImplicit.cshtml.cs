using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class RadioImplicit : PageModel
{
    public RadioImplicitModel Model { get; set; } =
        new RadioImplicitModel { SubscriptionType = "monthly" };

    public void OnGet() { }
}

public class RadioImplicitModel
{
    public string? SubscriptionType { get; set; }
}
