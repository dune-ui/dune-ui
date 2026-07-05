---
component: Badge
tags: [dui-badge]
generated: true
---

# Badge

A small label used to highlight status, counts, or categories.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `variant` | `BadgeVariant` | `Default` | `Default`, `Secondary`, `Destructive`, `Outline`, `Ghost`, `Link` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Badge/_Intro.cshtml`*

```razor
<dui-badge variant="BadgeVariant.Default">
    <dui-icon name="plane"/>
    Flight Booked
</dui-badge>
<dui-badge variant="BadgeVariant.Secondary">Requires Visa</dui-badge>
<dui-badge variant="BadgeVariant.Destructive">
    <dui-icon name="flame"/>
    Overbooked
</dui-badge>
```
