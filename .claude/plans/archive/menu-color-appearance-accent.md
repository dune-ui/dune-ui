# Plan: Support shadcn menu configuration (menu color / appearance / accent)

**Status:** Backlog — not started. Deferred out of the Dropdown Menu build.
**Created:** 2026-07-01
**Related component:** Dropdown Menu (keystone) → also Context Menu, Menubar, Navigation Menu, Select, Command, Popover.

## Why this exists

While building Dropdown Menu we found DuneUI renders a **translucent "liquid glass" menu with muted destructive items** by default, whereas real shadcn renders an **opaque menu with red destructive items** by default. Root cause (fully diagnosed):

- shadcn's `apps/v4/registry/styles/style-<pack>.css` defines a `.cn-menu-translucent { @apply … }` block. This is **not a component style that ships** — it's the *definition* of an **opt-in** treatment that shadcn's CLI inlines only when the user selects a translucent menu.
- `cn-menu-translucent` (plus `cn-menu-target`, `cn-logical-sides`, `cn-rtl-flip`, `cn-font-heading`) are **build-time marker directives**. shadcn's CLI (`node_modules/shadcn/dist/chunk-7SBJAAAY.js`, set `C = new Set([...])`) rewrites/strips them at `add` time based on `components.json`.
- DuneUI's themepack generator (`util/ThemePackGenerator/Program.cs:83`, `ExtractComponentsFromThemeStyle`) scoops **every** `.cn-*` block into a token, so `.cn-menu-translucent` leaked in as `dui-menu-translucent`. Then `DropdownMenuContentTagHelper` / `DropdownMenuSubContentTagHelper` apply it **unconditionally**, force-enabling shadcn's opt-in glass mode (and its deliberate destructive-neutralizing `!important` overrides) on every menu.

Verified against a real shadcn Nova project (`menuColor: "default"`): opaque content (`bg-popover`, `oklch(1 0 0)`), destructive item red (`oklch(0.577 0.245 27.325)` = `--destructive`).

**This plan makes menu color/appearance/accent first-class DuneUI configuration instead of an accidental always-on default.** A separate, smaller fix (below) unblocks correct default rendering in the meantime.

## Authoritative shadcn facts (source of truth for implementation)

From the shadcn CLI Zod schema (`node_modules/shadcn/dist/chunk-MTWZIHEW.js`) and preset table (`dist/index.js`):

- **`menuColor`**: enum `["default", "inverted", "default-translucent", "inverted-translucent"]`, default `"default"`.
  - Two orthogonal axes encoded in one property:
    - **inverted** — menu surface renders in the opposite color scheme. Mechanism: the `cn-menu-target` marker → `dark` class for `inverted`/`inverted-translucent`, else removed.
    - **translucent** — frosted-glass surface (`bg-popover/70` + `before:backdrop-blur-2xl …`) plus the item overrides (neutral `foreground/10` hover; destructive text/icon forced to `accent-foreground!`; destructive hover forced to `foreground/10!`). Mechanism: the `cn-menu-translucent` marker → inlined via `twMerge(existingClasses, translucentUtils)` then marker removed. When NOT translucent, the marker is simply stripped.
- **`menuAccent`**: enum `["subtle", "bold"]`, default `"subtle"`. Controls item focus/highlight accent intensity (subtle low-contrast hover vs. bold solid `bg-accent`). *(Exact class transform not yet fully reverse-engineered — confirm during implementation; see Open Questions.)*
- **All 8 built-in presets** (vega, nova, maia, lyra, mira, sera, rhea, luma) default to `menuColor: "default"` **and** `menuAccent: "subtle"`. None ship translucent by default. (Consistent with our finding that only Lyra lacked a translucent token; the rest carry the *definition* but shadcn never applies it by default.)

> Note on "menu appearance": the CLI schema exposes exactly two menu knobs — `menuColor` and `menuAccent`. If the shadcn **docs UI** presents a third "appearance" control, it is likely a presentation grouping over these two (e.g. appearance = the color/translucency axis). Reconcile the naming during implementation; internally there are two settings.

## Prerequisite / related fix (can land independently, ahead of this plan)

Make DuneUI's **default** match shadcn's default (`menuColor: default`) so destructive renders red and menus are opaque:

- **Option A (component layer):** stop unconditionally adding `dui-menu-translucent` in `DropdownMenuContentTagHelper.cs:42` and `DropdownMenuSubContentTagHelper.cs:50`.
- **Option B (generator layer, per repo rule "never hand-edit a themepack"):** add a custom processor in `util/ThemePackGenerator/Processors.cs` that drops marker-derived tokens (`dui-menu-translucent`, and guards the other four markers should they ever gain `@apply` blocks) so they never become tokens; regenerate the packs.

Recommended: do **both** — B for source hygiene, A because the tag helpers shouldn't apply an opt-in mode by default. This full menu-config work then *re-introduces* translucent as a deliberate, selectable option.

## Proposed DuneUI design (the actual deferred work)

### 1. Model the settings

Add strongly-typed enums mirroring shadcn (own enums per DuneUI convention, `extension(...)` for any data-attribute text):

- `MenuColor { Default, Inverted, DefaultTranslucent, InvertedTranslucent }`
- `MenuAccent { Subtle, Bold }`

