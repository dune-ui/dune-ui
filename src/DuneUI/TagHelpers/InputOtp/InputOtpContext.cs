namespace DuneUI.TagHelpers;

/// <summary>
///     The shared state a <c>dui-input-otp</c> publishes for its descendant slots to consume.
///     The host owns the resolved code, the total length, and the validation state; a slot only
///     contributes its position, which it self-assigns from <see cref="NextIndex" /> when the
///     author did not set an explicit <c>index</c>.
/// </summary>
internal sealed class InputOtpContext
{
    /// <summary>
    ///     The resolved one-time-code used to seed the slots for the initial (pre-hydration)
    ///     server render. May be shorter than <see cref="MaxLength" /> or empty.
    /// </summary>
    public required string Code { get; init; }

    public required int MaxLength { get; init; }

    public required bool Disabled { get; init; }

    /// <summary>
    ///     Whether the bound field has a validation error, so slots can reflect <c>aria-invalid</c>.
    /// </summary>
    public required bool HasError { get; init; }

    /// <summary>
    ///     A running counter so sequentially-authored slots self-assign their index. Mutable by
    ///     design: each slot reads it and advances it.
    /// </summary>
    public int NextIndex { get; set; }

    /// <summary>
    ///     The character displayed in the slot at <paramref name="index" /> for the initial server
    ///     render, or <c>null</c> when the bound code is shorter.
    /// </summary>
    public string? CharAt(int index) =>
        index >= 0 && index < Code.Length ? Code[index].ToString() : null;
}
