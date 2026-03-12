namespace DuneUI.TagHelpers;

public enum PositionArea
{
    TopCenter,
    TopSpanLeft,
    TopSpanRight,
    Top,
    LeftCenter,
    LeftSpanTop,
    LeftSpanBottom,
    Left,
    BottomCenter,
    BottomSpanLeft,
    BottomSpanRight,
    Bottom,
    RightCenter,
    RightSpanTop,
    RightSpanBottom,
    Right,
    TopLeft,
    TopRight,
    BottomLeft,
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
                _ => throw new ArgumentOutOfRangeException(nameof(area), area, null),
            };
        }
    }
}
