using DocsSamples.Pages.Field;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocsSamples.Pages;

public class DocsStatic : PageModel
{
    public object? Model { get; set; }

    public string PartialName { get; set; }

    public void OnGet(string name)
    {
        PartialName = name;

        Model = name switch
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
            "Progress/_FileUploadList" => Pages.Progress.Index.Files,
            "Radio/_ModelBinding" => new Radio.Index.ModelBindingModel(),
            "Radio/_Validation" => new Radio.Index.ValidationModel(),
            "Select/_ModelBinding" => new Select.Index.BookingFormModel(),
            "Select/_Validation" => new Select.Index.BookingFormValidationModel(),
            "Textarea/_ModelBinding" => new Textarea.Index.ReviewModel(),
            "Textarea/_Validation" => new Textarea.Index.ReviewValidationModel(),
            _ => null,
        };

        switch (name)
        {
            case "Checkbox/_Validation":
                ModelState.AddModelError(
                    "Model.AcceptTerms",
                    "You must accept the terms and conditions"
                );
                break;
            case "Input/_Validation":
                ModelState.AddModelError("Model.Email", "Enter your email address");
                break;
            case "Radio/_Validation":
                ModelState.AddModelError("Model.BedType", "Please select a bed type");
                break;
            case "Select/_Validation":
                ModelState.AddModelError("Model.CabinClass", "Please select a valid cabin class");
                break;
            case "Textarea/_Validation":
                ModelState.AddModelError(
                    "Model.Review",
                    "Please leave a review for other travelers"
                );
                break;
        }
    }
}
