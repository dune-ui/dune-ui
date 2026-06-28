namespace DocsSamplesGenerator;

public static class Program
{
    public static async Task<int> Main()
    {
        try
        {
            var generator = new Generator();

            Console.WriteLine($"Hosting DocsSamples from: {Generator.DocsSamplesContentRoot}");
            using var factory = new DocsSamplesApplicationFactory(Generator.DocsSamplesContentRoot);
            using var client = factory.CreateClient();

            Console.WriteLine("Downloading fixed static assets...");
            await generator.DownloadFixedStaticAssetsAsync(client);

            Console.WriteLine("Downloading dynamic static assets...");
            await generator.DownloadDynamicStaticAssetsAsync(client);

            Console.WriteLine($"Generating {Generator.DemoPartials.Length} demo partials...");
            var count = 0;
            foreach (var partial in Generator.DemoPartials)
            {
                await generator.GenerateDemoPartialSourceFileAsync(partial.Name);
                await generator.RenderDemoPartialOutputAsync(client, partial.Name, partial.Layout);

                count++;
                if (count % 25 == 0 || count == Generator.DemoPartials.Length)
                    Console.WriteLine($"  {count}/{Generator.DemoPartials.Length}");
            }

            Console.WriteLine("Done.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Generation failed: {ex}");
            return 1;
        }
    }
}
