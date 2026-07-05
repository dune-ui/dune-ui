namespace DuneUI.TagHelpers;

/// <summary>
///     Where a floating element (such as a popover or tooltip) is anchored relative to its trigger.
/// </summary>
public enum PositionArea
{
    /// <summary>Above the trigger, horizontally centred.</summary>
    TopCenter,

    /// <summary>Above the trigger, extending toward the left.</summary>
    TopSpanLeft,

    /// <summary>Above the trigger, extending toward the right.</summary>
    TopSpanRight,

    /// <summary>Above the trigger.</summary>
    Top,

    /// <summary>To the left of the trigger, vertically centred.</summary>
    LeftCenter,

    /// <summary>To the left of the trigger, extending toward the top.</summary>
    LeftSpanTop,

    /// <summary>To the left of the trigger, extending toward the bottom.</summary>
    LeftSpanBottom,

    /// <summary>To the left of the trigger.</summary>
    Left,

    /// <summary>Below the trigger, horizontally centred.</summary>
    BottomCenter,

    /// <summary>Below the trigger, extending toward the left.</summary>
    BottomSpanLeft,

    /// <summary>Below the trigger, extending toward the right.</summary>
    BottomSpanRight,

    /// <summary>Below the trigger.</summary>
    Bottom,

    /// <summary>To the right of the trigger, vertically centred.</summary>
    RightCenter,

    /// <summary>To the right of the trigger, extending toward the top.</summary>
    RightSpanTop,

    /// <summary>To the right of the trigger, extending toward the bottom.</summary>
    RightSpanBottom,

    /// <summary>To the right of the trigger.</summary>
    Right,

    /// <summary>Diagonally above and to the left of the trigger.</summary>
    TopLeft,

    /// <summary>Diagonally above and to the right of the trigger.</summary>
    TopRight,

    /// <summary>Diagonally below and to the left of the trigger.</summary>
    BottomLeft,

    /// <summary>Diagonally below and to the right of the trigger.</summary>
    BottomRight,
}

public static class PositionAreaExtensions
{
    extension(PositionArea area)
    {
        public string GetTailwindClassName()
        {
            return area switch
            {
                PositionArea.TopCenter => "anchored-top-center",
                PositionArea.TopSpanLeft => "anchored-top-span-left",
                PositionArea.TopSpanRight => "anchored-top-span-right",
                PositionArea.Top => "anchored-top",
                PositionArea.LeftCenter => "anchored-left-center",
                PositionArea.LeftSpanTop => "anchored-left-span-top",
                PositionArea.LeftSpanBottom => "anchored-left-span-bottom",
                PositionArea.Left => "anchored-left",
                PositionArea.BottomCenter => "anchored-bottom-center",
                PositionArea.BottomSpanLeft => "anchored-bottom-span-left",
                PositionArea.BottomSpanRight => "anchored-bottom-span-right",
                PositionArea.Bottom => "anchored-bottom",
                PositionArea.RightCenter => "anchored-right-center",
                PositionArea.RightSpanTop => "anchored-right-span-top",
                PositionArea.RightSpanBottom => "anchored-right-span-bottom",
                PositionArea.Right => "anchored-right",
                PositionArea.TopLeft => "anchored-top-left",
                PositionArea.TopRight => "anchored-top-right",
                PositionArea.BottomLeft => "anchored-bottom-left",
                PositionArea.BottomRight => "anchored-bottom-right",
                _ => string.Empty,
            };
        }
    }
}