Decide the resolution order (global default → per-menu override):
- **Global default** via `AddDuneUI()` options (e.g. `DuneUIOptions.MenuColor` / `MenuAccent`), so a consumer can set the app-wide menu style once, matching how they'd pick a shadcn preset.
- **Per-instance override** on `dui-dropdown-menu-content` (and siblings) via `menu-color` / `menu-accent` attributes, nullable, resolved at top of `ProcessAsync` (`var effective = X ?? options.X ?? MenuColor.Default;`).

### 2. Represent the axes as themepack tokens (not markers)

Split the leaked blob into intentional, independently-applicable tokens the generator emits and the tag helpers compose based on resolved settings:

- `dui-menu-translucent` — the frosted surface + hover overrides (already exists; keep, but apply **only** when the resolved color is a `*-translucent`).
- `dui-menu-inverted` — the inverted/`dark` treatment (new; derive from how `cn-menu-target` resolves to `dark`).
- `dui-menu-accent-subtle` / `dui-menu-accent-bold` — the two accent treatments (new; derive from the menuAccent transform).

The generator should produce these deterministically from the shadcn source rather than us hand-writing them, per the "no themepack hand-edits" rule. This likely means **new custom processors** in `Processors.cs` that:
- recognize the marker `.cn-*` blocks,
- emit them under DuneUI token names bound to a setting (not as always-on component classes),
- and encode the inverted/accent transforms (which in shadcn are done in TS, not CSS) as CSS-token equivalents.

### 3. Apply in the tag helpers

`DropdownMenuContentTagHelper` / `DropdownMenuSubContentTagHelper` compose the resolved tokens:

```
ClassMerger.Merge(
    new ThemeToken("dui-dropdown-menu-content"),
    new ThemeToken("dui-dropdown-menu-content-logical"),
    effectiveColor.IsTranslucent() ? new ThemeToken("dui-menu-translucent") : ThemeToken.None,
    effectiveColor.IsInverted()    ? new ThemeToken("dui-menu-inverted")    : ThemeToken.None,
    new ThemeToken(effectiveAccent == MenuAccent.Bold ? "dui-menu-accent-bold" : "dui-menu-accent-subtle"),
    DropdownMenuInternals.ContentStaticClasses,
    …,
    output.GetUserSuppliedClass())
```

(Confirm whether inverted should also toggle a `dark` scope on the popover element, matching shadcn's `cn-menu-target → dark`.)

### 4. Reuse across the menu family

Because Dropdown Menu is the keystone, the same resolution + token composition must be shared with Context Menu, Menubar, Navigation Menu, Select, Command, and Popover surfaces (all use `cn-menu-target` / `cn-menu-translucent` in shadcn). Factor the resolution + class composition into a shared helper (e.g. `DropdownMenuInternals`-style util or a `MenuSurface` helper) so every menu content honors the global/per-instance settings uniformly.

## Docs & samples

- Add a docs sample demonstrating each `menuColor` / `menuAccent` combination (travel theme — Voyager Travel).
- Note the global `AddDuneUI` option in the theming docs.
- Update the component-parity backlog (`component-parity.md`, sibling in this plans folder) once shipped.

## Open questions (resolve before implementing)

1. **`menuAccent` transform** — reverse-engineer the exact subtle↔bold class delta from the CLI (`dist/index.js` / `chunk-*`); the quick grep during diagnosis didn't isolate it.
2. **Inverted mechanism** — does DuneUI want a real `dark`-scope toggle on the popover (shadcn's approach) or a dedicated `dui-menu-inverted` token? The former is closer to shadcn; the latter avoids nested-dark surprises.
3. **"Appearance" naming** — confirm whether to expose two settings (color, accent) or add a friendlier grouping; keep internal model at two.
4. **Generator strategy** — how much of shadcn's TS-time transform (marker → inlined utilities) do we replicate as CSS tokens vs. compute in C#? Prefer generator-emitted tokens to honor the "no hand-edited themepacks" rule.
5. **Default** — confirm DuneUI ships `Default` + `Subtle` (matching all shadcn presets). The translucent look becomes opt-in, not the signature default.

## Scope boundaries

- **In:** enums + global/per-instance resolution; generator processors to emit color/accent/inverted tokens from shadcn source; tag-helper composition for the whole menu family; docs samples.
- **Out (separate tickets):** the prerequisite default-fix above (should land first, independently); any redesign of the themepack format; RTL / logical-sides markers (`cn-logical-sides`, `cn-rtl-flip`) and `cn-font-heading` — track separately even though they're the same class of build-time marker.

## Key references

- shadcn CLI marker set + transform: `node_modules/shadcn/dist/chunk-7SBJAAAY.js` (in a scaffolded shadcn app), Zod enums: `chunk-MTWZIHEW.js`, presets: `index.js`.
- shadcn style source the generator reads: `apps/v4/registry/styles/style-<pack>.css` (`.cn-menu-translucent`, `.cn-dropdown-menu-*`).
- DuneUI generator: `util/ThemePackGenerator/Program.cs`, `util/ThemePackGenerator/Processors.cs`.
- DuneUI apply sites: `src/DuneUI/TagHelpers/DropdownMenu/DropdownMenuContentTagHelper.cs`, `…/DropdownMenuSubContentTagHelper.cs`.
- Existing token: `-dui-menu-translucent` in `src/DuneUI/Theming/ThemePacks/*.themepack`.
