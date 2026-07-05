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

## Example

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
