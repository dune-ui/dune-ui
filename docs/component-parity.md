# shadcn/ui component parity

Tracks which [shadcn/ui](https://ui.shadcn.com/docs/components) components have been
brought over to DuneUI as Tag Helpers, and what's left. This is the durable backlog —
update it as each component lands.

## Definition of done

Every component should walk through this pipeline before it's marked ✅ (skip steps that
don't apply — not everything needs a web component or a form-posting playground demo):

- [ ] `TagHelper` under `src/DuneUI/TagHelpers/<Component>/` — inherit `DuneUITagHelperBase`
      (or `FieldInputBaseTagHelper` for form fields); enum→data-attr via `extension(...)`;
      nullable bound props resolved at the top of `ProcessAsync`; emit `data-slot`. Replicate
      shadcn's class names, tokens, and `data-*` attributes faithfully (see
      [Stay faithful to shadcn](#stay-faithful-to-shadcn)).
- [ ] Themepack tokens — usually already generated from shadcn into `Theming/ThemePacks/*.themepack`
      (verify, like Slider's `dui-slider*` were); only regenerate via `util/ThemePackGenerator` if missing.
- [ ] `del-*` Lit web component (light DOM) + `import` in `Client/js/dune-ui.ts` — only if
      HTML/CSS can't do it. Prefer the Invoker Commands API over click handlers.
- [ ] `npm run build` (js + css) from `src/DuneUI/Client/`; format with CSharpier + oxfmt.
- [ ] DocsSamples page: `docs/DocsSamples/Pages/<Component>/` (Index + partials) + nav entry
      in `Pages/Shared/_NavigationLayout.cshtml`. Write all example **content** in the
      travel-website theme (see [Example content: travel theme](#example-content-travel-theme)).
- [ ] ComponentPlayground demo (`sandbox/ComponentPlayground/Pages/Demo/`) — when it posts
      form values, add a postback round-trip there.
- [ ] Generator: register partials in `docs/DocsSamplesGenerator/Generator.cs`; add any
      model-bound demo's model to `Pages/DocsStatic.cshtml.cs`.
- [ ] Website: add `content/docs/tag-helpers/components/<component>.mdx` + `meta.json` entry,
      then run the generator to emit demo HTML + code-includes into the `website` repo.
- [ ] Commit both repos.

## Stay faithful to shadcn

**Mirror shadcn as closely as possible** — this is not cosmetic, it's load-bearing. When porting a
component, open its source (`apps/v4/registry/bases/base/ui/<component>.tsx`) and replicate:

- **Class names / Tailwind utilities** — copy the *static* (cross-theme) utility classes into the
  tag helper **verbatim**, in the same order. These drive layout and responsive behavior, e.g. Alert
  Dialog's footer needs `group-data-[size=sm]/alert-dialog-content:grid grid-cols-2` for the small
  size to lay out correctly. Don't borrow a sibling component's classes (Dialog ≠ Alert Dialog) or
  invent your own — diff against the real source.
- **Tokens** — every `cn-<name>` in shadcn maps to a `dui-<name>` themepack token. Reference the
  matching `new ThemeToken("dui-...")` for **each** slot, even sub-elements like `*-action` /
  `*-cancel`. Keep the theme token vs static-utility split shadcn uses: the token carries
  theme-specific styling, the static classes are constant across themes — pass both, with the token
  before the user-supplied class so authors can still override. A token that isn't generated yet
  resolves to `""` (harmless); still reference it so it lights up when the pack is regenerated.
- **`data-*` attributes** — emit the same `data-slot` (and `data-size`, `data-state`, …) values
  shadcn sets, including where a wrapped primitive *overrides* the inner element's slot (e.g. Alert
  Dialog's action/cancel set `data-slot="alert-dialog-action"`/`"-cancel"`, **not** `"button"`). The
  themepack selectors key off these (`group-data-[size=...]`, `has-data-[slot=...]`), so a wrong or
  missing `data-*` silently breaks styling.

Justified divergences (the native `<dialog>` having no overlay element / using `closedby` /
`data-open:grid` instead of Radix portal positioning) are fine — but call them out, don't let them
creep in by accident.

## Example content: travel theme

All DocsSamples example **content** runs a consistent fictitious **travel website** theme — match it
in every new partial (and re-theme any ported shadcn copy like "delete your account"). This is about
the *copy* only: titles, descriptions, button labels, placeholders, demo data. Never change a
component's structure, slots, `data-*`, or example names to fit the theme.

Voice: casual-professional consumer-travel SaaS — second person ("your trip"), Title Case noun-phrase
titles, short imperative button labels. Give each example on a page a *distinct* scenario so they
don't read repetitively.

Reusable vocabulary (grep existing partials for more):
- **Brand:** Voyager Travel (subtitle "Admin Console"); email domain `voyager.travel`.
- **Persona:** Ibn Battuta · handle `@@ibnbattuta` (escaped for Razor) · `ibn.battuta@rihlah.travel`.
  Secondary: Amelia Hart (`amelia@voyager.travel`), Sarah Chen.
- **Refs:** trip ref `Trip #TRV-987`; booking IDs `TRP-48xx`; statuses Confirmed / Pending /
  Cancelled; cabins Economy / Premium Economy / Business Class / First Class.
- **Destinations:** Paris, Bangkok, Kyoto, NYC (JFK), London, Rome, Cancun, Cape Town; regions Europe
  (France/Italy/Spain), Asia Pacific, The Americas.
- **Properties/tours:** Grand Hotel Venice, The Grand Resort & Spa, Westminster Abbey Tour.
- **Categories:** Flights / Accommodation / Car Rental. **Seed data:** `DocsSamples/StaticData.cs`.
- **Action verbs:** Book, View Tickets, Manage Booking, Add Traveler Details, Reset Search Filters,
  Book Again, Add to Calendar, Save to wishlist. Destructive confirms: short question title ("Cancel
  this booking?"), body opening "This action cannot be undone…", buttons like Keep booking / Cancel
  booking, destructive variant on the confirm action.

## Status

Legend: ✅ done · 🚧 in progress · ☐ todo

### Tier 1 — low effort (mostly HTML/CSS, little/no JS)

| Component | Status | Web component? | Depends on / notes |
|---|---|---|---|
| Toggle | ✅ | no — checkbox-backed (like Switch) | label wraps `sr-only` checkbox; `has-[:checked]` styling |
| Toggle Group | ✅ | no | native radio (single) / checkbox (multiple); no JS roving needed |
| Aspect Ratio | ☐ | no — pure CSS `aspect-ratio` | |
| Alert Dialog | ✅ | reuse Dialog's `del-dialog` | confirm/cancel over native `<dialog>`; `dui-alert-dialog-action`/`-cancel` set `returnValue`; `duneui.alertDialog().confirmAsync()` helper |
| Input OTP | ✅ | yes (small) — `del-input-otp` | single real input overlaid on presentational slots |

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

Accordion, Alert, **Alert Dialog** ✅, Avatar, Badge, Breadcrumb, Button, Button Group, Card, Checkbox,
Collapsible, Dialog, Empty, Field, Icon, Input, Input Group, **Input OTP** ✅, Item, Kbd, Label,
Pagination, Popover, Progress, Radio, Select, Separator, Sheet, Sidebar, Skeleton, Slider,
Spinner, Switch, Table, Tabs, Textarea, **Toggle** ✅, **Toggle Group** ✅, Tooltip.
Plus DuneUI-specific layout helpers
(Group, Stack) and JS helpers (js-dialog).
