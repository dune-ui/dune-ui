using Microsoft.AspNetCore.Mvc.Rendering;

namespace StellarAdmin.TagHelpers;

internal class FieldTagBuilder : TagBuilder
{
    private static readonly Dictionary<FieldOrientation, string[]> OrientationClasses = new()
    {
        [FieldOrientation.Vertical] = ["flex-col [&>*]:w-full [&>.sr-only]:w-auto"],
        [FieldOrientation.Horizontal] =
        [
            "flex-row items-center",
            "[&>[data-slot=field-label]]:flex-auto",
            "has-[>[data-slot=field-content]]:items-start has-[>[data-slot=field-content]]:[&>[role=checkbox],[role=radio]]:mt-px",
        ],
        [FieldOrientation.Responsive] =
        [
            "flex-col [&>*]:w-full [&>.sr-only]:w-auto @md/field-group:flex-row @md/field-group:items-center @md/field-group:[&>*]:w-auto",
            "@md/field-group:[&>[data-slot=field-label]]:flex-auto",
            "@md/field-group:has-[>[data-slot=field-content]]:items-start @md/field-group:has-[>[data-slot=field-content]]:[&>[role=checkbox],[role=radio]]:mt-px",
        ],
    };

    public FieldTagBuilder(
        ICssClassMerger classMerger,
        FieldOrientation orientation,
        string? userSuppliedClass
    )
        : base("div")
    {
        Attributes.Add("data-slot", "field");
        Attributes.Add("data-orientation", GetOrientationAttributeText(orientation));
        Attributes.Add(
            "class",
            classMerger.Merge(
                new[] { "group/field flex w-full gap-3 data-[invalid=true]:text-destructive" }
                    .Concat(OrientationClasses[orientation])
                    .Append(userSuppliedClass)
                    .ToArray()
            )
        );
    }

    private string GetOrientationAttributeText(FieldOrientation orientation) =>
        orientation switch
        {
            FieldOrientation.Vertical => "vertical",
            FieldOrientation.Horizontal => "horizontal",
            FieldOrientation.Responsive => "responsive",
            _ => string.Empty,
        };
}
