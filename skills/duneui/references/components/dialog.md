---
component: Dialog
tags: [dui-dialog, dui-dialog-description, dui-dialog-footer, dui-dialog-header, dui-dialog-title]
generated: true
---

# Dialog

A modal window overlaid on the page, rendered over a native `<dialog>` element. Open and close it with the Invoker Commands API — a trigger button carrying `commandfor` and `command="show-modal"` or `command="close"`.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-dialog>` | A modal window overlaid on the page, rendered over a native `<dialog>` element. Open and close it with the Invoker Commands API — a trigger button carrying `commandfor` and `command="show-modal"` or `command="close"`. |
| `<dui-dialog-description>` | The descriptive body text of a dialog, shown beneath the title. |
| `<dui-dialog-footer>` | The footer region of a dialog; typically contains action buttons. |
| `<dui-dialog-header>` | The header region of a dialog; typically contains the title and description. |
| `<dui-dialog-title>` | The title heading of a dialog. |

## Examples

*From `Pages/Dialog/_Intro.cshtml`*

```razor
<div class="flex justify-center">
    <dui-button variant="ButtonVariant.Outline" commandfor="--dialog-intro" command="show-modal">
        Open
    </dui-button>
</div>
<dui-dialog id="--dialog-intro">
    <dui-dialog-header>
        <dui-dialog-title>Edit profile</dui-dialog-title>
        <dui-dialog-description>Make changes to your profile here. Click save when you're done.</dui-dialog-description>
    </dui-dialog-header>
    <dui-field-group>
        <dui-field>
            <dui-label for="--dialog-intro-name">Name</dui-label>
            <dui-input id="--dialog-intro-name" name="name" defaultValue="Ibn Battuta"/>
        </dui-field>
        <dui-field>
            <dui-label for="--dialog-intro-username">Username</dui-label>
            <dui-input id="--dialog-intro-username" name="username" defaultValue="@@ibnbattuta"/>
        </dui-field>
    </dui-field-group>
    <dui-dialog-footer>
        <dui-button variant="ButtonVariant.Outline" commandfor="--dialog-intro" command="close">
            Cancel
        </dui-button>
        <dui-button commandfor="--dialog-intro" command="close">
            Save Changes
        </dui-button>
    </dui-dialog-footer>
</dui-dialog>
```

*From `Pages/Dialog/_ReturnValue.cshtml`*

```razor
<div class="flex justify-center">
    <dui-button variant="ButtonVariant.Outline" commandfor="--dialog-return-value" command="show-modal">
        Open Alert Dialog
    </dui-button>
</div>
<dui-dialog id="--dialog-return-value">
    <dui-dialog-header>
        <dui-dialog-title>Are you absolutely sure?</dui-dialog-title>
        <dui-dialog-description>
            This action cannot be undone. This will permanently delete your account from our servers.
        </dui-dialog-description>
    </dui-dialog-header>
    <form method="dialog">
        <dui-dialog-footer>
            <dui-button variant="ButtonVariant.Outline" type="submit" value="cancel">
                Cancel
            </dui-button>
            <dui-button type="submit" value="confirm" autofocus>
                Continue
            </dui-button>
        </dui-dialog-footer>
    </form>
</dui-dialog>
<script>
    (function() {
        const dialog = document.getElementById("--dialog-return-value");

        dialog.addEventListener("close", () => {
            const cancelled = dialog.returnValue === "" || dialog.returnValue === "cancel";
            if (cancelled) {
                alert("The action has been cancelled");
                return;
            }

            alert("The action has been confirmed");
        });
        dialog.addEventListener("toggle", (e) => {
            // Reset the return value every time the dialog opens to prevent a previous
            // returnValue from being returned when pressing the Esc key
            if (e.newState === "open") {
                dialog.returnValue = "";
            }
        });
    })();
</script>
```
