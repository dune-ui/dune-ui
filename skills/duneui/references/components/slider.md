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

## Example

*From `Pages/Slider/_Intro.cshtml`*

```razor
<dui-slider value="50" max="100"/>
```
