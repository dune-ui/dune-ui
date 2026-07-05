---
component: Avatar
tags: [dui-avatar, dui-avatar-badge, dui-avatar-group, dui-avatar-group-count]
generated: true
---

# Avatar

Displays a user's image, falling back to initials or a name-derived monogram when no image is available.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-avatar>` | Displays a user's image, falling back to initials or a name-derived monogram when no image is available. |
| `<dui-avatar-badge>` | A small badge overlaid on the corner of an avatar, such as a status indicator or icon. |
| `<dui-avatar-group>` | A container that displays a set of avatars as an overlapping stack. |
| `<dui-avatar-group-count>` | A trailing element within an avatar group that displays the count of additional, unshown avatars. |

## Attributes

### `<dui-avatar>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `initials` | `string` | — | — |
| `name` | `string` | — | — |
| `size` | `AvatarSize` | `Default` | `Default`, `Small`, `Large` |
| `src` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Avatar/_Intro.cshtml`*

```razor
<dui-avatar src="/avatars/avatar-1.jpg"/>
<dui-avatar-group>
    <dui-avatar src="/avatars/avatar-1.jpg"/>
    <dui-avatar src="/avatars/avatar-2.jpg"/>
    <dui-avatar src="/avatars/avatar-3.jpg"/>
</dui-avatar-group>
```
