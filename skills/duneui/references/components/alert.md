---
component: Alert
tags: [dui-alert, dui-alert-action, dui-alert-description, dui-alert-title]
generated: true
---

# Alert

A callout that displays a short, important message to the user, optionally with an icon, title, and description.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-alert>` | A callout that displays a short, important message to the user, optionally with an icon, title, and description. |
| `<dui-alert-action>` | A region within an alert for interactive elements such as buttons or links. |
| `<dui-alert-description>` | The descriptive body text of an alert, shown beneath the title. |
| `<dui-alert-title>` | The title heading of an alert. |

## Attributes

### `<dui-alert>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `description` | `string` | — | — |
| `icon` | `string` | — | — |
| `title` | `string` | — | — |
| `variant` | `AlertVariant` | `Default` | `Default`, `Destructive` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Examples

*From `Pages/Alert/_Intro.cshtml`*

```razor
<dui-alert>
    <dui-icon name="circle-check"/>
    <dui-alert-title>Booking Confirmed</dui-alert-title>
    <dui-alert-description>
        <p>Your trip to Paris has been successfully booked. Check your email for your e-tickets and itinerary.</p>
    </dui-alert-description>
    <dui-alert-action>
        <dui-button size="ButtonSize.ExtraSmall" variant="ButtonVariant.Outline">
            <dui-icon name="tickets-plane"/>
            View Tickets
        </dui-button>
    </dui-alert-action>
</dui-alert>
```

*From `Pages/Alert/_Actions.cshtml`*

```razor
<div class="mx-auto flex w-full max-w-lg flex-col gap-4">
    <dui-alert>
        <dui-icon name="circle-alert"/>
        <dui-alert-title>The selected emails have been marked as spam.</dui-alert-title>
        <dui-alert-action>
            <dui-button size="ButtonSize.ExtraSmall">Undo</dui-button>
        </dui-alert-action>
    </dui-alert>
    <dui-alert>
        <dui-icon name="circle-alert"/>
        <dui-alert-title>The selected emails have been marked as spam.</dui-alert-title>
        <dui-alert-description>
            This is a very long alert title that demonstrates how the component
            handles extended text content.
        </dui-alert-description>
        <dui-alert-action>
            <dui-badge variant="BadgeVariant.Secondary">Badge</dui-badge>
        </dui-alert-action>
    </dui-alert>
</div>
```
