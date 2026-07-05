---
component: Button
tags: [dui-button, dui-linkbutton]
generated: true
---

# Button

Renders a button element for triggering actions.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-button>` | Renders a button element for triggering actions. |
| `<dui-linkbutton>` | Renders an anchor element styled as a button, with routing support. |

## Attributes

### `<dui-button>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ButtonSize` | `Default` | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `variant` | `ButtonVariant` | `Default` | `Default`, `Destructive`, `Outline`, `Secondary`, `Ghost`, `Link` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-linkbutton>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ButtonSize` | `Default` | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `variant` | `ButtonVariant` | `Default` | `Default`, `Destructive`, `Outline`, `Secondary`, `Ghost`, `Link` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Button/_Intro.cshtml`*

```razor
<dui-button variant="ButtonVariant.Outline">
    <dui-icon name="undo-2"/>
    Cancel Changes
</dui-button>
<dui-button>Save Changes</dui-button>
```
