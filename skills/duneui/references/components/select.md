---
component: Select
tags: [dui-select]
generated: true
---

# Select

A styled dropdown for choosing a single option, wrapping a native `<select>` element with a custom chevron icon.

## Attributes

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `asp-items` | `IEnumerable<SelectListItem>` | — | — |
| `size` | `SelectSize` | `Default` | `Default`, `Small` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

## Examples

*From `Pages/Select/_Intro.cshtml`*

```razor
<dui-select>
    <option value="">-- Select cabin class --</option>
    <option value="economy">Economy</option>
    <option value="premium-economy">Premium Economy</option>
    <option value="business">Business</option>
    <option value="first">First</option>
</dui-select>
```

*From `Pages/Select/_ModelBinding.cshtml`*

```razor
<dui-select asp-for="CabinClass" asp-items="@Html.GetEnumSelectList<CabinClass>()">
</dui-select>
```

*From `Pages/Select/_Groups.cshtml`*

```razor
<dui-select>
    <option value="">-- Select your program --</option>
    <optgroup label="Star Alliance">
        <option value="united">United MileagePlus</option>
        <option value="lufthansa">Lufthansa Miles & More</option>
        <option value="air-canada">Air Canada Aeroplan</option>
        <option value="ana">ANA Mileage Club</option>
        <option value="singapore">Singapore Airlines KrisFlyer</option>
    </optgroup>
    <optgroup label="Oneworld">
        <option value="american">American Airlines AAdvantage</option>
        <option value="british">British Airways Executive Club</option>
        <option value="cathay">Cathay Pacific Asia Miles</option>
        <option value="qantas">Qantas Frequent Flyer</option>
        <option value="qatar">Qatar Airways Privilege Club</option>
    </optgroup>
    <optgroup label="SkyTeam">
        <option value="delta">Delta SkyMiles</option>
        <option value="air-france">Air France-KLM Flying Blue</option>
        <option value="korean">Korean Air SKYPASS</option>
        <option value="aeromexico">Aeromexico Club Premier</option>
        <option value="virgin-atlantic">Virgin Atlantic Flying Club</option>
    </optgroup>
    <optgroup label="Other Partners">
        <option value="emirates">Emirates Skywards</option>
        <option value="etihad">Etihad Guest</option>
        <option value="jetblue">JetBlue TrueBlue</option>
    </optgroup>
</dui-select>
```
