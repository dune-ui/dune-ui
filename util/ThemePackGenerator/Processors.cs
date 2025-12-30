using System.Text.RegularExpressions;

namespace ThemePackGenerator;

public static partial class Processors
{
    [GeneratedRegex(@"\saria-invalid:ring-(\[)?[1-9](px\])?")]
    public static partial Regex AriaInvalidRingRegex();

    [GeneratedRegex(@"(\s)(?<dark>dark:)?(aria\-invalid:)(?<class>\S+)")]
    public static partial Regex AriaInvalidClassRegex();

    [GeneratedRegex(@"(data-\[slot=checkbox-group\]:)(?<class>\S+)")]
    public static partial Regex FieldGroupCheckboxGroupRegex();

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

        /// <summary>
        /// Shadcn using the aria-invalid attribute to indicate errors. We want to make copies of all those classes
        /// and create a version that depends on whether the .input-validation-error class is added (which is what
        /// is added by ASP.NET Core validation).
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
                    newValue += $" {match.Groups["dark"].Value}[&.input-validation-error]:{match.Groups["class"].Value}";
                }
                
                output.Add(key, newValue);
            }

            return output;
        }

        /// <summary>
        /// dui-checkbox uses the data-checked attribute to style the checked state since that is what is
        /// being used by BaseUI. but since we use the native checkbox we should use the standard checked
        /// pseudo class instead. 
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
        /// dui-checkbox uses the data-checked attribute to style the checked state since that is what is
        /// being used by BaseUI. but since we use the native checkbox we should use the standard checked
        /// pseudo class instead. 
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
        /// Creates dui-radiobutton* styles based on the existing radio-group-item* styles that exists
        /// in shadcn
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
            if (output.TryGetValue("dui-radio-group-indicator-icon", out var radioGroupIndicatorIcon))
            {
                output["dui-radiobutton-indicator-icon"] = radioGroupIndicatorIcon;
            }

            return output;
        }
    }
}