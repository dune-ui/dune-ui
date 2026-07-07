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

## Examples

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

*From `Pages/Tooltip/_Elements.cshtml`*

```razor
<dui-button variant="ButtonVariant.Outline" interestfor="--tooltip-elements-button">
    Button
</dui-button>
<dui-linkbutton variant="ButtonVariant.Outline" interestfor="--tooltip-elements-linkbutton">
    Link Button
</dui-linkbutton>
<dui-input interestfor="--tooltip-elements-input"/>
<dui-textarea interestfor="--tooltip-elements-textarea"></dui-textarea>
<dui-select interestfor="--tooltip-elements-select">
    <option>Item 1</option>
    <option>Item 2</option>
</dui-select>
<dui-input type="radio" interestfor="--tooltip-elements-radio"/>
<dui-input type="checkbox" interestfor="--tooltip-elements-checkbox"/>
<dui-tooltip id="--tooltip-elements-button">
    This is a tooltip for the button
</dui-tooltip>
<dui-tooltip id="--tooltip-elements-linkbutton">
    This is a tooltip for the link button
</dui-tooltip>
<dui-tooltip id="--tooltip-elements-input">
    This is a tooltip for the input
</dui-tooltip>
<dui-tooltip id="--tooltip-elements-textarea">
    This is a tooltip for the textarea
</dui-tooltip>
<dui-tooltip id="--tooltip-elements-select">
    This is a tooltip for the select
</dui-tooltip>
<dui-tooltip id="--tooltip-elements-radio">
    This is a tooltip for the radio button
</dui-tooltip>
<dui-tooltip id="--tooltip-elements-checkbox">
    This is a tooltip for the checkbox
</dui-tooltip>
```
