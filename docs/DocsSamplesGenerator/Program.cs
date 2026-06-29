using Spectre.Console;

namespace DocsSamplesGenerator;

public static class Program
{
    public static async Task<int> Main()
    {
        try
        {
            var generator = new Generator();

            AnsiConsole.Write(new Rule("[bold]DuneUI docs generator[/]").LeftJustified());
            AnsiConsole.MarkupLineInterpolated(
                $"Hosting DocsSamples from: [grey]{Generator.DocsSamplesContentRoot}[/]"
            );

            using var factory = new DocsSamplesApplicationFactory(Generator.DocsSamplesContentRoot);
            using var client = factory.CreateClient();

            await AnsiConsole
                .Status()
                .StartAsync(
                    "Cleaning existing output...",
                    async ctx =>
                    {
                        generator.CleanOutputFolders();
                        AnsiConsole.MarkupLine("[green]✓[/] Cleaned existing output.");

                        ctx.Status("Downloading fixed static assets...");
                        await generator.DownloadFixedStaticAssetsAsync(client);
                        AnsiConsole.MarkupLine("[green]✓[/] Downloaded fixed static assets.");

                        ctx.Status("Downloading dynamic static assets...");
                        await generator.DownloadDynamicStaticAssetsAsync(client);
                        AnsiConsole.MarkupLine("[green]✓[/] Downloaded dynamic static assets.");
                    }
                );

            await AnsiConsole
                .Progress()
                .Columns(
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn(),
                    new SpinnerColumn()
                )
                .StartAsync(async ctx =>
                {
                    var task = ctx.AddTask(
                        "[green]Generating demos[/]",
                        maxValue: Generator.DemoPartials.Length
                    );

                    foreach (var partial in Generator.DemoPartials)
                    {
                        await generator.GenerateDemoPartialSourceFileAsync(partial.Name);
                        await generator.RenderDemoPartialOutputAsync(
                            client,
                            partial.Name,
                            partial.Layout
                        );

                        task.Increment(1);
                    }
                });

            AnsiConsole.MarkupLineInterpolated(
                $"[green]✓[/] Generated {Generator.DemoPartials.Length} demo partials."
            );
            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]Generation failed:[/]");
            AnsiConsole.WriteException(ex);
            return 1;
        }
    }
}
