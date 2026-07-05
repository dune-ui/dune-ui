---
component: ButtonGroup
tags: [dui-button-group, dui-button-group-separator, dui-button-group-text]
generated: true
---

# ButtonGroup

Groups related buttons together as a single visual unit.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-button-group>` | Groups related buttons together as a single visual unit. |
| `<dui-button-group-separator>` | Renders a divider between items within a button group. |
| `<dui-button-group-text>` | Renders a non-interactive text label within a button group. |

## Attributes

### `<dui-button-group>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `orientation` | `ButtonGroupOrientation` | `Horizontal` | `Horizontal`, `Vertical` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-button-group-separator>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `orientation` | `SeparatorOrientation` | `Vertical` | `Horizontal`, `Vertical` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/ButtonGroup/_Intro.cshtml`*

```razor
<dui-button-group>
    <dui-button-group>
        <dui-button variant="ButtonVariant.Outline" size="ButtonSize.Icon">
            <dui-icon name="arrow-left"/>
        </dui-button>
    </dui-button-group>
    <dui-button-group>
        <dui-button variant="ButtonVariant.Outline">Review</dui-button>
        <dui-button variant="ButtonVariant.Outline">Book Again</dui-button>
    </dui-button-group>
    <dui-button-group>
        <dui-button variant="ButtonVariant.Outline">
            <dui-icon name="mail"/>
            Email Hotel
        </dui-button>
    </dui-button-group>
</dui-button-group>
```
