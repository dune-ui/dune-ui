---
component: Sidebar
tags: [dui-sidebar, dui-sidebar-content, dui-sidebar-footer, dui-sidebar-group, dui-sidebar-group-content, dui-sidebar-group-label, dui-sidebar-header, dui-sidebar-inset, dui-sidebar-menu, dui-sidebar-menu-badge, dui-sidebar-menu-button, dui-sidebar-menu-item, dui-sidebar-menu-link, dui-sidebar-menu-sub, dui-sidebar-menu-sub-button, dui-sidebar-menu-sub-item, dui-sidebar-menu-sub-link, dui-sidebar-separator, dui-sidebar-trigger, dui-sidebar-wrapper]
generated: true
---

# Sidebar

The sidebar panel itself, hosting its header, content, and footer. On desktop it renders as a fixed panel that can collapse; on mobile it becomes an off-canvas drawer.

<!-- structure:begin -->

## Required structure

`<dui-sidebar-wrapper>` is the top-level container and holds **two** children:
the `<dui-sidebar>` and a `<dui-sidebar-inset>` for the page content.

```
dui-sidebar-wrapper
├── dui-sidebar
│   ├── dui-sidebar-header
│   ├── dui-sidebar-content
│   │   └── dui-sidebar-group
│   │       ├── dui-sidebar-group-label
│   │       └── dui-sidebar-group-content
│   │           └── dui-sidebar-menu
│   │               └── dui-sidebar-menu-item
│   │                   ├── dui-sidebar-menu-link      (or dui-sidebar-menu-button)
│   │                   ├── dui-sidebar-menu-badge     (optional)
│   │                   └── dui-sidebar-menu-sub       (optional nested menu)
│   │                       └── dui-sidebar-menu-sub-item
│   │                           └── dui-sidebar-menu-sub-link
│   └── dui-sidebar-footer
└── dui-sidebar-inset
    └── ... page content, usually starting with a header containing <dui-sidebar-trigger>
```

