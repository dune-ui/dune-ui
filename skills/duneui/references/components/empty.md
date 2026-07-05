---
component: Empty
tags: [dui-empty, dui-empty-content, dui-empty-description, dui-empty-header, dui-empty-media, dui-empty-title]
generated: true
---

# Empty

An empty-state container that communicates the absence of content, composed of a header, media, title, description, and content subcomponents.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-empty>` | An empty-state container that communicates the absence of content, composed of a header, media, title, description, and content subcomponents. |
| `<dui-empty-content>` | The content region of an empty state; typically contains actions or supplementary elements below the header. |
| `<dui-empty-description>` | A line of muted descriptive text within an empty state header. |
| `<dui-empty-header>` | The header region of an empty state; typically contains the media, title, and description. |
| `<dui-empty-media>` | The media region of an empty state, displaying an icon or illustration above the title. |
| `<dui-empty-title>` | The title text within an empty state header. |

## Attributes

### `<dui-empty-media>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `variant` | `EmptyMediaVariant` | `Default` | `Default`, `Icon` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Empty/_Intro.cshtml`*

```razor
<dui-empty>
    <dui-empty-header>
        <dui-empty-media variant="EmptyMediaVariant.Icon">
            <dui-icon name="search"/>
        </dui-empty-media>
        <dui-empty-title>No Destinations Match Your Search</dui-empty-title>
        <dui-empty-description>
            We couldn't find any flights, hotels, or packages matching your filters. Try adjusting your dates,
            increasing your search radius, or selecting a different airport.
        </dui-empty-description>
    </dui-empty-header>
    <dui-empty-content>
        <dui-button variant="ButtonVariant.Outline">
            <dui-icon name="funnel-x"/>
            Reset Search Filters
        </dui-button>
    </dui-empty-content>
</dui-empty>
```
