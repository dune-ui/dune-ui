namespace DuneUI.TagHelpers.Sheet;

public enum SheetSide
{
    Top,
    Right,
    Bottom,
    Left,
}

public static class SheetSideExtensions
{
    extension(SheetSide side)
    {
        public string GetDataAttributeText() =>
            side switch
            {
                SheetSide.Top => "top",
                SheetSide.Right => "right",
                SheetSide.Bottom => "bottom",
                SheetSide.Left => "left",
                _ => string.Empty,
            };
    }
}
