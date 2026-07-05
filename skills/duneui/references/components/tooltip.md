---
component: Tooltip
tags: [dui-tooltip]
generated: true
---

# Tooltip

A small floating label that appears when the user hovers or focuses a trigger element, rendered as a native hint popover.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `position` | `PositionArea` | `Top` | `TopCenter`, `TopSpanLeft`, `TopSpanRight`, `Top`, `LeftCenter`, `LeftSpanTop`, `LeftSpanBottom`, `Left`, `BottomCenter`, `BottomSpanLeft`, `BottomSpanRight`, `Bottom`, `RightCenter`, `RightSpanTop`, `RightSpanBottom`, `Right`, `TopLeft`, `TopRight`, `BottomLeft`, `BottomRight` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Tooltip/_Intro.cshtml`*

```razor
<dui-button variant="ButtonVariant.Outline" interestfor="--tooltip-intro">
    <dui-icon name="calendar-plus"/>
    Add to Calendar
</dui-button>
<dui-tooltip id="--tooltip-intro">
    Save your flight or hotel dates to your calendar. 
</dui-tooltip>
```
