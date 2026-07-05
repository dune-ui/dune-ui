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

*From `Pages/Skeleton/_Intro.cshtml`*

```razor
<dui-card class="w-full">
    <dui-card-header>
        <dui-skeleton class="h-4 w-2/3"/>
        <dui-skeleton class="h-4 w-1/2"/>
    </dui-card-header>
    <dui-card-content>
        <dui-skeleton class="aspect-square w-full"/>
    </dui-card-content>
</dui-card>
```
