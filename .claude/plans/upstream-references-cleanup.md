# Upstream references cleanup (shadcn / Base UI / Radix)

Goal: remove references to shadcn, Base UI, Radix from code and docs, **except** the
project-background / attribution section (which you've said stays) and load-bearing
infrastructure. For each item below, mark the box for the action you approve. Where a
straight deletion would leave a gap, I've drafted a **replacement** instead.

**How to use:** tick `[x]` next to the action you want (or edit the suggested text). Leave
`DECISION:` blank items for later. When you're done, hand it back and I'll apply the approved
edits. Items marked _(regen)_ need you to re-run the DocsSamplesGenerator afterwards.

Legend: **EDIT** = reword in place · **REMOVE** = delete the line/clause · **KEEP** = leave as-is

---

## Group 1 — User-facing website docs (highest priority)

### 1.1 `website/…/components/dropdown-menu.mdx:114` — EDIT _(regen source is 1.2)_
> Current:
> "Radio items report selection through a bubbling `valuechange` event. **Mirroring Base UI's `RadioGroup.onValueChange`,** listen on the `<dui-dropdown-menu-radio-group>` and read `event.detail.value`…"

> Suggested:
> "Radio items report selection through a bubbling `valuechange` event. Listen on the `<dui-dropdown-menu-radio-group>` and read `event.detail.value` — the newly selected value — from a single handler (`event.target` is the selected item, if you need the element). The component enforces single-selection before the event fires, and the menu stays open."

- [x] Approve edit as-is
- [ ] Approve with my changes (edit above)
- DECISION:

### 1.2 `dune-ui/…/DropdownMenu/_RadioEvents.cshtml:12` — EDIT (source of the generated `_include/dropdown-menu-radio-events.mdx:11`) _(regen)_
> Current: `<!-- Listen on the group (like Base UI's RadioGroup.onValueChange) -->`

> Suggested: `<!-- Listen once on the group; the event bubbles up from the selected radio item -->`

- [x] Approve edit as-is
- [ ] Approve with my changes
- DECISION:

### 1.3 Demo placeholder `https://x.com/shadcn` — EDIT _(regen)_
Appears in `dune-ui/…/InputGroup/_Buttons.cshtml:5` (source) → generated `website/…/_include/input-group-buttons.mdx:3`.
> Suggested new placeholder (Voyager Travel theme): `https://voyager.travel/@ibnbattuta`
> (or pick your own handle)

- [ ] Approve `https://voyager.travel/@ibnbattuta`
- [ ] Use this instead → ____________________
- DECISION: KEEP

---

## Group 2 — XML doc comments (leak into consumers' IntelliSense ⚠️)

These `<summary>` / `<c>` tags surface in the ASP.NET developer's editor tooltips, so they're
effectively user-facing. Suggested edits describe the component on its own terms.

### 2.1 `DropdownMenuLabelTagHelper.cs:6` — EDIT
> Current: `/// <summary>A non-interactive heading inside the menu (shadcn <c>DropdownMenuLabel</c>).</summary>`
> Suggested: `/// <summary>A non-interactive heading inside the menu.</summary>`

- [ ] Approve
- DECISION: delete entire comment

### 2.2 `DropdownMenuGroupTagHelper.cs:6` — EDIT
> Current: `/// <summary>Groups a set of related menu items (shadcn <c>DropdownMenuGroup</c>).</summary>`
> Suggested: `/// <summary>Groups a set of related menu items.</summary>`

- [ ] Approve
- DECISION: delete entire comment

### 2.3 `DropdownMenuItemTagHelper.cs:9,12` — EDIT
> Current (excerpt): "A selectable menu item **(shadcn `DropdownMenuItem`)**. Renders as a `<div role="menuitem">`… supplies a URL … **mirroring Base UI's `Menu.Item` / `Menu.LinkItem` primitives.** The `del-dropdown-menu`…"
> Suggested: "A selectable menu item. Renders as a `<div role="menuitem">`, or as an `<a role="menuitem">` when the author supplies a URL — either a raw `href` or ASP.NET routing attributes (`asp-page`, `asp-action`/`asp-controller`, `asp-route-*`, …). The `del-dropdown-menu` web component activates items by `role`, so both elements behave identically."

- [x] Approve (drops both the "(shadcn …)" and the "mirroring Base UI…" sentence)
- DECISION:

### 2.4 `InputOtpTagHelper.cs:12` — EDIT
> Current: "A segmented one-time-code input **(the server-rendered equivalent of shadcn's Input OTP)**. A single real `<input>`…"
> Suggested: "A segmented one-time-code input. A single real `<input>` holds the whole code and posts it as one form value…"

- [x] Approve
- DECISION:

### 2.5 `SliderTagHelper.cs:46` — EDIT
> Current: "…keeps the thumb fully within the track at the extremes **(shadcn default)**,"
> Suggested: "…keeps the thumb fully within the track at the extremes (the default),"

- [x] Approve
- DECISION:

### 2.6 `ToggleGroupTagHelper.cs:40` — EDIT
> Current: "…Defaults to `2` **(matching shadcn)**."
> Suggested: "…Defaults to `2`."

- [x] Approve
- DECISION:

---

## Group 3 — Internal code comments (maintainer-only; not shipped to users)

Plain `//` / `/* */` comments explaining why code matches an upstream source. Not user-visible.
Your call on whether to purge entirely or keep as maintenance breadcrumbs. Pick one policy, or
tick individual items.

**Policy:**
- [x] Remove the upstream name from ALL of the items below
- [ ] Keep them ALL (they document provenance for maintainers)
- [ ] Decide per-item (tick below)

| # | File:line | Comment gist |
|---|-----------|--------------|
| 3.1 | `del-dropdown-menu.ts:195` | "Base UI's closeOnClick overrides the per-role default" |
| 3.2 | `del-dropdown-menu.ts:220` | "Highlight follows the pointer (matching Radix/shadcn)" |
| 3.3 | `del-sidebar.ts:5` | "server-rendered equivalent of shadcn's …" (file header) |
| 3.4 | `del-input-otp.ts:21` | "server-rendered equivalent of shadcn's Input OTP" (header) |
| 3.5 | `del-slider.ts:5` | "equivalent of shadcn's `<Slider>`, a Radix …" (header) |
| 3.6 | `SidebarWrapperTagHelper.cs:25` | "server-rendered equivalent of shadcn's …" |
| 3.7 | `SidebarTagHelper.cs:38` | "mirrors shadcn's `collapsible="none"` branch" |
| 3.8 | `ToggleRenderingHelper.cs:36` | "shadcn's ToggleGroupItem raises the focused item…" |
| 3.9 | `ToggleGroupTagHelper.cs:106` | "honour the numeric spacing the way shadcn does" |
| 3.10 | `InputOtpTagHelper.cs:119,135,148` | refs to shadcn / guilhermerodz/input-otp container + caret |
| 3.11 | `SheetTagHelper.cs:44` | "Do not add any styles from shadcn's sheet.tsx" |
| 3.12 | `SliderTagHelper.cs:103,194` | "the shadcn 'slider' root" / "shadcn keeps disabled state…" |
| 3.13 | `AlertDialogActionTagHelper.cs:34` | "Match shadcn: override Button's data-slot…" |
| 3.14 | `AlertDialogCancelTagHelper.cs:34` | "Match shadcn: override Button's data-slot…" |
| 3.15 | `DropdownMenuInternals.cs:16` | "The Base UI menu content popup…" |

Per-item ticks (only if you chose "Decide per-item"): remove = ✂, keep = ⚓
`3.1__ 3.2__ 3.3__ 3.4__ 3.5__ 3.6__ 3.7__ 3.8__ 3.9__ 3.10__ 3.11__ 3.12__ 3.13__ 3.14__ 3.15__`

---

## Group 4 — Functional / infrastructure (recommend KEEP — removing breaks things)

| # | Item | Why it references upstream | Rec |
|---|------|----------------------------|-----|
| 4.1 | `util/ThemePackGenerator/Program.cs:27–34` | The **source URLs** themepacks are generated from (`raw.githubusercontent.com/shadcn-ui/ui/…`) | KEEP (load-bearing) |
| 4.2 | `util/ThemePackGenerator/Processors.cs:59–282` | Comments explaining each processor vs. the shadcn/BaseUI source; generator project, not shipped | KEEP (or purge as part of Group 3 policy) |
| 4.3 | `src/DuneUI/Client/css/shadcn-tailwind.css` (+ import at `dune-ui.css:7`, `--radix-…` vars) | File name + Radix CSS-var fallbacks | KEEP unless you want a deliberate rename (functional risk) |
| 4.4 | `src/DuneUI/Icons/Definitions/TablerOutline.json` (`brand-radix-ui`) | Third-party Tabler icon literally named that | KEEP |
| 4.5 | website `components.json`, `package.json` (`shadcn` dep), `src/styles/app.css:2`, generated `public/demo/**` | The docs **site's own** tooling/theme (Fumadocs uses shadcn) | KEEP |

Override a KEEP? Note it here: ____________________

---

## Group 5 — Project background / attribution (you said this STAYS — listed for completeness)

- `website/…/tag-helpers/index.mdx:8,16,18` — the introduction.
- `website/…/tag-helpers/acknowledgements.mdx:8–94` — background **+ the shadcn MIT license/copyright** (attribution is legally required — keep regardless).
- `dune-ui/readme.md:7`, `website/README.md:10`, `CLAUDE.md` — repo descriptions ("based on shadcn/ui"). Dev-facing.
- `.claude/plans/*.md`, `sandbox/**` — my scratch plans + throwaway prototypes; ignore.

Want any of these trimmed anyway? Note here: ____________________

---

## After approval
1. I apply all approved EDIT/REMOVE items in `dune-ui`.
2. You re-run **DocsSamplesGenerator** (regenerates the `_include` mdx + demo html for 1.2, 1.3).
3. I apply the approved website-side edits (1.1) and reconcile the regenerated files.
4. Commit both repos (only when you ask).
