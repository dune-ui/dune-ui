---
component: Breadcrumb
tags: [dui-breadcrumb, dui-breadcrumb-ellipsis, dui-breadcrumb-item, dui-breadcrumb-link, dui-breadcrumb-list, dui-breadcrumb-page, dui-breadcrumb-separator]
generated: true
---

# Breadcrumb

A breadcrumb navigation trail, rendered as a `<nav>`; shows the path to the current page. Compose it with the list, item, link, page, separator, and ellipsis subcomponents.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-breadcrumb>` | A breadcrumb navigation trail, rendered as a `<nav>`; shows the path to the current page. Compose it with the list, item, link, page, separator, and ellipsis subcomponents. |
| `<dui-breadcrumb-ellipsis>` | An ellipsis that stands in for collapsed breadcrumb items, rendered as a presentational `<span>` with an icon and screen-reader text. |
| `<dui-breadcrumb-item>` | A single item within the breadcrumb trail, rendered as a `<li>`; wraps a link, page, or separator. |
| `<dui-breadcrumb-link>` | A navigable link within a breadcrumb item, rendered as an `<a>`; supports the standard anchor routing attributes. |
| `<dui-breadcrumb-list>` | The ordered list of breadcrumb items, rendered as an `<ol>`. |
| `<dui-breadcrumb-page>` | The current page in the breadcrumb trail, rendered as a non-interactive `<span>` marked with `aria-current="page"`. |
| `<dui-breadcrumb-separator>` | A visual separator between breadcrumb items, rendered as a presentational `<li>`; defaults to a chevron icon when no content is supplied. |

## Examples

*From `Pages/Breadcrumb/_Intro.cshtml`*

```razor
<dui-breadcrumb>
    <dui-breadcrumb-list>
        <dui-breadcrumb-item>
            <dui-breadcrumb-link href="#">Home</dui-breadcrumb-link>
        </dui-breadcrumb-item>
        <dui-breadcrumb-separator/>
        <dui-breadcrumb-item>
            <dui-breadcrumb-link href="#">Europe</dui-breadcrumb-link>
        </dui-breadcrumb-item>
        <dui-breadcrumb-separator/>
        <dui-breadcrumb-item>
            <dui-breadcrumb-link href="#">Italy</dui-breadcrumb-link>
        </dui-breadcrumb-item>
        <dui-breadcrumb-separator/>
        <dui-breadcrumb-page>Grand Hotel Venice</dui-breadcrumb-page>
    </dui-breadcrumb-list>
</dui-breadcrumb>
```

*From `Pages/Breadcrumb/_Collapsed.cshtml`*

```razor
<dui-breadcrumb>
    <dui-breadcrumb-list>
        <dui-breadcrumb-item>
            <dui-breadcrumb-link href="#">Home</dui-breadcrumb-link>
        </dui-breadcrumb-item>
        <dui-breadcrumb-separator/>
        <dui-breadcrumb-item>
            <dui-breadcrumb-ellipsis />
        </dui-breadcrumb-item>
        <dui-breadcrumb-separator/>
        <dui-breadcrumb-item>
            <dui-breadcrumb-link href="#">Trip #TRV-987</dui-breadcrumb-link>
        </dui-breadcrumb-item>
        <dui-breadcrumb-separator/>
        <dui-breadcrumb-page>Add Traveler Details</dui-breadcrumb-page>
    </dui-breadcrumb-list>
</dui-breadcrumb>
```
