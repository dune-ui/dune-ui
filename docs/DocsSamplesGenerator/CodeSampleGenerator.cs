using DocsSamples;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DocsSamplesGenerator;

public class CodeSampleGenerator(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private static string OutputFolder = @"C:\development\stellar-admin\stellar-ui";

    // [Fact(Skip = "This test is intended for manual execution only.")]
    [Fact]
    public async Task Generate()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/Alert/Intro?clean");

        File.WriteAllText(
            Path.Combine(OutputFolder, $"{Guid.NewGuid()}.html"),
            await response.Content.ReadAsStringAsync()
        );
    }
}
