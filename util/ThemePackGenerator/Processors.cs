using System.Text.RegularExpressions;

namespace ThemePackGenerator;

public static partial class Processors
{
    [GeneratedRegex(@"(\s)(?<dark>dark:)?(aria\-invalid:)(?<class>\S+)")]
    public static partial Regex AriaInvalidClassRegex();

    [GeneratedRegex(@"\saria-invalid:ring-(\[)?[1-9](px\])?")]
    public static partial Regex AriaInvalidRingRegex();

    [GeneratedRegex(@"duration-(\w+)\s?")]
    public static partial Regex DurationRegex();

    [GeneratedRegex(@"(data-\[slot=checkbox-group\]:)(?<class>\S+)")]
    public static partial Regex FieldGroupCheckboxGroupRegex();

    [GeneratedRegex(@"(\w+-)?flex(-\w+)?\s?")]
    public static partial Regex FlexRegex();

    [GeneratedRegex(@"(?<!group-)data-\[size=(?<size>default|sm)\]:")]
    public static partial Regex SwitchTrackSizeRegex();

    [GeneratedRegex(@"grid(\s)?")]
    public static partial Regex GridRegex();

    [GeneratedRegex(@"data-((open)|(closed)|(\[state=delayed-open])|(\[side=\w+])):\S+\s?")]
    public static partial Regex TooltipContentDataAnimationClasses();

    [GeneratedRegex(@"data-((open)|(closed)|(\[state=delayed-open])|(\[side=\w+])):\S+\s?")]
    public static partial Regex PopoverContentDataAnimationClasses();

    extension(Dictionary<string, string> input)
    {
        /// <summary>
        ///     dui-field-group contains a rule for data-slot=checkbox-group which tightens the gap between checkboxes.
        ///     We want to duplicate this rule for data-slot=radio-group so radio group spacing is also tightened up.
        /// </summary>
        public Dictionary<string, string> AddFieldRadioGroupSupport()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-field-group", out var fieldGroupClass))
            {
                foreach (Match match in FieldGroupCheckboxGroupRegex().Matches(fieldGroupClass))
                {
                    fieldGroupClass += $" data-[slot=radio-group]:{match.Groups["class"].Value}";
                }

                // Store the updated
                output["dui-field-group"] = fieldGroupClass;
            }

            return output;
        }

        /// <summary>
        ///     Shadcn using the aria-invalid attribute to indicate errors. We want to make copies of all those classes
        ///     and create a version that depends on whether the .input-validation-error class is added (which is what
        ///     is added by ASP.NET Core validation).
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> CreateInputValidationErrorClassesFromAriaInvalid()
        {
            var output = new Dictionary<string, string>();

            foreach (var (key, value) in input)
            {
                var newValue = value;

                foreach (Match match in AriaInvalidClassRegex().Matches(value))
                {
                    newValue +=
                        $" {match.Groups["dark"].Value}[&.input-validation-error]:{match.Groups["class"].Value}";
                }

                output.Add(key, newValue);
            }

            return output;
        }

        /// <summary>
        ///     Creates dui-radiobutton* styles based on the existing radio-group-item* styles that exists
        ///     in shadcn
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> CreateRadioButtonStyles()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-radio-group-item", out var radioGroupItem))
            {
                output["dui-radiobutton"] = radioGroupItem;
            }

            if (output.TryGetValue("dui-radio-group-indicator", out var radioGroupIndicator))
            {
                output["dui-radiobutton-indicator"] = radioGroupIndicator;
            }

            if (
                output.TryGetValue(
                    "dui-radio-group-indicator-icon",
                    out var radioGroupIndicatorIcon
                )
            )
            {
                output["dui-radiobutton-indicator-icon"] = radioGroupIndicatorIcon;
            }

