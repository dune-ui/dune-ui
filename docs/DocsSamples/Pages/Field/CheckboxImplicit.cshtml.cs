using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Field;

public class CheckboxImplicit : PageModel
{
    public CheckboxImplicitModel Model { get; set; } =
        new CheckboxImplicitModel { MustSyncDesktopFolders = true };

    public void OnGet() { }
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
