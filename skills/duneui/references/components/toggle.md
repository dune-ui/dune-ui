---
component: Toggle
tags: [dui-toggle]
generated: true
---

# Toggle

Renders a two-state button that can be toggled on or off.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `form` | `string` | — | — |
| `variant` | `ToggleVariant` | `Default` | `Default`, `Outline` |
| `size` | `ToggleSize` | `Default` | `Default`, `Small`, `Large` |
| `value` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Examples

*From `Pages/Toggle/_Intro.cshtml`*

```razor
<dui-toggle aria-label="Save to wishlist">
    <dui-icon name="heart"/>
</dui-toggle>
```

*From `Pages/Toggle/_WithText.cshtml`*

```razor
<dui-toggle variant="ToggleVariant.Outline" aria-label="Free Wi-Fi">
    <dui-icon name="wifi"/>
    Free Wi-Fi
</dui-toggle>
```
