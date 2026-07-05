---
name: duneui-forms
description: >-
  Builds accessible, model-bound forms with DuneUI tag helpers in ASP.NET Core MVC / Razor
  Pages — fields with labels/descriptions/validation, asp-for model binding, validation
  messages, field groups and fieldsets, and the input family (text, select, textarea, switch,
  checkbox, radio, OTP). Use when creating or editing a form in a .cshtml/.razor file that uses
  DuneUI, or when the user mentions DuneUI forms, fields, inputs, model binding, or validation.
metadata:
  author: DuneUI
paths:
  - "**/*.cshtml"
  - "**/*.razor"
---

# Building forms with DuneUI

This skill covers the *patterns* for DuneUI forms. For a specific component's attributes and
values, open its file under `../duneui/references/components/` (e.g. `field.md`, `input.md`,
`select.md`). Read `../duneui/references/conventions.md` for the cross-cutting rules (enums are
fully-qualified, `class` wins, etc.).

## The field is the unit of a form

A form field is a `<dui-field>` composing, **in this order**: label → input → description →
error.

```razor
<dui-field>
    <dui-field-label for="email">Email</dui-field-label>
    <dui-input id="email" type="email" placeholder="you@example.com" />
    <dui-field-description>Where we send your confirmation.</dui-field-description>
    <dui-field-error>Enter a valid email address.</dui-field-error>
</dui-field>
```

Any DuneUI input goes in the input slot: `<dui-input>`, `<dui-select>`, `<dui-textarea>`,
`<dui-switch>`, `<dui-input-otp>`.

## Explicit vs implicit — prefer implicit for model-bound forms

DuneUI inputs can **render their own wrapping field** (label + description + error) so you don't
write `<dui-field>` by hand. This is the default, preferred style for model-bound forms.

An input renders an implicit field when **any** of `asp-for`, `label`, `description`, or `error`
is present — **unless** it is already inside a `<dui-field>` (it never double-wraps). Force it
either way with `render-field="true|false"`.

**Implicit + model binding (the recommended default):**

```razor
@model CheckoutModel
<dui-field-set>
    <dui-field-group>
        <dui-input asp-for="Email" />
        <dui-input asp-for="Password" />
        <dui-select asp-for="CabinClass" asp-items="@Html.GetEnumSelectList<CabinClass>()"></dui-select>
        <dui-textarea asp-for="Notes" />
    </dui-field-group>
</dui-field-set>
```

The label, description, placeholder, and input type all come from the model:

```csharp
[Display(Name = "Email address",
         Description = "Where we send your booking confirmation",
         Prompt = "you@example.com")]   // Prompt -> placeholder
[DataType(DataType.EmailAddress)]        // DataType -> input type
[Required]
public string? Email { get; set; }
```

**Implicit without a model** — supply the copy via attributes:

```razor
<dui-input label="Email address"
           description="Where we send your confirmation"
           placeholder="you@example.com"
           type="email" />
```

**Explicit** — use when you need full control over structure/markup (write every part yourself,
as in the first example above). Inside an explicit `<dui-field>`, inputs never auto-wrap.

## Validation

The validation message element is **`<dui-field-error>`**.

**Automatic (model-bound) — preferred.** With a validated model property, an implicit input
renders its error on its own; there's nothing else to wire:

```razor
<dui-input asp-for="Email" />   @* shows the model error for Email when invalid *@
```

In an explicit field, bind the error to the same property:

```razor
<dui-field>
    <dui-field-label asp-for="BedType">Bed type</dui-field-label>
    <dui-input asp-for="BedType" type="radio" value="king" label="1 King" />
    <dui-field-error asp-for="BedType" />
</dui-field>
```

Server side, standard model validation applies (`[Required]`, `ModelState`, etc.):

```csharp
if (!ModelState.IsValid) return Page();
```

**Manual.** Set `aria-invalid="true"` on the input and provide the message — explicitly:

```razor
<dui-input aria-invalid="true" type="email" />
<dui-field-error>Enter your email address</dui-field-error>
```

…or implicitly via the `error` attribute:

```razor
<dui-input label="Email address" error="Enter your email address" aria-invalid="true" type="email" />
```

(jQuery-unobtrusive-validation's `input-validation-error` class also triggers the error styling.)

## Grouping: `<dui-field-group>` and `<dui-field-set>`

- `<dui-field-group>` — spacing container for a set of fields (nestable).
- `<dui-field-set>` — a semantic fieldset; holds a `<dui-field-legend>` (heading) and optional
  `<dui-field-description>`, then a `<dui-field-group>`.
- `<dui-field-separator />` — divider between fieldsets.

```razor
<dui-field-set>
    <dui-field-legend>Address</dui-field-legend>
    <dui-field-description>We use this to deliver your tickets.</dui-field-description>
    <dui-field-group>
        <dui-input asp-for="Street" />
        <div class="grid grid-cols-2 gap-4">
            <dui-input asp-for="City" />
            <dui-input asp-for="PostalCode" />
        </div>
    </dui-field-group>
</dui-field-set>
```

## The input family (what tag to use)

| Need | Use |
|------|-----|
| Text / email / password / number / date… | `<dui-input>` with `type="…"` (or `[DataType]` via `asp-for`) |
| **Checkbox** | `<dui-input type="checkbox" asp-for="…">` — there is **no** `<dui-checkbox>` tag |
| **Radio** | `<dui-input type="radio" asp-for="…" value="…" label="…">` — no `<dui-radio>` tag |
| Dropdown | `<dui-select asp-for="…" asp-items="…">` (supports inline `<option>` / `<optgroup>`) |
| Multi-line | `<dui-textarea asp-for="…">` |
| On/off toggle | `<dui-switch asp-for="…">` |
| One-time code | `<dui-input-otp asp-for="…" groups="3,3">` |

**Checkbox / radio groups:** put `data-slot="checkbox-group"` (or `radio-group`) on the enclosing
`<dui-field-group>`, inside a `<dui-field-set>` with a `<dui-field-legend>`, one
`<dui-input asp-for="…">` per option.

**Horizontal fields** (checkbox/switch/radio rows): set `orientation="FieldOrientation.Horizontal"`
on the `<dui-field>` and wrap the label + description in `<dui-field-content>`.

## Rules & gotchas

1. **Prefer implicit + `asp-for`** for model-bound forms; fall back to explicit `<dui-field>`
   only when you need custom structure.
2. Implicit wrapping fires on `asp-for`/`label`/`description`/`error`, but **not** inside an
   existing `<dui-field>`. Override with `render-field`.
3. The error tag is `<dui-field-error>`. Bind `asp-for` for automatic validation; otherwise set
   `aria-invalid="true"` and supply the message.
4. Checkboxes and radios are `<dui-input type="checkbox|radio">`, not dedicated tags.
5. Explicit field order is label → input → description → error.
6. Enum attributes are fully-qualified (`FieldOrientation.Horizontal`).
7. Wrap the form in a standard `<form method="post">`; DuneUI adds no form element of its own.
