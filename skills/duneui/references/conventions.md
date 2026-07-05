# DuneUI conventions

Read this before writing non-trivial DuneUI markup. These are the rules that
separate markup that works from markup that silently renders wrong.

## 1. Enums are fully-qualified in Razor

Attributes backed by a C# enum take the **fully-qualified enum value**, not a
lowercase string:

```razor
<!-- correct -->
<dui-button variant="ButtonVariant.Outline" size="ButtonSize.Small">Save</dui-button>

<!-- wrong — Razor can't bind "outline" to a ButtonVariant -->
<dui-button variant="outline">Save</dui-button>
```

Each component reference lists the enum type and its allowed members. When an enum
attribute is omitted, the component's documented default applies.

## 2. Attributes are kebab-case

A bound C# property maps to a kebab-case HTML attribute:
`ShowCloseButton` → `show-close-button`, `variant` → `variant`.

## 3. `class` is merged last and always wins

Every component composes its own theme classes plus utilities, and folds your
`class` attribute in **last**. Merging is done with TailwindMerge, so a
conflicting utility you supply overrides the default:

```razor
<!-- w-full overrides whatever width the button would default to -->
<dui-button class="w-full">Full width</dui-button>
```

Use `class` to adjust a component; you don't need to override or fight its base
styles.

## 4. Overlays open via the native Invoker Commands API

Sheet, Dialog, Popover, DropdownMenu, and AlertDialog are opened and closed with
the browser's **Invoker Commands API** — never `onclick` / JS handlers.

- Give the overlay an `id` (or let the tag helper auto-generate one).
- A trigger **button** carries `commandfor="<overlay-id>"` and a `command`:
  - `command="show-modal"` to open,
  - `command="close"` to close.

```razor
<dui-button variant="ButtonVariant.Outline" commandfor="edit-profile" command="show-modal">
    Open
</dui-button>

<dui-sheet id="edit-profile">
    <dui-sheet-header>
        <dui-sheet-title>Edit profile</dui-sheet-title>
    </dui-sheet-header>
    <!-- ... -->
    <dui-sheet-footer>
        <dui-button commandfor="edit-profile" command="close">Save</dui-button>
    </dui-sheet-footer>
</dui-sheet>
```

`commandfor` must reference a real element `id`; the trigger must be a button.

## 5. Composite components have a required tag hierarchy

Many components are a family of tags that must nest correctly (sidebar, accordion,
select, dropdown menu, field groups, etc.). Children must sit inside their parent
in the documented order — e.g. `<dui-sidebar-menu-item>` inside
`<dui-sidebar-menu>` inside `<dui-sidebar-group-content>`. Follow the nesting in
each component's reference; don't flatten it.

## 6. Icons: `<dui-icon name="...">`

Icons use `<dui-icon>` with a Lucide icon `name` (kebab-case), e.g.
`<dui-icon name="circle-arrow-left"/>`. They compose naturally inside buttons,
menu links, etc.

## 7. Don't hand-author `<del-*>` or `data-*` state

The `<del-*>` web components and the `data-state` / `data-side` / `data-mobile`
attributes are emitted by the tag helpers and driven at runtime. Author only the
`<dui-*>` markup; never write `<del-*>` yourself or set the runtime `data-*`
attributes by hand.

## 8. Theming (brief)

Colors, radius, and other design tokens come from the active theme pack (**Vega**
by default) as CSS variables, plus Tailwind utilities like `bg-primary`,
`text-muted-foreground`, `border`. Prefer these semantic tokens over hard-coded
colors so components stay consistent in light and dark mode. Deeper theming
(dark mode, menu color/appearance/accent) will be covered by the `duneui-theming`
task skill.
