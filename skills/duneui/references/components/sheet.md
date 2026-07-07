---
component: Sheet
tags: [dui-sheet, dui-sheet-description, dui-sheet-footer, dui-sheet-header, dui-sheet-title]
generated: true
---

# Sheet

A panel that slides in from an edge of the screen, rendered over a native `<dialog>` element. Open and close it with the Invoker Commands API — a trigger button carrying `commandfor` and `command="show-modal"` or `command="close"`.

<!-- structure:begin -->

## Required structure

The trigger button lives **outside** `<dui-sheet>` and points at it via
`commandfor`. The sheet's own children are shallow — header (title +
description), body content, and footer.

```
(a trigger <dui-button commandfor="…" command="show-modal">)   ← outside the sheet
dui-sheet
├── dui-sheet-header
│   ├── dui-sheet-title
│   └── dui-sheet-description
├── ... body content (e.g. dui-field-group)
└── dui-sheet-footer
    └── ... action buttons (dui-button commandfor="…" command="close")
```

<!-- structure:end -->

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-sheet>` | A panel that slides in from an edge of the screen, rendered over a native `<dialog>` element. Open and close it with the Invoker Commands API — a trigger button carrying `commandfor` and `command="show-modal"` or `command="close"`. |
| `<dui-sheet-description>` | Supporting description text for a sheet, shown beneath the title. |
| `<dui-sheet-footer>` | The footer region of a sheet; typically contains action buttons. |
| `<dui-sheet-header>` | The header region of a sheet; typically contains the title and description. |
| `<dui-sheet-title>` | The accessible title of a sheet, rendered as a heading in the sheet header. |

## Attributes

### `<dui-sheet>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `show-close-button` | `bool` | `true` | `true`, `false` |
| `side` | `SheetSide` | `Right` | `Top`, `Right`, `Bottom`, `Left` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Examples

*From `Pages/Sheet/_Intro.cshtml`*

```razor
<div class="flex justify-center">
    <dui-button variant="ButtonVariant.Outline" commandfor="--sheet-intro" command="show-modal">
        Open
    </dui-button>
</div>
<dui-sheet id="--sheet-intro">
    <dui-sheet-header>
        <dui-sheet-title>Edit profile</dui-sheet-title>
        <dui-sheet-description>Make changes to your profile here. Click save when you're done.
        </dui-sheet-description>
    </dui-sheet-header>
    <dui-field-group class="grid gap-6 px-4">
        <dui-field>
            <dui-label for="--sheet-intro-name">Name</dui-label>
            <dui-input id="--sheet-intro-name" name="name" defaultValue="Ibn Battuta"/>
        </dui-field>
        <dui-field>
            <dui-label for="--sheet-intro-username">Username</dui-label>
            <dui-input id="--sheet-intro-username" name="username" defaultValue="@@ibnbattuta"/>
        </dui-field>
    </dui-field-group>
    <dui-sheet-footer>
        <dui-button variant="ButtonVariant.Outline" commandfor="--sheet-intro" command="close">
            Cancel
        </dui-button>
        <dui-button commandfor="--sheet-intro" command="close">
            Save Changes
        </dui-button>
    </dui-sheet-footer>
</dui-sheet>
```

*From `Pages/Sheet/_Sides.cshtml`*

```razor
<div class="flex justify-center gap-2">
    <dui-button variant="ButtonVariant.Outline" commandfor="--sheet-sides-top" command="show-modal">
        Top
    </dui-button>
    <dui-button variant="ButtonVariant.Outline" commandfor="--sheet-sides-right" command="show-modal">
        Right
    </dui-button>
    <dui-button variant="ButtonVariant.Outline" commandfor="--sheet-sides-bottom" command="show-modal">
        Bottom
    </dui-button>
    <dui-button variant="ButtonVariant.Outline" commandfor="--sheet-sides-left" command="show-modal">
        Left
    </dui-button>
</div>
<dui-sheet id="--sheet-sides-top" side="SheetSide.Top" class="data-[side=top]:h-[50vh]">
    <div class="p-4">
        Open on the top
    </div>
</dui-sheet>
<dui-sheet id="--sheet-sides-right" side="SheetSide.Right">
    <div class="p-4">
        Open on the right
    </div>
</dui-sheet>
<dui-sheet id="--sheet-sides-bottom" side="SheetSide.Bottom" class="data-[side=bottom]:h-[50vh]">
    <div class="p-4">
        Open on the bottom
    </div>
</dui-sheet>
<dui-sheet id="--sheet-sides-left" side="SheetSide.Left">
    <div class="p-4">
        Open on the left
    </div>
</dui-sheet>
```
