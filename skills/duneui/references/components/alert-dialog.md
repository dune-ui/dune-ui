---
component: AlertDialog
tags: [dui-alert-dialog, dui-alert-dialog-action, dui-alert-dialog-cancel, dui-alert-dialog-description, dui-alert-dialog-footer, dui-alert-dialog-header, dui-alert-dialog-media, dui-alert-dialog-title]
generated: true
---

# AlertDialog

A modal dialog that interrupts the user to confirm an important action, rendered over a native `<dialog>` element. Open and close it with the Invoker Commands API — a trigger button carrying `commandfor` and `command="show-modal"` or `command="close"`. Unlike a regular dialog, it is not dismissed by clicking the backdrop.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-alert-dialog>` | A modal dialog that interrupts the user to confirm an important action, rendered over a native `<dialog>` element. Open and close it with the Invoker Commands API — a trigger button carrying `commandfor` and `command="show-modal"` or `command="close"`. Unlike a regular dialog, it is not dismissed by clicking the backdrop. |
| `<dui-alert-dialog-action>` | The confirming button of an alert dialog. Renders a styled submit button with `value="confirm"` so a wrapping `<form method="dialog">` closes the dialog with that `returnValue` (which the `duneui.alertDialog` helper reads as `confirmed: true`). Override `variant` for a destructive action. |
| `<dui-alert-dialog-cancel>` | The dismissing button of an alert dialog. Renders a styled outline submit button with `value="cancel"` so a wrapping `<form method="dialog">` closes the dialog with that `returnValue` (which the `duneui.alertDialog` helper reads as `confirmed: false`). |
| `<dui-alert-dialog-description>` | The descriptive body text of an alert dialog, shown beneath the title. |
| `<dui-alert-dialog-footer>` | The footer region of an alert dialog; typically contains the cancel and action buttons. |
| `<dui-alert-dialog-header>` | The header region of an alert dialog; typically contains the title and description. |
| `<dui-alert-dialog-media>` | A region within an alert dialog for media such as an icon or illustration. |
| `<dui-alert-dialog-title>` | The title heading of an alert dialog. |

## Attributes

### `<dui-alert-dialog>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `AlertDialogSize` | `Default` | `Default`, `Small` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-alert-dialog-action>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `variant` | `ButtonVariant` | `Default` | `Default`, `Destructive`, `Outline`, `Secondary`, `Ghost`, `Link` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-alert-dialog-cancel>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `variant` | `ButtonVariant` | `Outline` | `Default`, `Destructive`, `Outline`, `Secondary`, `Ghost`, `Link` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/AlertDialog/_Intro.cshtml`*

```razor
<div class="flex justify-center">
    <dui-button variant="ButtonVariant.Outline" commandfor="--alert-dialog-intro" command="show-modal">
        Show Dialog
    </dui-button>
</div>
<dui-alert-dialog id="--alert-dialog-intro">
    <dui-alert-dialog-header>
        <dui-alert-dialog-title>Discard unsaved changes?</dui-alert-dialog-title>
        <dui-alert-dialog-description>
            You have unsaved changes to your itinerary for Trip #TRV-987. If you continue, your edits will be lost.
        </dui-alert-dialog-description>
    </dui-alert-dialog-header>
    <form method="dialog">
        <dui-alert-dialog-footer>
            <dui-alert-dialog-cancel>Keep editing</dui-alert-dialog-cancel>
            <dui-alert-dialog-action>Continue</dui-alert-dialog-action>
        </dui-alert-dialog-footer>
    </form>
</dui-alert-dialog>
```
