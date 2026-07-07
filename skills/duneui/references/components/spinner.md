---
component: Spinner
tags: [dui-spinner]
generated: true
---

# Spinner

An animated spinning icon that indicates a loading or busy state.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Spinner/_InButtons.cshtml`*

```razor
<div class="flex flex-wrap items-center gap-4">
    <dui-button>
        <dui-spinner/>
        Submit
    </dui-button>
    <dui-button disabled>
        <dui-spinner/>
        Disabled
    </dui-button>
    <dui-button variant="ButtonVariant.Outline" disabled>
        <dui-spinner/>
        Outline
    </dui-button>
    <dui-button variant="ButtonVariant.Outline" size="ButtonSize.Icon" disabled>
        <dui-spinner/>
        <span class="sr-only">Loading...</span>
    </dui-button>
</div>
```
