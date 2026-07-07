---
component: Layout
tags: [dui-group, dui-container, dui-stack]
generated: true
---

# Layout

A horizontal flex layout that arranges its children in a row, with configurable alignment, spacing, and justification.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-group>` | A horizontal flex layout that arranges its children in a row, with configurable alignment, spacing, and justification. |
| `<dui-container>` | A centered, width-constrained wrapper that horizontally centers page content and applies responsive horizontal padding. |
| `<dui-stack>` | A vertical flex layout that arranges its children in a column, with configurable alignment, spacing, and justification. |

## Attributes

### `<dui-group>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `align` | `GroupAlign` | `Start` | `Stretch`, `Center`, `Start`, `End`, `Baseline` |
| `gap` | `GroupGap` | `Default` | `ExtraSmall`, `Small`, `Default`, `Large`, `ExtraLarge` |
| `justify` | `GroupJustify` | `Start` | `Center`, `Start`, `End`, `SpaceBetween`, `SpaceAround` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-stack>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `align` | `StackAlign` | `Stretch` | `Stretch`, `Center`, `Start`, `End` |
| `gap` | `StackGap` | `Default` | `ExtraSmall`, `Small`, `Default`, `Large`, `ExtraLarge` |
| `justify` | `StackJustify` | `Start` | `Center`, `Start`, `End`, `SpaceBetween`, `SpaceAround` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Examples

*From `Pages/Container/_Intro.cshtml`*

```razor
<dui-container class="rounded-lg border bg-muted py-8 text-center">
    <h2 class="text-lg font-semibold">Plan your next journey</h2>
    <p class="text-sm text-muted-foreground">
        This content sits inside a container — horizontally centered, width-constrained, with
        responsive horizontal padding.
    </p>
</dui-container>
```

*From `Pages/Container/_PageLayout.cshtml`*

```razor
<dui-container>
    <dui-stack gap="StackGap.Large">
        <dui-group justify="GroupJustify.SpaceBetween" align="GroupAlign.Center">
            <h1 class="text-xl font-semibold">Destinations</h1>
            <dui-button>
                <dui-icon name="plus"/>
                Add destination
            </dui-button>
        </dui-group>
        <p class="text-sm text-muted-foreground">
            Use a container to center and constrain your page, then Stack and Group handle the
            vertical and horizontal rhythm of the content inside it.
        </p>
    </dui-stack>
</dui-container>
```
