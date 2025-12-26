using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkSelectTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-select")]
public class SelectTagHelper : FieldInputBaseTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly IIconManager _iconManager;
    private FrameworkSelectTagHelper? _frameworkTagHelper;

    public SelectTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, htmlGenerator, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _iconManager = iconManager;
    }

    [HtmlAttributeName("asp-items")]
    public IEnumerable<SelectListItem>? Items { get; set; }

    [HtmlAttributeName("size")]
    public SelectSize? Size { get; set; }

    public override void Init(TagHelperContext context)
    {
        base.Init(context);

        if (For != null || Items != null)
        {
            _frameworkTagHelper = new FrameworkSelectTagHelper(_htmlGenerator)
            {
                For = For,
                Items = Items,
                ViewContext = ViewContext,
                Name = Name,
            };
            _frameworkTagHelper.Init(context);
        }
    }

    protected override async Task<AutoFieldLayout> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        var effectiveSize = Size ?? SelectSize.Default;
        var userSuppliedClass = output.GetUserSuppliedClass();

        /*
         * Render the select
         */
        var selectOutput = new TagHelperOutput(
            "select",
            new TagHelperAttributeList(
                output
                    .Attributes.Where(a =>
                        !a.Name.Equals("class", StringComparison.OrdinalIgnoreCase)
                    )
                    .Union(
                        [
                            new TagHelperAttribute("data-slot", "native-select"),
                            new TagHelperAttribute(
                                "data-size",
                                effectiveSize.GetDataAttributeText()
                            ),
                            new TagHelperAttribute(
                                "class",
                                ClassMerger.Merge(
                                    new ComponentName("dui-native-select"),
                                    "outline-none disabled:pointer-events-none disabled:cursor-not-allowed"
                                )
                            ),
                        ]
                    )
            ),
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        if ((For != null || Items != null) && _frameworkTagHelper != null)
        {
            _frameworkTagHelper.Process(context, selectOutput);
            selectOutput.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
        else
        {
            selectOutput.Content.SetHtmlContent(await output.GetChildContentAsync());
        }
        output.Content.AppendHtml(selectOutput);

        /*
         * Render the icon
         */
        var iconTagHelperOutput = new TagHelperOutput(
            string.Empty,
            [
                new TagHelperAttribute(
                    "class",
                    ClassMerger.Merge(
                        new ComponentName("dui-native-select-icon"),
                        "pointer-events-none absolute select-none"
                    )
                ),
                new TagHelperAttribute("aria-hidden", "true"),
                new TagHelperAttribute("data-slot", "native-select-icon"),
            ],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
        {
            Name = "chevron-down",
        };
        await iconTagHelper.ProcessAsync(context, iconTagHelperOutput);

        output.Content.AppendHtml(iconTagHelperOutput);

        /*
         * Finally, render the wrapper
         */
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "native-select-wrapper");
        output.Attributes.Add("data-size", effectiveSize.GetDataAttributeText());
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-native-select-wrapper"),
                "group/native-select relative w-fit has-[select:disabled]:opacity-50",
                userSuppliedClass
            )
        );

        return AutoFieldLayout.Vertical;
    }
}
