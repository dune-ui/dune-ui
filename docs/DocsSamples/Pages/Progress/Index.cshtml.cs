using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages.Progress;

public class Index : PageModel
{
    public static List<ProgressFile> Files =>
        [
            new ProgressFile("1", "document.pdf", 45, "2m 30s"),
            new ProgressFile("2", "presentation.pptx", 78, "45s"),
            new ProgressFile("3", "spreadsheet.xlsx", 12, "5m 12s"),
            new ProgressFile("4", "image.jpg", 100, "complete"),
        ];

    public void OnGet() { }

    public record ProgressFile(string Id, string Name, int Progress, string TimeRemaining);
}
