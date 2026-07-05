namespace DuneUI;

/// <summary>
///     Root options for configuring DuneUI. Configure it via the
///     <see cref="Builders.DuneUIBuilder" /> returned from <c>AddDuneUI()</c> (for example
///     <see cref="Builders.DuneUIBuilder.ConfigureMenu" />); components read the effective
///     defaults at render time via <c>IOptions&lt;DuneUIOptions&gt;</c>.
/// </summary>
public class DuneUIOptions
{
    /// <summary>Defaults for floating menu surfaces (Dropdown Menu, and future menu families).</summary>
    public DuneUIMenuOptions Menu { get; } = new();
}
