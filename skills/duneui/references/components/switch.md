---
component: Switch
tags: [dui-switch]
generated: true
---

# Switch

A toggle control that switches between on and off states. Backed by a native checkbox so its value model-binds and posts back like any other checkbox, with the visuals driven entirely by CSS.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `form` | `string` | — | — |
| `size` | `SwitchSize` | `Default` | `Default`, `Small` |
| `value` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Switch/_Intro.cshtml`*

```razor
<dui-field orientation="FieldOrientation.Horizontal">
    <dui-switch id="intro-notifications" checked/>
    <dui-field-content>
        <dui-field-label for="intro-notifications">Email notifications</dui-field-label>
        <dui-field-description>
            Receive emails about your account activity.
        </dui-field-description>
    </dui-field-content>
</dui-field>
```
