---
component: Popover
tags: [dui-popover, dui-popover-description, dui-popover-header, dui-popover-title]
generated: true
---

# Popover

A floating panel of rich content anchored to a trigger element, rendered as a native popover.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-popover>` | A floating panel of rich content anchored to a trigger element, rendered as a native popover. |
| `<dui-popover-description>` | The descriptive body text of a popover, shown beneath the title. |
| `<dui-popover-header>` | The header region of a popover; typically contains the title and description. |
| `<dui-popover-title>` | The title heading of a popover. |

## Attributes

### `<dui-popover>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `position` | `PositionArea` | `Bottom` | `TopCenter`, `TopSpanLeft`, `TopSpanRight`, `Top`, `LeftCenter`, `LeftSpanTop`, `LeftSpanBottom`, `Left`, `BottomCenter`, `BottomSpanLeft`, `BottomSpanRight`, `Bottom`, `RightCenter`, `RightSpanTop`, `RightSpanBottom`, `Right`, `TopLeft`, `TopRight`, `BottomLeft`, `BottomRight` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Example

*From `Pages/Popover/_Intro.cshtml`*

```razor
<dui-button variant="ButtonVariant.Outline" popovertarget="--popover-intro">
    <dui-icon name="sliders-horizontal" class="text-muted-foreground"/>
    Configure View
</dui-button>
<dui-popover id="--popover-intro">
    <div class="flex flex-col gap-y-3">
        <dui-field-set>
            <dui-field-legend variant="FieldLegendVariant.Label">Sort By</dui-field-legend>
            <dui-field-group data-slot="radio-group">
                <dui-field orientation="FieldOrientation.Horizontal">
                    <dui-input type="radio" name="sort-by" value="departure" id="sort-departure" checked/>
                    <dui-field-label for="sort-departure" class="font-normal">
                        Departure Date
                    </dui-field-label>
                </dui-field>
                <dui-field orientation="FieldOrientation.Horizontal">
                    <dui-input type="radio" name="sort-by" value="posted" id="sort-posted"/>
                    <dui-field-label for="sort-posted" class="font-normal">
                        Date Posted
                    </dui-field-label>
                </dui-field>
            </dui-field-group>
        </dui-field-set>
        <dui-separator orientation="SeparatorOrientation.Horizontal"/>
        <dui-field-set>
            <dui-field-legend variant="FieldLegendVariant.Label">View As</dui-field-legend>
            <dui-field-group data-slot="radio-group">
                <dui-field orientation="FieldOrientation.Horizontal">
                    <dui-input type="radio" name="view-as" value="departure" id="view-list" checked/>
                    <dui-field-label for="view-list" class="font-normal">
                        List
                    </dui-field-label>
                </dui-field>
                <dui-field orientation="FieldOrientation.Horizontal">
                    <dui-input type="radio" name="view-as" value="posted" id="view-gallery"/>
                    <dui-field-label for="view-gallery" class="font-normal">
                        Gallery
                    </dui-field-label>
                </dui-field>
            </dui-field-group>
        </dui-field-set>
    </div>
</dui-popover>
```
