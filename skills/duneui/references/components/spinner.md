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

*From `Pages/Spinner/_Intro.cshtml`*

```razor
<div class="w-md">
    <dui-item variant="ItemVariant.Muted">
        <dui-item-media>
            <dui-spinner/>
        </dui-item-media>
        <dui-item-content>
            <dui-item-title class="line-clamp-1">Processing payment...</dui-item-title>
        </dui-item-content>
        <dui-item-content class="flex-none justify-end">
            <span class="text-sm tabular-nums">$100.00</span>
        </dui-item-content>
    </dui-item>
</div>
```
