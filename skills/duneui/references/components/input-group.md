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

## Example

*From `Pages/InputGroup/_Intro.cshtml`*

```razor
<dui-input-group>
  <dui-input-group-input placeholder="Search..."/>
  <dui-input-group-addon>
    <dui-icon name="search"/>
  </dui-input-group-addon>
  <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">12 results</dui-input-group-addon>
</dui-input-group>
<dui-input-group>
  <dui-input-group-input placeholder="example.com" class="!pl-1"/>
  <dui-input-group-addon>
    <dui-input-group-text>https://</dui-input-group-text>
  </dui-input-group-addon>
  <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">
    <dui-input-group-button class="rounded-full" size="InputGroupButtonSize.IconExtraSmall">
      <dui-icon name="info"/>
    </dui-input-group-button>
  </dui-input-group-addon>
</dui-input-group>
<dui-input-group>
  <dui-input-group-textarea placeholder="Ask, Search or Chat..."/>
  <dui-input-group-addon align="InputGroupAddOnVariantAlignment.BlockEnd">
    <dui-input-group-button
      variant="ButtonVariant.Outline"
      class="rounded-full"
      size="InputGroupButtonSize.IconExtraSmall"
    >
      <dui-icon name="plus"/>
    </dui-input-group-button>
    <dui-input-group-text class="ml-auto">52% used</dui-input-group-text>
    <dui-separator orientation="SeparatorOrientation.Vertical" class="!h-4"/>
    <dui-input-group-button
      variant="ButtonVariant.Default"
      class="rounded-full"
      size="InputGroupButtonSize.IconExtraSmall"
    >
      <dui-icon name="arrow-up"/>
      <span class="sr-only">Send</span>
    </dui-input-group-button>
  </dui-input-group-addon>
</dui-input-group>
<dui-input-group>
  <dui-input-group-input placeholder="@@duneui"/>
  <dui-input-group-addon align="InputGroupAddOnVariantAlignment.InlineEnd">
    <div class="bg-primary text-primary-foreground flex size-4 items-center justify-center rounded-full">
      <dui-icon name="check" class="size-3"/>
    </div>
  </dui-input-group-addon>
</dui-input-group>
```
