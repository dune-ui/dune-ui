---
component: Tabs
tags: [dui-tab-link, dui-tab-list]
generated: true
---

# Tabs

A single tab within a `<dui-tab-list>`, rendered as a link to its target view.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-tab-link>` | A single tab within a `<dui-tab-list>`, rendered as a link to its target view. |
| `<dui-tab-list>` | A list of tabs, each linking to a different view or page. |

## Attributes

### `<dui-tab-link>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `disabled` | `bool` | `false` | `true`, `false` |
| `is-active` | `bool` | — | `true`, `false` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-tab-list>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `orientation` | `TabListOrientation` | `Horizontal` | `Horizontal`, `Vertical` |
| `variant` | `TabListVariant` | `Default` | `Default`, `Line` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Tabs/_Intro.cshtml`*

```razor
<dui-tab-list>
    <dui-tab-link href="#">Flights</dui-tab-link>
    <dui-tab-link href="#">Accommodation</dui-tab-link>
    <dui-tab-link href="#">Car Rental</dui-tab-link>
</dui-tab-list>
```
