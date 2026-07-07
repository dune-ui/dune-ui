---
component: ToggleGroup
tags: [dui-toggle-group, dui-toggle-group-item]
generated: true
---

# ToggleGroup

Groups a set of toggle items into a single-select or multi-select control.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-toggle-group>` | Groups a set of toggle items into a single-select or multi-select control. |
| `<dui-toggle-group-item>` | A single selectable item within a toggle group. |

## Attributes

### `<dui-toggle-group>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `type` | `ToggleGroupType` | `Single` | `Single`, `Multiple` |
| `variant` | `ToggleVariant` | `Default` | `Default`, `Outline` |
| `size` | `ToggleSize` | `Default` | `Default`, `Small`, `Large` |
| `orientation` | `ToggleGroupOrientation` | `Horizontal` | `Horizontal`, `Vertical` |
| `spacing` | `int` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-toggle-group-item>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `value` | `string` | — | — |
| `variant` | `ToggleVariant` | — | `Default`, `Outline` |
| `size` | `ToggleSize` | — | `Default`, `Small`, `Large` |
| `selected` | `bool` | — | `true`, `false` |
| `disabled` | `bool` | — | `true`, `false` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Examples

*From `Pages/ToggleGroup/_Intro.cshtml`*

```razor
<dui-toggle-group name="results-view" type="ToggleGroupType.Single">
    <dui-toggle-group-item value="list" selected="true" aria-label="List view">
        <dui-icon name="list"/>
    </dui-toggle-group-item>
    <dui-toggle-group-item value="grid" aria-label="Grid view">
        <dui-icon name="layout-grid"/>
    </dui-toggle-group-item>
    <dui-toggle-group-item value="map" aria-label="Map view">
        <dui-icon name="map"/>
    </dui-toggle-group-item>
</dui-toggle-group>
```

*From `Pages/ToggleGroup/_ModelBinding.cshtml`*

```razor
<dui-toggle-group asp-for="ResultsView" type="ToggleGroupType.Single" spacing="0" variant="ToggleVariant.Outline">
    <dui-toggle-group-item value="list" aria-label="List view">
        <dui-icon name="list"/>
    </dui-toggle-group-item>
    <dui-toggle-group-item value="grid" aria-label="Grid view">
        <dui-icon name="layout-grid"/>
    </dui-toggle-group-item>
    <dui-toggle-group-item value="map" aria-label="Map view">
        <dui-icon name="map"/>
    </dui-toggle-group-item>
</dui-toggle-group>

<dui-toggle-group asp-for="Amenities" type="ToggleGroupType.Multiple" spacing="0" variant="ToggleVariant.Outline">
    <dui-toggle-group-item value="wifi" aria-label="Free Wi-Fi">
        <dui-icon name="wifi"/>
    </dui-toggle-group-item>
    <dui-toggle-group-item value="pool" aria-label="Pool">
        <dui-icon name="waves"/>
    </dui-toggle-group-item>
    <dui-toggle-group-item value="parking" aria-label="Parking">
        <dui-icon name="square-parking"/>
    </dui-toggle-group-item>
</dui-toggle-group>

<dui-toggle-group asp-for="Sort" type="ToggleGroupType.Single" spacing="0" variant="ToggleVariant.Outline">
    <dui-toggle-group-item value="Recommended">
        <dui-icon name="sparkles"/>
        Recommended
    </dui-toggle-group-item>
    <dui-toggle-group-item value="Price">
        <dui-icon name="banknote"/>
        Price
    </dui-toggle-group-item>
    <dui-toggle-group-item value="Rating">
        <dui-icon name="star"/>
        Rating
    </dui-toggle-group-item>
</dui-toggle-group>

<dui-toggle-group asp-for="TripStyles" type="ToggleGroupType.Multiple" spacing="0" variant="ToggleVariant.Outline">
    <dui-toggle-group-item value="Beach">
        <dui-icon name="sailboat"/>
        Beach
    </dui-toggle-group-item>
    <dui-toggle-group-item value="City">
        <dui-icon name="building"/>
        City
    </dui-toggle-group-item>
    <dui-toggle-group-item value="Camping">
        <dui-icon name="tent"/>
        Camping
    </dui-toggle-group-item>
</dui-toggle-group>
```
