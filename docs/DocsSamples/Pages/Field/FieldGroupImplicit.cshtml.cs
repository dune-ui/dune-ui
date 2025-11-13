using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class FieldGroupImplicit : PageModel
{
    public FieldGroupImplicitModel Model { get; set; } =
        new FieldGroupImplicitModel { EnableResponsePushNotifications = true };

    public void OnGet() { }
}

public class FieldGroupImplicitModel
{
    [Display(Name = "Push notifications")]
    public bool EnableResponsePushNotifications { get; set; }

    [Display(Name = "Email notifications")]
    public bool EnableTaskEmailNotifications { get; set; }

    [Display(Name = "Push notifications")]
    public bool EnableTaskPushNotifications { get; set; }
}