<!-- structure:end -->

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-sidebar>` | The sidebar panel itself, hosting its header, content, and footer. On desktop it renders as a fixed panel that can collapse; on mobile it becomes an off-canvas drawer. |
| `<dui-sidebar-content>` | The main scrollable content region of the sidebar; holds the sidebar's groups and menus. |
| `<dui-sidebar-footer>` | The footer region of the sidebar, pinned below its content; typically holds a user menu or secondary actions. |
| `<dui-sidebar-group>` | A titled section within the sidebar that groups related menu items together. |
| `<dui-sidebar-group-content>` | The content region of a sidebar group, wrapping the group's menu. |
| `<dui-sidebar-group-label>` | The label heading for a sidebar group. |
| `<dui-sidebar-header>` | The header region of the sidebar, pinned above its content; typically holds branding or a workspace switcher. |
| `<dui-sidebar-inset>` | The main content area shown alongside the sidebar, rendered as a `<main>` element. |
| `<dui-sidebar-menu>` | A list of menu items within a sidebar group, rendered as a list. |
| `<dui-sidebar-menu-badge>` | A small badge, typically a count, shown at the end of a sidebar menu item; hidden while the sidebar is collapsed to icons. |
| `<dui-sidebar-menu-button>` | A button rendered as an entry within a sidebar menu item. |
| `<dui-sidebar-menu-item>` | A single item within a sidebar menu, rendered as a list item. |
| `<dui-sidebar-menu-link>` | An anchor rendered as an entry within a sidebar menu item, with routing support; marks itself active when it matches the current route. |
| `<dui-sidebar-menu-sub>` | A nested submenu within a sidebar menu item, rendered as a list. |
| `<dui-sidebar-menu-sub-button>` | A button rendered as an entry within a nested sidebar submenu. |
| `<dui-sidebar-menu-sub-item>` | A single item within a nested sidebar submenu, rendered as a list item. |
| `<dui-sidebar-menu-sub-link>` | An anchor rendered as an entry within a nested sidebar submenu, with routing support; marks itself active when it matches the current route. |
| `<dui-sidebar-separator>` | A horizontal separator used to divide sections of the sidebar. |
| `<dui-sidebar-trigger>` | A button that toggles the open or collapsed state of its parent sidebar. |
| `<dui-sidebar-wrapper>` | The outermost sidebar container that provides layout and shared state for the sidebar and its inset content. Renders the `del-sidebar` web component that nested triggers and the backdrop toggle. |

## Attributes

### `<dui-sidebar>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `variant` | `SidebarVariant` | `Sidebar` | `Sidebar`, `Floating`, `Inset` |
| `side` | `SidebarSide` | `Left` | `Left`, `Right` |
| `collapsible` | `SidebarCollapsible` | `Offcanvas` | `Offcanvas`, `Icon`, `None` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-sidebar-menu-button>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `SidebarMenuButtonSize` | `Default` | `Default`, `Small`, `Large` |
| `variant` | `SidebarMenuButtonVariant` | `Default` | `Default`, `Outline` |
| `is-active` | `bool` | `false` | `true`, `false` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-sidebar-menu-link>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `SidebarMenuLinkSize` | `Default` | `Default`, `Small`, `Large` |
| `variant` | `SidebarMenuLinkVariant` | `Default` | `Default`, `Outline` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-sidebar-menu-sub-button>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `SidebarMenuSubLinkSize` | `Medium` | `Small`, `Medium` |
| `is-active` | `bool` | `false` | `true`, `false` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-sidebar-menu-sub-link>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `size` | `SidebarMenuSubLinkSize` | `Medium` | `Small`, `Medium` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Sidebar/_Intro.cshtml`*

```razor
<dui-sidebar-wrapper>
    <dui-sidebar>
        <dui-sidebar-header>
            <dui-sidebar-menu>
                <dui-sidebar-menu-item>
                    <dui-sidebar-menu-link href="#" size="SidebarMenuLinkSize.Large">
                        <div
                            class="flex aspect-square size-8 items-center justify-center rounded-lg bg-primary text-primary-foreground">
                            <dui-icon name="compass"/>
                        </div>
                        <div class="grid flex-1 text-left text-sm leading-tight">
                            <span class="truncate font-semibold">Voyager Travel</span>
                            <span class="truncate text-xs text-muted-foreground">Admin Console</span>
                        </div>
                    </dui-sidebar-menu-link>
                </dui-sidebar-menu-item>
            </dui-sidebar-menu>
        </dui-sidebar-header>

        <dui-sidebar-content>
            <dui-sidebar-group>
                <dui-sidebar-group-label>Platform</dui-sidebar-group-label>
                <dui-sidebar-group-content>
                    <dui-sidebar-menu>
                        @* Active item — points back at this page so it resolves as the active route. *@
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="layout-dashboard"/>
                                <span>Dashboard</span>
                            </dui-sidebar-menu-link>
                        </dui-sidebar-menu-item>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="ticket"/>
                                <span>Bookings</span>
                            </dui-sidebar-menu-link>
                            <dui-sidebar-menu-badge>12</dui-sidebar-menu-badge>
                        </dui-sidebar-menu-item>
                        @* Menu item with a nested sub-menu. *@
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="map-pinned"/>
                                <span>Destinations</span>
                            </dui-sidebar-menu-link>
                            <dui-sidebar-menu-sub>
                                <dui-sidebar-menu-sub-item>
                                    <dui-sidebar-menu-sub-link href="#"><span>Europe</span>
                                    </dui-sidebar-menu-sub-link>
                                </dui-sidebar-menu-sub-item>
                                <dui-sidebar-menu-sub-item>
                                    <dui-sidebar-menu-sub-link href="#"><span>Asia Pacific</span>
                                    </dui-sidebar-menu-sub-link>
                                </dui-sidebar-menu-sub-item>
                                <dui-sidebar-menu-sub-item>
                                    <dui-sidebar-menu-sub-link href="#"><span>The Americas</span>
                                    </dui-sidebar-menu-sub-link>
                                </dui-sidebar-menu-sub-item>
                            </dui-sidebar-menu-sub>
                        </dui-sidebar-menu-item>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="users"/>
                                <span>Customers</span>
                            </dui-sidebar-menu-link>
                        </dui-sidebar-menu-item>
                    </dui-sidebar-menu>
                </dui-sidebar-group-content>
            </dui-sidebar-group>

            <dui-sidebar-group>
                <dui-sidebar-group-label>Operations</dui-sidebar-group-label>
                <dui-sidebar-group-content>
                    <dui-sidebar-menu>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="plane-takeoff"/>
                                <span>Flights</span>
                            </dui-sidebar-menu-link>
                            <dui-sidebar-menu-badge>3</dui-sidebar-menu-badge>
                        </dui-sidebar-menu-item>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="hotel"/>
                                <span>Hotels</span>
                            </dui-sidebar-menu-link>
                        </dui-sidebar-menu-item>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="calendar-days"/>
                                <span>Itineraries</span>
                            </dui-sidebar-menu-link>
                        </dui-sidebar-menu-item>
                    </dui-sidebar-menu>
                </dui-sidebar-group-content>
            </dui-sidebar-group>

            <dui-sidebar-separator/>

            <dui-sidebar-group>
                <dui-sidebar-group-label>Insights</dui-sidebar-group-label>
                <dui-sidebar-group-content>
                    <dui-sidebar-menu>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="chart-line"/>
                                <span>Reports</span>
                            </dui-sidebar-menu-link>
                        </dui-sidebar-menu-item>
                        <dui-sidebar-menu-item>
                            <dui-sidebar-menu-link href="#">
                                <dui-icon name="file-text"/>
                                <span>Invoices</span>
                            </dui-sidebar-menu-link>
                        </dui-sidebar-menu-item>
                    </dui-sidebar-menu>
                </dui-sidebar-group-content>
            </dui-sidebar-group>
        </dui-sidebar-content>

        <dui-sidebar-footer>
            <dui-sidebar-menu>
                <dui-sidebar-menu-item>
                    <dui-dropdown-menu id="nav-user-menu">
                        <dui-sidebar-menu-button size="SidebarMenuButtonSize.Large" popovertarget="nav-user-menu" aria-haspopup="menu">
                            <div
                                class="flex aspect-square size-8 items-center justify-center rounded-lg bg-muted text-foreground">
                                <dui-icon name="user"/>
                            </div>
                            <div class="grid flex-1 text-left text-sm leading-tight">
                                <span class="truncate font-semibold">Amelia Hart</span>
                                <span class="truncate text-xs text-muted-foreground">amelia@voyager.travel</span>
                            </div>
                            <dui-icon name="chevrons-up-down" class="ml-auto"/>
                        </dui-sidebar-menu-button>
                        <dui-dropdown-menu-content class="w-56" position="PositionArea.RightSpanTop">
                            <dui-dropdown-menu-label class="p-0 font-normal">
                                <div class="flex items-center gap-2 px-1 py-1.5 text-left text-sm">
                                    <div
                                        class="flex aspect-square size-8 items-center justify-center rounded-lg bg-muted text-foreground">
                                        <dui-icon name="user"/>
                                    </div>
                                    <div class="grid flex-1 text-left text-sm leading-tight">
                                        <span class="truncate font-semibold">Amelia Hart</span>
                                        <span class="truncate text-xs text-muted-foreground">amelia@voyager.travel</span>
                                    </div>
                                </div>
                            </dui-dropdown-menu-label>
                            <dui-dropdown-menu-separator/>
                            <dui-dropdown-menu-group>
                                <dui-dropdown-menu-item>
                                    <dui-icon name="badge-check"/>
                                    Account
                                </dui-dropdown-menu-item>
                                <dui-dropdown-menu-item>
                                    <dui-icon name="credit-card"/>
                                    Billing
                                </dui-dropdown-menu-item>
                                <dui-dropdown-menu-item>
                                    <dui-icon name="bell"/>
                                    Notifications
                                </dui-dropdown-menu-item>
                            </dui-dropdown-menu-group>
                            <dui-dropdown-menu-separator/>
                            <dui-dropdown-menu-item variant="DropdownMenuItemVariant.Destructive">
                                <dui-icon name="log-out"/>
                                Log out
                            </dui-dropdown-menu-item>
                        </dui-dropdown-menu-content>
                    </dui-dropdown-menu>
                </dui-sidebar-menu-item>
            </dui-sidebar-menu>
        </dui-sidebar-footer>
    </dui-sidebar>

    <dui-sidebar-inset>
        <header class="flex h-14 items-center gap-2 border-b px-4">
            <dui-sidebar-trigger></dui-sidebar-trigger>
            <dui-separator orientation="SeparatorOrientation.Vertical" class="mx-1 h-4"/>
            <span class="text-sm font-medium">Dashboard</span>
        </header>
        <div class="p-4">
            <p class="text-sm text-muted-foreground">
                A full-featured sidebar — header, grouped navigation with labels, an active
                item, badges, a nested sub-menu, a separator, and a user footer. Toggle it
                with the trigger (Ctrl/⌘ + B).
            </p>
        </div>
    </dui-sidebar-inset>
</dui-sidebar-wrapper>
```
