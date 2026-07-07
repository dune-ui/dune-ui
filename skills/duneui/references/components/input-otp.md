---
component: InputOtp
tags: [dui-input-otp, dui-input-otp-group, dui-input-otp-separator, dui-input-otp-slot]
generated: true
---

# InputOtp

A segmented one-time-code input. A single real `<input>` holds the whole code and posts it as one form value; the presentational slot cells (one per character) display the value and the active caret. The `del-input-otp` web component distributes the value into the cells and drives the active / caret state once hydrated.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-input-otp>` | A segmented one-time-code input. A single real `<input>` holds the whole code and posts it as one form value; the presentational slot cells (one per character) display the value and the active caret. The `del-input-otp` web component distributes the value into the cells and drives the active / caret state once hydrated. |
| `<dui-input-otp-group>` | A group of `dui-input-otp-slot`s within a `dui-input-otp`. Groups are separated by a `dui-input-otp-separator`. |
| `<dui-input-otp-separator>` | A separator placed between `dui-input-otp-group`s. Renders a Lucide `minus` icon by default; supply child content to override it. |
| `<dui-input-otp-slot>` | A single presentational slot cell within a `dui-input-otp`. Displays one character of the code and the active caret. The slot self-assigns its index from the parent's running counter unless an explicit `index` is set. |

## Attributes

### `<dui-input-otp>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `max-length` | `int` | — | — |
| `pattern` | `string` | — | — |
| `groups` | `string` | — | — |
| `inputmode` | `string` | — | — |
| `disabled` | `bool` | `false` | `true`, `false` |
| `aria-invalid` | `bool` | — | `true`, `false` |
| `value` | `string` | — | — |
| `form` | `string` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-input-otp-slot>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `index` | `int` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Examples

*From `Pages/InputOtp/_Intro.cshtml`*

```razor
<dui-input-otp max-length="6"/>
```

*From `Pages/InputOtp/_Composition.cshtml`*

```razor
<dui-input-otp max-length="6">
    <dui-input-otp-group>
        <dui-input-otp-slot/>
        <dui-input-otp-slot/>
        <dui-input-otp-slot/>
    </dui-input-otp-group>
    <dui-input-otp-separator/>
    <dui-input-otp-group>
        <dui-input-otp-slot/>
        <dui-input-otp-slot/>
        <dui-input-otp-slot/>
    </dui-input-otp-group>
</dui-input-otp>
```

*From `Pages/InputOtp/_ModelBinding.cshtml`*

```razor
<dui-input-otp asp-for="OneTimePassword" groups="3,3"/>
```
