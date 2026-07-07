---
component: Skeleton
tags: [dui-skeleton]
generated: true
---

# Skeleton

A placeholder that shows an animated pulsing shape while content is loading.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Skeleton/_Form.cshtml`*

```razor
<div class="flex w-full flex-col gap-7">
    <div class="flex flex-col gap-3">
        <dui-skeleton class="h-4 w-20"/>
        <dui-skeleton class="h-10 w-full"/>
    </div>
    <div class="flex flex-col gap-3">
        <dui-skeleton class="h-4 w-24"/>
        <dui-skeleton class="h-10 w-full"/>
    </div>
    <dui-skeleton class="h-9 w-24"/>
</div>
```
