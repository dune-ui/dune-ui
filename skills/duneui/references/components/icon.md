---
component: Icon
tags: [dui-icon]
generated: true
---

# Icon

Renders an SVG icon from the active icon pack by name.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `name` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Icon/_Intro.cshtml`*

```razor
<dui-icon name="rocket"/>
<dui-icon name="message-circle-heart"/>
<dui-icon name="banana"/>
<dui-icon name="timer"/>
```
