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

## Example

*From `Pages/Group/_Align.cshtml`*

```razor
<p>Stretch</p>
<dui-group align="GroupAlign.Stretch" class="font-mono text-sm leading-6 font-bold text-white bg-indigo-100 rounded">
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-4">01</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-12">02</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-8">03</div>
</dui-group>
<p>Start</p>
<dui-group align="GroupAlign.Start" class="font-mono text-sm leading-6 font-bold text-white bg-indigo-100 rounded">
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-4">01</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-12">02</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-8">03</div>
</dui-group>
<p>Center</p>
<dui-group align="GroupAlign.Center" class="font-mono text-sm leading-6 font-bold text-white bg-indigo-100 rounded">
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-4">01</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-12">02</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-8">03</div>
</dui-group>
<p>End</p>
<dui-group align="GroupAlign.End" class="font-mono text-sm leading-6 font-bold text-white bg-indigo-100 rounded">
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-4">01</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-12">02</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 py-8">03</div>
</dui-group>
<p>Baseline</p>
<dui-group align="GroupAlign.Baseline" class="font-mono text-sm leading-6 font-bold text-white bg-indigo-100 rounded">
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 pt-2 pb-6">01</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 pt-8 pb-12">02</div>
    <div class="flex flex-1 items-center justify-center rounded-lg bg-indigo-500 pt-12 pb-4">03</div>
</dui-group>
```