            return output;
        }

        /// <summary>
        ///     Remove the ring around inputs that are in an error state. The ring is confusing as it is the same
        ///     ring used when an input has focus, so the presence of this ring makes it difficult to see when an
        ///     input that is in error state has input focus
        /// </summary>
        public Dictionary<string, string> RemoveAriaInvalidRing()
        {
            var output = new Dictionary<string, string>();

            foreach (var (key, value) in input)
            {
                output.Add(key, AriaInvalidRingRegex().Replace(value, string.Empty));
            }

            return output;
        }

        public Dictionary<string, string> CleanDialogClasses()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-dialog-content", out var classes))
            {
                // The native dialog element controls it visibility by itself. The grid utility will force it to be
                // always visible, so we add it only when the data-open attribute is present (i.e. the dialog is open)
                classes = classes.Replace("grid", "data-open:grid");

                output["dui-dialog-content"] = classes;
            }

            return output;
        }

        /// <summary>
        ///     Shadcn popover animations were specified using a number of data classes. Since we are using the native
        ///     popover APIs, animations are declared differently and these are not required anymore.
        /// </summary>
        public Dictionary<string, string> CleanPopoverClasses()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-popover-content", out var classes))
            {
                classes = PopoverContentDataAnimationClasses().Replace(classes, string.Empty);
                classes = FlexRegex().Replace(classes, string.Empty);
                classes = DurationRegex().Replace(classes, string.Empty);

                output["dui-popover-content"] = classes;
            }

            return output;
        }

        public Dictionary<string, string> CleanSheetClasses()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-sheet-content", out var classes))
            {
                classes = classes.Replace("z-50 flex flex-col ", string.Empty);

                output["dui-sheet-content"] = classes;
            }

            return output;
        }

        /// <summary>
        ///     Shadcn tooltip animations were specified using a number of data classes. Since we are using the native
        ///     popover APIs, animations are declared differently and these are not required anymore.
        /// </summary>
        public Dictionary<string, string> CleanTooltipClasses()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-tooltip-content", out var classes))
            {
                classes = TooltipContentDataAnimationClasses().Replace(classes, string.Empty);
                classes = FlexRegex().Replace(classes, string.Empty);
                classes = DurationRegex().Replace(classes, string.Empty);

                output["dui-tooltip-content"] = classes;
            }

            return output;
        }

        /// <summary>
        ///     dui-checkbox uses the data-checked attribute to style the checked state since that is what is
        ///     being used by BaseUI. but since we use the native checkbox we should use the standard checked
        ///     pseudo class instead.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ReplaceDuiCheckboxDataChecked()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-checkbox", out var classes))
            {
                output["dui-checkbox"] = classes.Replace("data-checked:", "checked:");
            }

            return output;
        }

        /// <summary>
        ///     dui-checkbox uses the data-checked attribute to style the checked state since that is what is
        ///     being used by BaseUI. but since we use the native checkbox we should use the standard checked
        ///     pseudo class instead.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ReplaceDuiRadioGroupItemDataChecked()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-radio-group-item", out var classes))
            {
                output["dui-radio-group-item"] = classes.Replace("data-checked:", "checked:");
            }

            return output;
        }

        /// <summary>
        ///     dui-switch uses the data-checked/data-unchecked attributes to style the on/off state since that is
        ///     what is being used by BaseUI. Since we render a native checkbox as the switch track (so the value
        ///     posts back), we drive the state purely from CSS instead:
        ///     <list type="bullet">
        ///         <item>
        ///             The track (dui-switch) is the &lt;input&gt; itself, so it reacts to its own
        ///             <c>checked</c> pseudo class. The unchecked styles become the default (the prefix is dropped).
        ///             It also sizes off the wrapper's <c>data-size</c> (group/switch) since the input does not
        ///             carry the size attribute.
        ///         </item>
        ///         <item>
        ///             The thumb (dui-switch-thumb) is rendered as a sibling after the peer &lt;input&gt;, so it
        ///             reacts via <c>peer-checked</c>.
        ///         </item>
        ///     </list>
        /// </summary>
        public Dictionary<string, string> CleanSwitchClasses()
        {
            var output = new Dictionary<string, string>(input);

            if (output.TryGetValue("dui-switch", out var trackClasses))
            {
                trackClasses = trackClasses.Replace("data-unchecked:", string.Empty);
                trackClasses = trackClasses.Replace("data-checked:", "checked:");
                trackClasses = SwitchTrackSizeRegex()
                    .Replace(trackClasses, "group-data-[size=${size}]/switch:");

                output["dui-switch"] = trackClasses;
            }

            if (output.TryGetValue("dui-switch-thumb", out var thumbClasses))
            {
                thumbClasses = thumbClasses.Replace("data-unchecked:", string.Empty);
                thumbClasses = thumbClasses.Replace("data-checked:", "peer-checked:");

                output["dui-switch-thumb"] = thumbClasses;
            }

            return output;
        }

        /// <summary>
        ///     Shadcn drives the toggle on/off state from the element itself (<c>aria-pressed</c> for a
        ///     single toggle, <c>data-[state=on]</c> for a toggle group item) and expects focus/validation
        ///     styles on that same element. DuneUI renders a toggle as a &lt;label&gt; wrapping an
        ///     <c>sr-only</c> native &lt;input&gt; (so the value posts back with no JavaScript), which means
        ///     the checked/focus/validation state lives on a descendant. We rewrite those prefixes to the
        ///     <c>has-*</c> forms so the wrapping label reacts to its inner input.
        /// </summary>
        public Dictionary<string, string> CleanToggleClasses()
        {
            var output = new Dictionary<string, string>(input);

            static string Adapt(string classes) =>
                classes
                    .Replace("aria-pressed:", "has-[:checked]:")
                    .Replace("data-[state=on]:", "has-[:checked]:")
                    .Replace("focus-visible:", "has-[:focus-visible]:")
                    .Replace("[&.input-validation-error]:", "has-[.input-validation-error]:")
                    .Replace("aria-invalid:", "has-[[aria-invalid]]:");

            if (output.TryGetValue("dui-toggle", out var toggleClasses))
            {
                output["dui-toggle"] = Adapt(toggleClasses);
            }

            if (output.TryGetValue("dui-toggle-group-item", out var itemClasses))
            {
                output["dui-toggle-group-item"] = Adapt(itemClasses);
            }

            return output;
        }
    }
}
