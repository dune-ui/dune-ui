---
component: DropdownMenu
tags: [dui-dropdown-menu, dui-dropdown-menu-checkbox-item, dui-dropdown-menu-content, dui-dropdown-menu-group, dui-dropdown-menu-item, dui-dropdown-menu-label, dui-dropdown-menu-radio-group, dui-dropdown-menu-radio-item, dui-dropdown-menu-separator, dui-dropdown-menu-shortcut, dui-dropdown-menu-sub, dui-dropdown-menu-sub-content, dui-dropdown-menu-sub-trigger, dui-dropdown-menu-trigger]
generated: true
---

# DropdownMenu

The root of a dropdown menu, pairing a trigger with its content and generating the shared id that links them.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-dropdown-menu>` | The root of a dropdown menu, pairing a trigger with its content and generating the shared id that links them. |
| `<dui-dropdown-menu-checkbox-item>` | A menu item with a checkable state, rendered as a `<div role="menuitemcheckbox">` with a check indicator. |
| `<dui-dropdown-menu-content>` | The popover panel that holds a dropdown menu's items, positioned relative to its trigger. |
| `<dui-dropdown-menu-group>` | Groups related menu items together, exposed to assistive technology as a `role="group"`. |
| `<dui-dropdown-menu-item>` | A selectable menu item. Renders as a `<div role="menuitem">`, or as an `<a role="menuitem">` when the author supplies a URL — either a raw `href` or ASP.NET routing attributes (`asp-page`, `asp-action`/`asp-controller`, `asp-route-*`, …). The `del-dropdown-menu` web component activates items by `role`, so both elements behave identically. |
| `<dui-dropdown-menu-label>` | A non-interactive label used to caption a section of menu items. |
| `<dui-dropdown-menu-radio-group>` | Groups `dui-dropdown-menu-radio-item` children into a single-selection set and tracks which value is selected. |
| `<dui-dropdown-menu-radio-item>` | A single option within a `dui-dropdown-menu-radio-group`, rendered as a `<div role="menuitemradio">` with a selection indicator. |
| `<dui-dropdown-menu-separator>` | A horizontal divider that visually separates groups of menu items. |
| `<dui-dropdown-menu-shortcut>` | Displays a keyboard shortcut hint, aligned to the trailing edge of a menu item. |
| `<dui-dropdown-menu-sub>` | Wraps a submenu, pairing a `dui-dropdown-menu-sub-trigger` with its `dui-dropdown-menu-sub-content` and generating the shared id that links them. |
| `<dui-dropdown-menu-sub-content>` | The popover panel that holds a submenu's items, positioned relative to its sub-trigger. |
| `<dui-dropdown-menu-sub-trigger>` | The menu item that opens a submenu, rendered with a trailing chevron and wired to its sub-content panel. |
| `<dui-dropdown-menu-trigger>` | The button that toggles a dropdown menu and anchors its content, styled with button variant and size options. |

## Attributes

### `<dui-dropdown-menu-checkbox-item>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `checked` | `bool` | `false` | `true`, `false` |
| `close-on-click` | `bool` | — | `true`, `false` |
| `disabled` | `bool` | — | `true`, `false` |
| `inset` | `bool` | — | `true`, `false` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-dropdown-menu-content>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `position` | `PositionArea` | `BottomSpanRight` | `TopCenter`, `TopSpanLeft`, `TopSpanRight`, `Top`, `LeftCenter`, `LeftSpanTop`, `LeftSpanBottom`, `Left`, `BottomCenter`, `BottomSpanLeft`, `BottomSpanRight`, `Bottom`, `RightCenter`, `RightSpanTop`, `RightSpanBottom`, `Right`, `TopLeft`, `TopRight`, `BottomLeft`, `BottomRight` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-dropdown-menu-item>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `close-on-click` | `bool` | — | `true`, `false` |
| `disabled` | `bool` | — | `true`, `false` |
| `href` | `string` | — | — |
| `inset` | `bool` | — | `true`, `false` |
| `variant` | `DropdownMenuItemVariant` | `Default` | `Default`, `Destructive` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-dropdown-menu-label>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `inset` | `bool` | — | `true`, `false` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-dropdown-menu-radio-group>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `value` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-dropdown-menu-radio-item>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `close-on-click` | `bool` | — | `true`, `false` |
| `disabled` | `bool` | — | `true`, `false` |
| `value` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-dropdown-menu-sub-content>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `position` | `PositionArea` | `RightSpanBottom` | `TopCenter`, `TopSpanLeft`, `TopSpanRight`, `Top`, `LeftCenter`, `LeftSpanTop`, `LeftSpanBottom`, `Left`, `BottomCenter`, `BottomSpanLeft`, `BottomSpanRight`, `Bottom`, `RightCenter`, `RightSpanTop`, `RightSpanBottom`, `Right`, `TopLeft`, `TopRight`, `BottomLeft`, `BottomRight` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-dropdown-menu-sub-trigger>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `inset` | `bool` | — | `true`, `false` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-dropdown-menu-trigger>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `ButtonSize` | `Default` | `Default`, `ExtraSmall`, `Small`, `Large`, `Icon`, `IconExtraSmall`, `IconSmall`, `IconLarge` |
| `variant` | `ButtonVariant` | `Outline` | `Default`, `Destructive`, `Outline`, `Secondary`, `Ghost`, `Link` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Examples

