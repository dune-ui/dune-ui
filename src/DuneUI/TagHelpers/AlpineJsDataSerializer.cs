using System.Text;

namespace DuneUI.TagHelpers;

internal static class AlpineJsDataSerializer
{
    public static string SerializeDataFunction(string functionName, object[] parameters)
    {
        var sb = new StringBuilder();
        sb.Append(functionName);
        sb.Append("(");

        for (var i = 0; i < parameters.Length; i++)
        {
            var rawValue = parameters[i];

            if (i > 0)
            {
                sb.Append(", ");
            }

            switch (rawValue)
            {
                case string s:
                    sb.Append("'");
                    sb.Append(s);
                    sb.Append("'");
                    break;
                case bool b:
                    sb.Append(b ? "true" : "false");
                    break;
                default:
                    sb.Append(rawValue);
                    break;
            }
        }
        sb.Append(")");

        return sb.ToString();
    }

    public static string SerializePopupPosition(PopupSide side, PopupAlign align)
    {
        return (side, align) switch
        {
            (PopupSide.Top, PopupAlign.Start) => "top-start",
            (PopupSide.Top, PopupAlign.Center) => "top",
            (PopupSide.Top, PopupAlign.End) => "top-end",
            (PopupSide.Right, PopupAlign.Start) => "right-start",
            (PopupSide.Right, PopupAlign.Center) => "right",
            (PopupSide.Right, PopupAlign.End) => "right-end",
            (PopupSide.Bottom, PopupAlign.Start) => "bottom-start",
            (PopupSide.Bottom, PopupAlign.Center) => "bottom",
            (PopupSide.Bottom, PopupAlign.End) => "bottom-end",
            (PopupSide.Left, PopupAlign.Start) => "left-start",
            (PopupSide.Left, PopupAlign.Center) => "left",
            (PopupSide.Left, PopupAlign.End) => "left-end",
            _ => "bottom",
        };
    }
}
