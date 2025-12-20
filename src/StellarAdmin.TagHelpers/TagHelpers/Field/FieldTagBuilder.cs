using Microsoft.AspNetCore.Mvc.Rendering;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

internal class FieldTagBuilder : TagBuilder
{
    private static readonly Dictionary<FieldOrientation, ClassElement[]> OrientationClasses = new()
    {
        [FieldOrientation.Vertical] =
        [
            new ComponentName("dui-field-orientation-vertical"),
            "flex-col [&>*]:w-full [&>.sr-only]:w-auto",
        ],
        [FieldOrientation.Horizontal] =
        [
            new ComponentName("dui-field-orientation-horizontal"),
            "flex-row items-center [&>[data-slot=field-label]]:flex-auto has-[>[data-slot=field-content]]:items-start has-[>[data-slot=field-content]]:[&>[role=checkbox],[role=radio]]:mt-px",
        ],
        [FieldOrientation.Responsive] =
        [
            new ComponentName("dui-field-orientation-responsive"),
            "flex-col [&>*]:w-full [&>.sr-only]:w-auto @md/field-group:flex-row @md/field-group:items-center @md/field-group:[&>*]:w-auto @md/field-group:[&>[data-slot=field-label]]:flex-auto @md/field-group:has-[>[data-slot=field-content]]:items-start @md/field-group:has-[>[data-slot=field-content]]:[&>[role=checkbox],[role=radio]]:mt-px",
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
        Attributes.Add("data-orientation", orientation.GetDataAttributeText());
        Attributes.Add(
            "class",
            classMerger.Merge(
                new ClassElement[] { new ComponentName("dui-field"), "group/field flex w-full" }
                    .Union(OrientationClasses[orientation])
                    .Append(userSuppliedClass)
                    .ToArray()
            )
        );
    }
}
