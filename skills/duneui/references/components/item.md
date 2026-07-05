---
component: Item
tags: [dui-item, dui-item-actions, dui-item-content, dui-item-description, dui-item-footer, dui-item-group, dui-item-header, dui-item-media, dui-item-separator, dui-item-title, dui-link-item]
generated: true
---

# Item

A flexible row for presenting content, combining media, a title, description, and actions.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-item>` | A flexible row for presenting content, combining media, a title, description, and actions. |
| `<dui-item-actions>` | The region of an item that holds action controls such as buttons, aligned to its trailing edge. |
| `<dui-item-content>` | The main content region of an item; typically wraps the title and description. |
| `<dui-item-description>` | The secondary descriptive text of an item, rendered beneath its title. |
| `<dui-item-footer>` | The footer region of an item, spanning its full width beneath the main content. |
| `<dui-item-group>` | A vertical list container that groups related items together. |
| `<dui-item-header>` | The header region of an item, spanning its full width above the main content. |
| `<dui-item-media>` | The leading media region of an item, holding an icon, image, or avatar. |
| `<dui-item-separator>` | A horizontal divider used to separate items within a group. |
| `<dui-item-title>` | The primary title text of an item. |
| `<dui-link-item>` | An item rendered as an anchor, making the entire row a clickable link. |

## Attributes

### `<dui-item>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ItemSize` | `Default` | `Default`, `Small`, `ExtraSmall` |
| `variant` | `ItemVariant` | `Default` | `Default`, `Outline`, `Muted` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-item-media>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `variant` | `ItemMediaVariant` | `Default` | `Default`, `Icon`, `Image` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-link-item>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ItemSize` | `Default` | `Default`, `Small`, `ExtraSmall` |
| `variant` | `ItemVariant` | `Default` | `Default`, `Outline`, `Muted` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Item/_Intro.cshtml`*

```razor
<dui-item variant="ItemVariant.Outline">
    <dui-item-content>
        <dui-item-title>Flight to Paris</dui-item-title>
        <dui-item-description>
            Departs 10:15, Gate A4
        </dui-item-description>
    </dui-item-content>
    <dui-item-actions>
        <dui-button variant="ButtonVariant.Outline" size="ButtonSize.Small">
            View Details
        </dui-button>
    </dui-item-actions>
</dui-item>
<dui-link-item variant="ItemVariant.Outline" size="ItemSize.Small" href="#">
    <dui-item-media>
        <dui-icon name="luggage" class="size-5"/>
    </dui-item-media>
    <dui-item-content>
        <dui-item-title>Baggage policy updated.</dui-item-title>
    </dui-item-content>
    <dui-item-actions>
        <dui-icon name="chevron-right" class="size-4"/>
    </dui-item-actions>
</dui-link-item>
```
