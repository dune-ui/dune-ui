---
name: duneui
description: >-
  Builds ASP.NET Core MVC / Razor Pages UIs with DuneUI tag helpers (the <dui-*>
  elements). Covers project setup, the component catalog (tags, attributes, enum
  values, examples), and the conventions that make DuneUI markup correct — the
  Invoker Commands API for overlays, CSS-class override order, fully-qualified
  enums in Razor, and theming. Use when editing .cshtml or .razor files that use
  DuneUI, when scaffolding a new DuneUI-based page or form, or when the user
  mentions DuneUI, dui tag helpers, or any <dui-*> component.
metadata:
  author: DuneUI
  version: 0.x
paths:
  - "**/*.cshtml"
  - "**/*.razor"
  - "**/_ViewImports.cshtml"
---

# Building UIs with DuneUI

DuneUI is a library of **ASP.NET Core Tag Helpers** that mirror
[shadcn/ui](https://ui.shadcn.com/), for building MVC / Razor Pages UIs. Every
component is a server-rendered `<dui-*>` element. Interactivity that HTML/CSS
can't do alone is provided by small bundled web components (`<del-*>`), which you
never write by hand — the tag helpers emit them for you.

Use this skill whenever you write or edit Razor markup that uses DuneUI.

## How to use this skill

The detail lives in `references/`, loaded on demand — open only what the task needs:

- **`references/setup.md`** — is DuneUI installed and wired up? Read this when a
  project is new to DuneUI, or when components render unstyled (missing CSS/JS, or
  a conflicting CSS framework).
- **`references/conventions.md`** — the rules that make DuneUI markup *correct*.
  **Read this before writing any non-trivial DuneUI markup**; the mistakes it
  prevents (wrong enum syntax, broken overlays, class order) are the common ones.
- **`references/components-index.md`** — one-line-per-component table (tags +
  summary). Scan this to find the right component.
- **`references/components/<name>.md`** — per-component reference: exact tag
  names, every attribute with its type / default / allowed values, and working
  code examples taken from the DuneUI docs site. Open the specific component(s)
  you're using.

## The five things to get right (details in conventions.md)

1. **Enums are written fully-qualified in Razor**, not as bare strings:
   `variant="ButtonVariant.Outline"`, not `variant="outline"`.
2. **`class` always wins.** Any `class` you add is merged *last* (via
   TailwindMerge), so it overrides the component's defaults — use it to tweak,
   don't fight it.
3. **Overlays (Sheet, Dialog, Popover, DropdownMenu, AlertDialog) open via the
   native Invoker Commands API** — a trigger button carries `commandfor="<id>"`
   and `command="show-modal"` / `close`. No JS click handlers, no `onclick`.
4. **Kebab-case attributes.** A C# property `ShowCloseButton` binds to
   `show-close-button`.
5. **Composite components have a required tag hierarchy** (e.g. sidebar,
   accordion, select). Follow the nesting shown in the component's reference —
   children must sit inside their parent.

## Never hand-author `<del-*>` web components or `data-*` state attributes

Those are emitted by the tag helpers. Your job is the `<dui-*>` markup only.
