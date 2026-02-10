using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class Index : PageModel
{
    public IndexModel Model { get; set; } =
        new IndexModel
        {
            CheckboxImplicit = new CheckboxImplicitModel { MustSyncDesktopFolders = true },
            FieldGroupImplicit = new FieldGroupImplicitModel
            {
                EnableResponsePushNotifications = true,
            },
            FieldsetImplicit = new FieldsetImplicitModel(),
            Implicit = new ImplicitModel(),
            InputImplicit = new InputImplicitModel(),
            RadioImplicit = new RadioImplicitModel { SubscriptionType = "yearly" },
            SelectImplicit = new SelectImplicitModel { Department = "design" },
            TextareaImplicit = new TextareaImplicitModel(),
        };

    public void OnGet() { }
}

public class IndexModel
{
    public CheckboxImplicitModel CheckboxImplicit { get; set; }

    public FieldGroupImplicitModel FieldGroupImplicit { get; set; }

    public FieldsetImplicitModel FieldsetImplicit { get; set; }

    public ImplicitModel Implicit { get; set; }

    public InputImplicitModel InputImplicit { get; set; }

    public RadioImplicitModel RadioImplicit { get; set; }

    public SelectImplicitModel SelectImplicit { get; set; }

    public TextareaImplicitModel TextareaImplicit { get; set; }
}

public class CheckboxImplicitModel
{
    [Display(Name = "CDs, DVDs, and iPods")]
    public bool MustDisplayCdDvd { get; set; }

    [Display(Name = "Connected servers")]
    public bool MustDisplayConnectedServers { get; set; }

    [Display(Name = "External disks")]
    public bool MustDisplayExternalDisks { get; set; }

    [Display(Name = "Hard disks")]
    public bool MustDisplayHardDisks { get; set; }

    [Display(
        Name = "Sync Desktop & Documents folders",
        Description = "Your Desktop & Documents folders are being synced with iCloud Drive. You can access them from other devices."
    )]
    public bool MustSyncDesktopFolders { get; set; }
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

public class FieldsetImplicitModel
{
    [Display(Name = "Street Address", Prompt = "123 Main St")]
    public string? Street { get; set; }

    [Display(Name = "City", Prompt = "New York")]
    public string? City { get; set; }

    [Display(Name = "Postal Code", Prompt = "90502")]
    public string? PostalCode { get; set; }
}

public class ImplicitModel
{
    [Display(
        Name = "Card Number",
        Description = "Enter your 16-digit card number",
        Prompt = "1234 5678 9012 3456"
    )]
    public string? CardNumber { get; set; }

    [Display(Name = "Comments", Prompt = "Add any additional comments")]
    public string? Comments { get; set; }

    [Display(Name = "CVV")]
    public string? Cvv { get; set; }

    [Display(Name = "Same as shipping address")]
    public bool IsBillingAddressSame { get; set; }

    public string? Month { get; set; }

    [Display(Name = "Name on Card", Prompt = "Ibn Battuta")]
    public string? Name { get; set; }

    public string? Year { get; set; }
}

public class InputImplicitModel
{
    [Display(
        Name = "Email",
        Description = "Your email address",
        Prompt = "e.g. ibn.battuta@rihlah.travel"
    )]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(
        Name = "Password",
        Description = "Must be at least 8 characters long.",
        Prompt = "••••••••"
    )]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}

public class RadioImplicitModel
{
    public string? SubscriptionType { get; set; }
}

public class SelectImplicitModel
{
    [Display(Name = "Department", Description = "Select your department or area of work.")]
    public string? Department { get; set; }
}

public class TextareaImplicitModel
{
    [Display(
        Name = "Feedback",
        Description = "Share your thoughts about our service.",
        Prompt = "Your feedback helps us improve..."
    )]
    public string? Feedback { get; set; }
}
