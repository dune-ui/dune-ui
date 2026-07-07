---
component: Slider
tags: [dui-slider]
generated: true
---

# Slider

An input for selecting a numeric value, or a range of values, by dragging one or more thumbs along a track.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `min` | `int` | — | — |
| `max` | `int` | — | — |
| `step` | `int` | — | — |
| `min-distance` | `int` | — | — |
| `orientation` | `SliderOrientation` | `Horizontal` | `Horizontal`, `Vertical` |
| `thumb-alignment` | `SliderThumbAlignment` | `Edge` | `Center`, `Edge` |
| `disabled` | `bool` | `false` | `true`, `false` |
| `value` | `string` | — | — |
| `form` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Examples

*From `Pages/Slider/_Range.cshtml`*

```razor
<dui-slider value="20,80" min="0" max="100" min-distance="10"/>
```

*From `Pages/Slider/_ModelBinding.cshtml`*

```razor
<dui-slider asp-for="MaximumDistanceFromCenter" max="100"/>
<dui-slider asp-for="PricePerNight" min="0" max="1000" step="50"/>
<dui-slider asp-for="GuestRatingBands" min="0" max="100" min-distance="5"/>
```
