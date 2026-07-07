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

## Examples

*From `Pages/ButtonGroup/_Basic.cshtml`*

```razor
<dui-button-group>
    <dui-button variant="ButtonVariant.Outline">
        Button
    </dui-button>
    <dui-button variant="ButtonVariant.Outline">
        Another Button
    </dui-button>
</dui-button-group>
```

*From `Pages/ButtonGroup/_WithInput.cshtml`*

```razor
<div class="flex flex-col gap-4">
    <dui-button-group>
        <dui-button variant="ButtonVariant.Outline">Button</dui-button>
        <dui-input placeholder="Type something here..." />
    </dui-button-group>
    <dui-button-group>
        <dui-input placeholder="Type something here..." />
        <dui-button variant="ButtonVariant.Outline">Button</dui-button>
    </dui-button-group>
</div>
```
