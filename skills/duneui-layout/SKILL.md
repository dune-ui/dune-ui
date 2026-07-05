---
name: duneui-layout
description: >-
  Composes page layouts and app shells with DuneUI tag helpers in ASP.NET Core — the sidebar
  dashboard shell (wrapper + sidebar + inset with a trigger), the Container / Stack / Group
  spacing primitives, and Card composition. Use when building a DuneUI page layout, an
  admin/dashboard shell, a navigation sidebar, or a card, or when the user mentions a DuneUI
  layout, sidebar, dashboard, app shell, or card.
metadata:
  author: DuneUI
---

# Composing layouts with DuneUI

Patterns for page structure. For a component's attributes/values, open its file under
`../duneui/references/components/` (`sidebar.md`, `layout.md`, `card.md`); for cross-cutting
rules see `../duneui/references/conventions.md`.

## The sidebar app shell (dashboard)

`<dui-sidebar-wrapper>` is the shell; it contains exactly two children — the `<dui-sidebar>` and
the `<dui-sidebar-inset>` (your main content). The toggle lives in the inset's header.

```razor
<dui-sidebar-wrapper>
    <dui-sidebar>
        <dui-sidebar-header>
            <!-- brand / workspace switcher, usually a dui-sidebar-menu-link -->
        </dui-sidebar-header>
        <dui-sidebar-content>
            <dui-sidebar-group>
                <dui-sidebar-group-label>Platform</dui-sidebar-group-label>
                <dui-sidebar-group-content>
                    <dui-sidebar-menu>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="/dashboard">
                                <dui-icon name="layout-dashboard" /><span>Dashboard</span>
                            </dui-sidebar-menu-link>
                            <dui-sidebar-menu-badge>12</dui-sidebar-menu-badge>
                        </dui-sidebar-menu-item>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="/destinations">
                                <dui-icon name="map-pinned" /><span>Destinations</span>
                            </dui-sidebar-menu-link>
                            <dui-sidebar-menu-sub>
                                <dui-sidebar-menu-sub-item>
                                    <dui-sidebar-menu-sub-link href="/destinations/europe"><span>Europe</span></dui-sidebar-menu-sub-link>
                                </dui-sidebar-menu-sub-item>
                            </dui-sidebar-menu-sub>
                        </dui-sidebar-menu-item>
                    </dui-sidebar-menu>
                </dui-sidebar-group-content>
            </dui-sidebar-group>
        </dui-sidebar-content>
        <dui-sidebar-footer>
            <!-- user menu, often a dui-dropdown-menu over a dui-sidebar-menu-button -->
        </dui-sidebar-footer>
    </dui-sidebar>

    <dui-sidebar-inset>
        <header class="flex h-14 items-center gap-2 border-b px-4">
            <dui-sidebar-trigger></dui-sidebar-trigger>
            <dui-separator orientation="SeparatorOrientation.Vertical" class="mx-1 h-4" />
            <span class="text-sm font-medium">Dashboard</span>
        </header>
        <div class="p-4">
            <!-- page content -->
        </div>
    </dui-sidebar-inset>
</dui-sidebar-wrapper>
```

Rules:
- Body order inside `<dui-sidebar>`: header → content (groups) → footer.
- Follow the menu hierarchy exactly — `dui-sidebar-menu` › `dui-sidebar-menu-item` ›
  `dui-sidebar-menu-link` (or `-menu-button`); optional `-menu-badge`; nested submenus are
  `-menu-sub` › `-menu-sub-item` › `-menu-sub-link`. Don't flatten it.
- `<dui-sidebar-trigger>` is a real toggle (also Ctrl/⌘ + B) — don't add `onclick`.
- Variants on `<dui-sidebar>`: `variant="SidebarVariant.Inset"` (main content as a floating
  card), `SidebarVariant.Floating`; `side="SidebarSide.Right"`; `collapsible="SidebarCollapsible.Icon"`.
- `<dui-sidebar-menu-link>` supports routing (`href`/`asp-*`) and marks itself active on the
  current route.

## Spacing primitives: Container / Stack / Group

Reach for these instead of hand-rolling flex utilities, and control spacing with the
`gap`/`align`/`justify` enum attributes.

- **`<dui-container>`** — centered, width-constrained page wrapper. Outermost page-width element.
- **`<dui-stack>`** — vertical column (`flex-col`). `gap` (`StackGap`), `align` (`StackAlign`,
  default `Stretch`), `justify` (`StackJustify`).
- **`<dui-group>`** — horizontal row (`flex-row`). `gap` (`GroupGap`), `align` (`GroupAlign`),
  `justify` (`GroupJustify`).

```razor
<dui-container>
    <dui-stack gap="StackGap.Large">
        <dui-group justify="GroupJustify.SpaceBetween" align="GroupAlign.Center">
            <h1 class="text-xl font-semibold">Bookings</h1>
            <dui-button><dui-icon name="plus" />New booking</dui-button>
        </dui-group>
        <!-- content rows -->
    </dui-stack>
</dui-container>
```

Gap members: `ExtraSmall, Small, Default, Large, ExtraLarge`. Justify: `Start, Center, End,
SpaceBetween, SpaceAround`. Align: `Stretch, Start, Center, End, Baseline`.

## Card composition

Slots in fixed order: header (title / description / optional action) → content → footer.

```razor
<dui-card class="mx-auto w-full max-w-sm">
    <dui-card-header>
        <dui-card-title>Login to your account</dui-card-title>
        <dui-card-description>Enter your email below to sign in.</dui-card-description>
        <dui-card-action>
            <!-- optional top-right slot, e.g. a dui-dropdown-menu trigger -->
        </dui-card-action>
    </dui-card-header>
    <dui-card-content>
        <form method="post">
            <dui-field-group>
                <dui-input asp-for="Email" />
                <dui-input asp-for="Password" />
            </dui-field-group>
        </form>
    </dui-card-content>
    <dui-card-footer class="flex-col gap-2">
        <dui-button type="submit" class="w-full">Login</dui-button>
    </dui-card-footer>
</dui-card>
```

- Dividers are opt-in utilities: `<dui-card-header class="border-b">` /
  `<dui-card-footer class="border-t">`.
- Adjust width/spacing with `class` (merged last, so it wins).
- Building the form inside the card? See the `duneui-forms` skill.

## Rules

1. `<dui-sidebar-wrapper>` must hold both `<dui-sidebar>` and `<dui-sidebar-inset>`; page content
   and the trigger go in the inset.
2. Respect the sidebar and card tag hierarchies; don't flatten or reorder slots.
3. Use `<dui-container>`/`<dui-stack>`/`<dui-group>` + their `gap`/`align`/`justify` enums for
   layout rhythm rather than raw flex classes.
4. All variant/size/align/gap/justify attributes take fully-qualified enum values.
