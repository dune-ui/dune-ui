---
component: Textarea
tags: [dui-textarea]
generated: true
---

# Textarea

A styled multi-line text input that grows with its content. Supports model binding via `asp-for`.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Textarea/_Intro.cshtml`*

```razor
<dui-field>
    <dui-field-label>Describe your experience</dui-field-label>
    <dui-textarea
        placeholder="The view from the balcony was breathtaking, but the breakfast service was a bit slow..."/>
    <dui-field-description>
        Your feedback helps other travelers make better choices. Be as descriptive as possible!
    </dui-field-description>
</dui-field>
```
