---
component: Card
tags: [dui-card, dui-card-action, dui-card-content, dui-card-description, dui-card-footer, dui-card-header, dui-card-title]
generated: true
---

# Card

A flexible container that groups related content, composed of a header, title, description, content, footer, and action subcomponents.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-card>` | A flexible container that groups related content, composed of a header, title, description, content, footer, and action subcomponents. |
| `<dui-card-action>` | An action region within a card header, aligned to the top-right corner; typically contains a button or other interactive control. |
| `<dui-card-content>` | The main content region of a card. |
| `<dui-card-description>` | A secondary line of muted text within a card header, describing the card's contents. |
| `<dui-card-footer>` | The footer region of a card; typically contains actions or supplementary information. |
| `<dui-card-header>` | The header region of a card; typically contains the title, description, and action. |
| `<dui-card-title>` | The title text within a card header. |

## Attributes

### `<dui-card>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `CardSize` | `Default` | `Default`, `Small` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Card/_Intro.cshtml`*

```razor
<dui-card class="relative mx-auto w-full max-w-sm pt-0">
    <div class="absolute inset-0 z-30 aspect-video"></div>
    <img
        src="https://images.unsplash.com/photo-1563492065599-3520f775eeed?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=2274"
        alt="Photo by cartayen on Unsplash"
        title="Photo by cartayen on Unsplash"
        class="relative z-20 aspect-video w-full object-cover"
    />
    <dui-card-header>
        <dui-card-title>Bangkok, Thailand</dui-card-title>
        <dui-card-description>
            Bangkok is an exhilarating metropolis where ancient, gilded temples stand in vibrant contrast to modern
            skyscrapers, world-class street food, and electrifying nightlife.
        </dui-card-description>
    </dui-card-header>
    <dui-card-footer>
        <dui-button variant="ButtonVariant.Outline" class="w-full">
            <dui-icon name="bookmark"/>
            Bookmark
        </dui-button>
    </dui-card-footer>
</dui-card>
```
