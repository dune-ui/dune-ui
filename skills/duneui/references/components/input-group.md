---
component: InputGroup
tags: [dui-input-group, dui-input-group-addon, dui-input-group-button, dui-input-group-input, dui-input-group-text, dui-input-group-textarea]
generated: true
---

# InputGroup

A container that groups an input with add-ons, buttons, or text so they render as a single combined field.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-input-group>` | A container that groups an input with add-ons, buttons, or text so they render as a single combined field. |
| `<dui-input-group-addon>` | A decoration attached to an input group, such as an icon, text, or button, aligned to one of the input's edges. Clicking the add-on focuses the group's input. |
| `<dui-input-group-button>` | A button styled to sit inside an input group, typically within an add-on. |
| `<dui-input-group-input>` | A text input rendered inside an input group, styled to blend into the group so add-ons appear within a single field. |
| `<dui-input-group-text>` | A run of text or an icon displayed inside an input group, typically within an add-on. |
| `<dui-input-group-textarea>` | A multi-line text input rendered inside an input group, styled to blend into the group. |

## Attributes

### `<dui-input-group-addon>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `align` | `InputGroupAddOnVariantAlignment` | `InlineStart` | `InlineStart`, `InlineEnd`, `BlockStart`, `BlockEnd` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-input-group-button>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `InputGroupButtonSize` | `ExtraSmall` | `ExtraSmall`, `Small`, `IconExtraSmall`, `IconSmall` |
| `variant` | `ButtonVariant` | `Ghost` | `Default`, `Destructive`, `Outline`, `Secondary`, `Ghost`, `Link` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-input-group-input>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `asp-for` | `ModelExpression` | — | — |
| `asp-format` | `string` | — | — |
| `form` | `string` | — | — |
| `type` | `string` | — | — |
| `value` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-input-group-textarea>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `asp-for` | `ModelExpression` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Examples

*From `Pages/InputGroup/_Icons.cshtml`*

```razor
<dui-input-group>
    <dui-input-group-input placeholder="Search..."/>
    <dui-input-group-addon>
        <dui-icon name="search"/>
    </dui-input-group-addon>
</dui-input-group>
<dui-input-group>
    <dui-input-group-input type="email" placeholder="Enter your email"/>
    <dui-input-group-addon>
        <dui-icon name="mail"/>
    </dui-input-group-addon>
</dui-input-group>
<dui-input-group>
    <dui-input-group-input placeholder="Card number"/>
    <dui-input-group-addon>
        <dui-icon name="credit-card"/>
    </dui-input-group-addon>
    <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">
        <dui-icon name="check"/>
    </dui-input-group-addon>
</dui-input-group>
<dui-input-group>
    <dui-input-group-input placeholder="Card number"/>
    <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">
        <dui-icon name="star"/>
        <dui-icon name="info"/>
    </dui-input-group-addon>
</dui-input-group>
```

*From `Pages/InputGroup/_Buttons.cshtml`*

```razor
<dui-input-group>
    <dui-input-group-input placeholder="https://x.com/shadcn" readonly/>
    <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">
        <dui-input-group-button
            aria-label="Copy"
            title="Copy"
            size="InputGroupButtonSize.IconExtraSmall"
        >
            <dui-icon name="copy"/>
        </dui-input-group-button>
    </dui-input-group-addon>
</dui-input-group>
<dui-input-group class="[--radius:9999px]">
    <dui-input-group-addon class="text-muted-foreground pl-1.5">
        https://
    </dui-input-group-addon>
    <dui-input-group-input id="input-secure-19"/>
    <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">
        <dui-input-group-button
            size="InputGroupButtonSize.IconExtraSmall"
        >
            <dui-icon name="star"/>
        </dui-input-group-button>
    </dui-input-group-addon>
</dui-input-group>
<dui-input-group>
    <dui-input-group-input placeholder="Type to search..."/>
    <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">
        <dui-input-group-button variant="ButtonVariant.Secondary">Search</dui-input-group-button>
    </dui-input-group-addon>
</dui-input-group>
```
