---
component: Select
tags: [dui-select]
generated: true
---

# Select

A styled dropdown for choosing a single option, wrapping a native `<select>` element with a custom chevron icon.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `asp-items` | `IEnumerable<SelectListItem>` | — | — |
| `size` | `SelectSize` | `Default` | `Default`, `Small` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Select/_Intro.cshtml`*

```razor
<dui-select>
    <option value="">-- Select cabin class --</option>
    <option value="economy">Economy</option>
    <option value="premium-economy">Premium Economy</option>
    <option value="business">Business</option>
    <option value="first">First</option>
</dui-select>
```
