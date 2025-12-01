using System.Collections.Immutable;
using StellarAdmin;
using StellarAdmin.Icons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder
    .Services.AddStellarAdmin()
    .AddIcon(
        "ampersand-square",
        [
            new SvgShape(
                "rect",
                new Dictionary<string, string>
                {
                    ["width"] = "18",
                    ["height"] = "18",
                    ["x"] = "3",
                    ["y"] = "3",
                    ["rx"] = "2",
                }.ToImmutableDictionary()
            ),
            new SvgShape(
                "path",
                new Dictionary<string, string>
                {
                    ["d"] =
                        "M16 17c-4-2-7-6-7-8a2 2 0 0 1 4 0c0 3-5 1.5-5 5 0 1.7 1.3 3 3 3 3 0 5-2 5-4",
                }.ToImmutableDictionary()
            ),
        ]
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