*From `Pages/DropdownMenu/_Intro.cshtml`*

```razor
<dui-dropdown-menu>
    <dui-dropdown-menu-trigger variant="ButtonVariant.Outline">
        <dui-icon name="circle-user-round" class="text-muted-foreground"/>
        Ibn Battuta
    </dui-dropdown-menu-trigger>
    <dui-dropdown-menu-content class="w-56">
        <dui-dropdown-menu-label>My Account</dui-dropdown-menu-label>
        <dui-dropdown-menu-separator/>
        <dui-dropdown-menu-group>
            <dui-dropdown-menu-item>
                <dui-icon name="user"/>
                Profile
                <dui-dropdown-menu-shortcut>⇧⌘P</dui-dropdown-menu-shortcut>
            </dui-dropdown-menu-item>
            <dui-dropdown-menu-item>
                <dui-icon name="luggage"/>
                My Bookings
                <dui-dropdown-menu-shortcut>⌘B</dui-dropdown-menu-shortcut>
            </dui-dropdown-menu-item>
            <dui-dropdown-menu-item>
                <dui-icon name="settings"/>
                Settings
                <dui-dropdown-menu-shortcut>⌘,</dui-dropdown-menu-shortcut>
            </dui-dropdown-menu-item>
        </dui-dropdown-menu-group>
        <dui-dropdown-menu-separator/>
        <dui-dropdown-menu-item variant="DropdownMenuItemVariant.Destructive">
            <dui-icon name="log-out"/>
            Log out
            <dui-dropdown-menu-shortcut>⇧⌘Q</dui-dropdown-menu-shortcut>
        </dui-dropdown-menu-item>
    </dui-dropdown-menu-content>
</dui-dropdown-menu>
```

*From `Pages/DropdownMenu/_CheckboxItems.cshtml`*

```razor
<dui-dropdown-menu>
    <dui-dropdown-menu-trigger variant="ButtonVariant.Outline">
        <dui-icon name="sliders-horizontal" class="text-muted-foreground"/>
        Trip Filters
    </dui-dropdown-menu-trigger>
    <dui-dropdown-menu-content class="w-56">
        <dui-dropdown-menu-label>Show Categories</dui-dropdown-menu-label>
        <dui-dropdown-menu-separator/>
        <dui-dropdown-menu-checkbox-item checked="true">Flights</dui-dropdown-menu-checkbox-item>
        <dui-dropdown-menu-checkbox-item checked="true">Accommodation</dui-dropdown-menu-checkbox-item>
        <dui-dropdown-menu-checkbox-item>Car Rental</dui-dropdown-menu-checkbox-item>
    </dui-dropdown-menu-content>
</dui-dropdown-menu>
```

*From `Pages/DropdownMenu/_Submenu.cshtml`*

```razor
<dui-dropdown-menu>
    <dui-dropdown-menu-trigger variant="ButtonVariant.Outline">
        <dui-icon name="ellipsis" class="text-muted-foreground"/>
        Trip Actions
    </dui-dropdown-menu-trigger>
    <dui-dropdown-menu-content class="w-56">
        <dui-dropdown-menu-item>
            <dui-icon name="ticket"/>
            View Tickets
        </dui-dropdown-menu-item>
        <dui-dropdown-menu-item>
            <dui-icon name="calendar-plus"/>
            Add to Calendar
        </dui-dropdown-menu-item>
        <dui-dropdown-menu-separator/>
        <dui-dropdown-menu-sub>
            <dui-dropdown-menu-sub-trigger>
                <dui-icon name="map-pin"/>
                Add Destination
            </dui-dropdown-menu-sub-trigger>
            <dui-dropdown-menu-sub-content class="w-44">
                <dui-dropdown-menu-item>Paris</dui-dropdown-menu-item>
                <dui-dropdown-menu-item>Bangkok</dui-dropdown-menu-item>
                <dui-dropdown-menu-item>Kyoto</dui-dropdown-menu-item>
                <dui-dropdown-menu-item>Cape Town</dui-dropdown-menu-item>
            </dui-dropdown-menu-sub-content>
        </dui-dropdown-menu-sub>
        <dui-dropdown-menu-separator/>
        <dui-dropdown-menu-item variant="DropdownMenuItemVariant.Destructive">
            <dui-icon name="trash-2"/>
            Cancel Trip
        </dui-dropdown-menu-item>
    </dui-dropdown-menu-content>
</dui-dropdown-menu>
```
