---
component: Progress
tags: [dui-progress, dui-progress-label, dui-progress-value]
generated: true
---

# Progress

A progress bar that visualizes the completion of a task as a filled track. Compose it with the label and value subcomponents.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-progress>` | A progress bar that visualizes the completion of a task as a filled track. Compose it with the label and value subcomponents. |
| `<dui-progress-label>` | A text label for a progress bar, rendered as a `<span>`. |
| `<dui-progress-value>` | Displays a progress bar's value, rendered as a `<span>`; falls back to the computed completion percentage when no content is supplied. |

## Attributes

### `<dui-progress>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `maximum` | `int` | `100` | — |
| `minimum` | `int` | `0` | — |
| `value` | `int` | `0` | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Examples

*From `Pages/Progress/_Intro.cshtml`*

```razor
<div class="flex w-full flex-col gap-4">
    <dui-progress value="0"/>
    <dui-progress value="25"/>
    <dui-progress value="50"/>
    <dui-progress value="75"/>
    <dui-progress value="100"/>
</div>
```

*From `Pages/Progress/_WithLabel.cshtml`*

```razor
<dui-progress value="56">
    <dui-progress-label>Upload progress</dui-progress-label>
    <dui-progress-value/>
</dui-progress>
```
