using DocsSamples.Pages.Field;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages;

public class DocsStatic : PageModel
{
    public string? Layout { get; set; }

    public object? PartialModel { get; set; }

    public string PartialName { get; set; }

    public void OnGet(string name, string? layout)
    {
        Layout = layout;
        PartialName = name;

        PartialModel = name switch
        {
            "Checkbox/_GroupModelBinding" => new Checkbox.Index.GroupModel(),
            "Checkbox/_ModelBinding" => new Checkbox.Index.ModelBindingModel(),
            "Checkbox/_Validation" => new Checkbox.Index.ValidationModel(),
            "Field/_CheckboxImplicit" => new CheckboxImplicitModel
            {
                MustSyncDesktopFolders = true,
            },
            "Field/_FieldGroupImplicit" => new FieldGroupImplicitModel
            {
                EnableResponsePushNotifications = true,
            },
            "Field/_FieldsetImplicit" => new FieldsetImplicitModel(),
            "Field/_Implicit" => new ImplicitModel(),
            "Field/_InputImplicit" => new InputImplicitModel(),
            "Field/_RadioImplicit" => new RadioImplicitModel { SubscriptionType = "yearly" },
            "Field/_SelectImplicit" => new SelectImplicitModel { Department = "design" },
            "Field/_TextareaImplicit" => new TextareaImplicitModel(),
            "Input/_ModelBinding" => new Pages.Input.Index.ModelBindingModel(),
            "Input/_InputTypesModelBinding" => new Pages.Input.Index.InputTypesModelBindingModel(),
            "Input/_Validation" => new Input.Index.ValidationModel(),
            "InputOtp/_ModelBinding" => new Pages.InputOtp.Index.ModelBindingModel(),
            "InputOtp/_Validation" => new Pages.InputOtp.Index.ValidationModel(),
            "Progress/_FileUploadList" => Pages.Progress.Index.Files,
            "Radio/_ModelBinding" => new Radio.Index.ModelBindingModel(),
            "Radio/_Validation" => new Radio.Index.ValidationModel(),
            "Select/_ModelBinding" => new Select.Index.BookingFormModel(),
            "Select/_Validation" => new Select.Index.BookingFormValidationModel(),
            "Slider/_ModelBinding" => new Slider.Index.ModelBindingModel(),
            "Switch/_ModelBinding" => new Switch.Index.ModelBindingModel(),
            "Textarea/_ModelBinding" => new Textarea.Index.ReviewModel(),
            "Textarea/_Validation" => new Textarea.Index.ReviewValidationModel(),
            "Toggle/_ModelBinding" => new Toggle.Index.ModelBindingModel(),
            "ToggleGroup/_ModelBinding" => new ToggleGroup.Index.ModelBindingModel(),
            _ => null,
        };

        // DocsStatic renders the partial with the "PartialModel" prefix (see DocsStatic.cshtml),
        // so validation errors must be keyed to the fully-qualified field name to attach to the input.
        switch (name)
        {
            case "Checkbox/_Validation":
                ModelState.AddModelError(
                    "PartialModel.AcceptTerms",
                    "You must accept the terms and conditions"
                );
                break;
            case "Input/_Validation":
                ModelState.AddModelError("PartialModel.Email", "Enter your email address");
                break;
            case "InputOtp/_Validation":
                ModelState.AddModelError(
                    "PartialModel.OneTimePassword",
                    "Enter the complete 6-digit code"
                );
                break;
            case "Radio/_Validation":
                ModelState.AddModelError("PartialModel.BedType", "Please select a bed type");
                break;
            case "Select/_Validation":
                ModelState.AddModelError(
                    "PartialModel.CabinClass",
                    "Please select a valid cabin class"
                );
                break;
            case "Textarea/_Validation":
                ModelState.AddModelError(
                    "PartialModel.Review",
                    "Please leave a review for other travelers"
                );
                break;
        }
    }
}
