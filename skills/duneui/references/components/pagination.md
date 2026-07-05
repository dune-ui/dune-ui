---
component: Pagination
tags: [dui-pagination, dui-pagination-content, dui-pagination-ellipsis, dui-pagination-first, dui-pagination-item, dui-pagination-last, dui-pagination-link, dui-pagination-next, dui-pagination-previous]
generated: true
---

# Pagination

Navigation for moving between pages of content.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-pagination>` | Navigation for moving between pages of content. |
| `<dui-pagination-content>` | The list that holds the individual pagination items. |
| `<dui-pagination-ellipsis>` | A non-interactive item that indicates omitted pages within the pagination. |
| `<dui-pagination-first>` | A pagination link that navigates to the first page. |
| `<dui-pagination-item>` | A single item within the pagination list. |
| `<dui-pagination-last>` | A pagination link that navigates to the last page. |
| `<dui-pagination-link>` | A link to a specific page within the pagination. |
| `<dui-pagination-next>` | A pagination link that navigates to the next page. |
| `<dui-pagination-previous>` | A pagination link that navigates to the previous page. |

## Attributes

### `<dui-pagination-first>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ButtonSize` | — | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-pagination-last>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ButtonSize` | — | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-pagination-link>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `is-active` | `bool` | `false` | `true`, `false` |
| `size` | `ButtonSize` | `Default` | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-pagination-next>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ButtonSize` | — | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-pagination-previous>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ButtonSize` | — | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Pagination/_Intro.cshtml`*

```razor
<dui-pagination>
    <dui-pagination-content>
        <dui-pagination-item>
            <dui-pagination-first href="#"/>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-previous href="#"/>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-link href="#">1</dui-pagination-link>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-link href="#" is-active="true">2</dui-pagination-link>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-link href="#">3</dui-pagination-link>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-ellipsis/>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-link href="#">10</dui-pagination-link>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-link href="#">11</dui-pagination-link>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-next href="#"/>
        </dui-pagination-item>
        <dui-pagination-item>
            <dui-pagination-last href="#"/>
        </dui-pagination-item>
    </dui-pagination-content>
</dui-pagination>
```
