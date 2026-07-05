---
component: Input
tags: [dui-input]
generated: true
---

# Input

A form input. Renders a styled `<input>` for text-like types, and a styled checkbox or radio button (with its indicator) when the type is `checkbox` or `radio`. Supports model binding via `asp-for`.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `form` | `string` | — | — |
| `asp-format` | `string` | — | — |
| `type` | `string` | — | — |
| `value` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Input/_Intro.cshtml`*

```razor
<dui-input placeholder="Enter your email address" type="email"/>
```
