---
component: Collapsible
tags: [dui-collapsible]
generated: true
---

# Collapsible

A container whose content can be expanded or collapsed. Toggle it with the Invoker Commands API — a trigger button carrying `commandfor` and a custom command.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Examples

*From `Pages/Collapsible/_Intro.cshtml`*

```razor
<div class="flex w-[350px] flex-col gap-2 group/collapsible">
    <div class="flex items-center justify-between gap-4">
        <h4 class="text-sm font-semibold">
            @@ibnbattuta has 3 bookings
        </h4>
        <dui-button variant="ButtonVariant.Ghost" size="ButtonSize.Icon" commandfor="my-collapsible" command="--toggle" class="size-8">
            <dui-icon name="chevrons-up-down" class="in-aria-expanded:hidden"/>
            <dui-icon name="chevrons-down-up" class="not-in-aria-expanded:hidden"/>
            <span class="sr-only">Toggle</span>
        </dui-button>
    </div>
    <dui-item variant="ItemVariant.Outline" size="ItemSize.Small">
        <dui-item-media>
            <img
                class="object-cover size-12 rounded"
                src="https://images.unsplash.com/photo-1681785841804-4d8a976ee892?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=2670"/>
        </dui-item-media>
        <dui-item-content>
            <dui-item-title>Non-stop to NYC (JFK)</dui-item-title>
            <dui-item-description>Departure 8AM - Dec 24, 2025</dui-item-description>
        </dui-item-content>
    </dui-item>
    <dui-collapsible id="my-collapsible" hidden="" class="flex flex-col gap-2">
        <dui-item variant="ItemVariant.Outline" size="ItemSize.Small">
            <dui-item-media>
                <img
                    class="object-cover size-12 rounded"
                    src="https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=2670"/>
            </dui-item-media>
            <dui-item-content>
                <dui-item-title>The Grand Resort & Spa</dui-item-title>
                <dui-item-description>Check-in Dec 25 | 7 Nights</dui-item-description>
            </dui-item-content>
        </dui-item>
        <dui-item variant="ItemVariant.Outline" size="ItemSize.Small">
            <dui-item-media>
                <img
                    class="object-cover size-12 rounded"
                    src="https://images.unsplash.com/photo-1557223562-6c77ef16210f?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&q=80&w=2670"/>
            </dui-item-media>
            <dui-item-content>
                <dui-item-title>Westminster Abbey Tour</dui-item-title>
                <dui-item-description>Dec 26, 11:00 AM Slot | 2 Adult Tickets</dui-item-description>
            </dui-item-content>
        </dui-item>
    </dui-collapsible>
</div>
```

*From `Pages/Collapsible/_Settings.cshtml`*

```razor
<dui-card class="mx-auto w-[350px]" size="CardSize.Small">
  <dui-card-header>
    <dui-card-title>Radius</dui-card-title>
    <dui-card-description>
      Set the corner radius of the element.
    </dui-card-description>
  </dui-card-header>
  <dui-card-content>
    <div class="flex items-start gap-2 group/collapsible">
      <dui-field-group class="grid w-full grid-cols-2 gap-2">
        <dui-field>
          <dui-field-label for="radius-x-1" class="sr-only">
            Radius X
          </dui-field-label>
          <dui-input id="radius-x-1" placeholder="0"/>
        </dui-field>
        <dui-field>
          <dui-field-label for="radius-y-1" class="sr-only">
            Radius Y
          </dui-field-label>
          <dui-input id="radius-y-1" placeholder="0"/>
        </dui-field>
        <dui-collapsible id="collapsible-settings" hidden="" class="col-span-full grid grid-cols-subgrid gap-2">
          <dui-field>
            <dui-field-label for="radius-x-2" class="sr-only">
              Radius X
            </dui-field-label>
            <dui-input id="radius-x-2" placeholder="0"/>
          </dui-field>
          <dui-field>
            <dui-field-label for="radius-y-2" class="sr-only">
              Radius Y
            </dui-field-label>
            <dui-input id="radius-y-2" placeholder="0"/>
          </dui-field>
        </dui-collapsible>
      </dui-field-group>
      <dui-button variant="ButtonVariant.Outline" size="ButtonSize.Icon" commandfor="collapsible-settings" command="--toggle">
        <dui-icon name="minimize" class="not-in-aria-expanded:hidden"/>
        <dui-icon name="maximize" class="in-aria-expanded:hidden"/>
      </dui-button>
    </div>
  </dui-card-content>
</dui-card>
```
