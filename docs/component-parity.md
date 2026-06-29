# shadcn/ui component parity

Tracks which [shadcn/ui](https://ui.shadcn.com/docs/components) components have been
brought over to DuneUI as Tag Helpers, and what's left. This is the durable backlog —
update it as each component lands.

## Definition of done

Every component should walk through this pipeline before it's marked ✅ (skip steps that
don't apply — not everything needs a web component or a form-posting playground demo):

- [ ] `TagHelper` under `src/DuneUI/TagHelpers/<Component>/` — inherit `DuneUITagHelperBase`
      (or `FieldInputBaseTagHelper` for form fields); enum→data-attr via `extension(...)`;
      nullable bound props resolved at the top of `ProcessAsync`; emit `data-slot`.
- [ ] Themepack tokens — usually already generated from shadcn into `Theming/ThemePacks/*.themepack`
      (verify, like Slider's `dui-slider*` were); only regenerate via `util/ThemePackGenerator` if missing.
- [ ] `del-*` Lit web component (light DOM) + `import` in `Client/js/dune-ui.ts` — only if
      HTML/CSS can't do it. Prefer the Invoker Commands API over click handlers.
- [ ] `npm run build` (js + css) from `src/DuneUI/Client/`; format with CSharpier + oxfmt.
- [ ] DocsSamples page: `docs/DocsSamples/Pages/<Component>/` (Index + partials) + nav entry
      in `Pages/Shared/_NavigationLayout.cshtml`.
- [ ] ComponentPlayground demo (`sandbox/ComponentPlayground/Pages/Demo/`) — when it posts
      form values, add a postback round-trip there.
- [ ] Generator: register partials in `docs/DocsSamplesGenerator/Generator.cs`; add any
      model-bound demo's model to `Pages/DocsStatic.cshtml.cs`.
- [ ] Website: add `content/docs/tag-helpers/components/<component>.mdx` + `meta.json` entry,
      then run the generator to emit demo HTML + code-includes into the `website` repo.
- [ ] Commit both repos.

## Status

Legend: ✅ done · 🚧 in progress · ☐ todo

### Tier 1 — low effort (mostly HTML/CSS, little/no JS)

| Component | Status | Web component? | Depends on / notes |
|---|---|---|---|
| Toggle | ☐ | no — checkbox-backed (like Switch) | |
| Toggle Group | ☐ | no | Toggle |
| Aspect Ratio | ☐ | no — pure CSS `aspect-ratio` | |
| Alert Dialog | ☐ | reuse Dialog | styled confirm preset over existing Dialog |
| Input OTP | ☐ | yes (small) | segmented one-time-code input |

### Tier 2 — menu / overlay family (web component + popover positioning)

Build **Dropdown Menu first** — it establishes the menu + Invoker-command + positioning
machinery the rest of this tier reuses.

| Component | Status | Web component? | Depends on / notes |
|---|---|---|---|
| Dropdown Menu | ☐ | yes | **keystone — unblocks the rest of this tier** |
| Context Menu | ☐ | yes | Dropdown Menu |
| Menubar | ☐ | yes | Dropdown Menu |
| Navigation Menu | ☐ | yes | Dropdown Menu |
| Hover Card | ☐ | maybe | Popover already has a hover variant — may be mostly there |
| Command | ☐ | yes | client-side filtering (command palette) |
| Combobox | ☐ | yes | Command + Popover |

### Tier 3 — heavier JS / data-driven

| Component | Status | Web component? | Depends on / notes |
|---|---|---|---|
| Calendar | ☐ | yes | date grid + keyboard nav |
| Date Picker | ☐ | yes | Calendar + Popover |
| Carousel | ☐ | yes | embla-style |
| Sonner / Toast | ☐ | yes | toast queue |
| Drawer | ☐ | yes | Sheet covers most side-panel cases; Drawer is the draggable bottom sheet |
| Resizable | ☐ | yes | drag-to-resize panels |
| Scroll Area | ☐ | yes | custom scrollbars |
| Data Table | ☐ | — | recipe over existing Table (sort/paginate) |
| Chart | ☐ | yes | largest lift; possibly out of scope |

### Not applicable to a server-side Tag Helper library

- **Form** — react-hook-form/zod; DuneUI solves this via `asp-for` + Field/validation.
- **Typography** — prose styles, not a component.

### Already shipped

Accordion, Alert, Avatar, Badge, Breadcrumb, Button, Button Group, Card, Checkbox,
Collapsible, Dialog, Empty, Field, Icon, Input, Input Group, Item, Kbd, Label, Pagination,
Popover, Progress, Radio, Select, Separator, Sheet, Sidebar, Skeleton, **Slider** ✅,
Spinner, Switch, Table, Tabs, Textarea, Tooltip. Plus DuneUI-specific layout helpers
(Group, Stack) and JS helpers (js-dialog).
