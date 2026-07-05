---
component: Field
tags: [dui-field, dui-field-content, dui-field-description, dui-field-error, dui-field-group, dui-field-label, dui-field-legend, dui-field-separator, dui-field-set, dui-field-title]
generated: true
---

# Field

Wraps a form control together with its label, description, and error message, arranging them according to the chosen orientation.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-field>` | Wraps a form control together with its label, description, and error message, arranging them according to the chosen orientation. |
| `<dui-field-content>` | A container that holds a field's label and description, keeping them stacked together when the field is laid out horizontally alongside its control. |
| `<dui-field-description>` | Supporting help text for a field. Renders its own content, or falls back to the description from the model metadata when bound with `asp-for`. |
| `<dui-field-error>` | Displays the validation error message for a field. When bound with `asp-for`, it shows the model's validation message and appears only when that field is invalid. |
| `<dui-field-group>` | Groups a set of related fields together, arranging them in a column with consistent spacing. |
| `<dui-field-label>` | The label for a field's control, rendered as a `<label>` element. |
| `<dui-field-legend>` | The caption for a field set, rendered as a `<legend>` element. |
| `<dui-field-separator>` | A horizontal divider between fields, optionally with content (such as a label) centered on the line. |
| `<dui-field-set>` | Groups related fields under a common legend, rendered as a `<fieldset>` element. |
| `<dui-field-title>` | A title for a field or field set that is styled like a label but is not associated with a control. |

## Attributes

### `<dui-field>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `orientation` | `FieldOrientation` | `Vertical` | `Vertical`, `Horizontal`, `Responsive` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

> In Razor, enum values are written fully-qualified, e.g. `variant="ButtonVariant.Outline"`.

### `<dui-field-description>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `for` | `ModelExpression` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-field-error>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `for` | `ModelExpression` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-field-label>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `for` | `ModelExpression` | — | — |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

### `<dui-field-legend>`

| Attribute | Type | Default | Values |
|-----------|------|---------|--------|
| `variant` | `FieldLegendVariant` | `Legend` | `Legend`, `Label` |
| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |

## Example

*From `Pages/Field/_Intro.cshtml`*

```razor
<dui-field-group>
  <dui-field-set>
    <dui-field-legend>Payment Method</dui-field-legend>
    <dui-field-description>
      All transactions are secure and encrypted
    </dui-field-description>
    <dui-field-group>
      <dui-field>
        <dui-field-label for="card-name">
          Name on Card
        </dui-field-label>
        <dui-input
          id="card-name"
          placeholder="Ibn Battuta"
        />
      </dui-field>
      <dui-field>
        <dui-field-label for="card-number">
          Card Number
        </dui-field-label>
        <dui-input
          id="card-number"
          placeholder="1234 5678 9012 3456"
        />
        <dui-field-description>
          Enter your 16-digit card number
        </dui-field-description>
      </dui-field>
      <div class="grid grid-cols-3 gap-4">
        <dui-field>
          <dui-field-label for="exp-month">
            Month
          </dui-field-label>
          <dui-select id="exp-month">
            <option value="01">01</option>
            <option value="02">02</option>
            <option value="03">03</option>
            <option value="04">04</option>
            <option value="05">05</option>
            <option value="06">06</option>
            <option value="07">07</option>
            <option value="08">08</option>
            <option value="09">09</option>
            <option value="10">10</option>
            <option value="11">11</option>
            <option value="12">12</option>
          </dui-select>
        </dui-field>
        <dui-field>
          <dui-field-label for="exp-year">
            Year
          </dui-field-label>
          <dui-select id="exp-year">
            <option value="2024">2024</option>
            <option value="2025">2025</option>
            <option value="2026">2026</option>
            <option value="2027">2027</option>
            <option value="2028">2028</option>
            <option value="2029">2029</option>
          </dui-select>
        </dui-field>
        <dui-field>
          <dui-field-label for="cvv">CVV</dui-field-label>
          <dui-input id="cvv" placeholder="123"/>
        </dui-field>
      </div>
    </dui-field-group>
  </dui-field-set>
  <dui-field-separator/>
  <dui-field-set>
    <dui-field-legend>Billing Address</dui-field-legend>
    <dui-field-description>
      The billing address associated with your payment method
    </dui-field-description>
    <dui-field-group>
      <dui-field orientation="FieldOrientation.Horizontal">
        <dui-input
          type="checkbox"
          id="same-as-shipping"
        />
        <dui-field-label
          for="same-as-shipping"
          class="font-normal"
        >
          Same as shipping address
        </dui-field-label>
      </dui-field>
    </dui-field-group>
  </dui-field-set>
  <dui-field-set>
    <dui-field-group>
      <dui-field>
        <dui-field-label for="optional-comments">
          Comments
        </dui-field-label>
        <dui-textarea
          id="optional-comments"
          placeholder="Add any additional comments"
          class="resize-none"
        />
      </dui-field>
    </dui-field-group>
  </dui-field-set>
  <dui-field orientation="FieldOrientation.Horizontal">
    <dui-button type="button">Submit</dui-button>
    <dui-button variant="ButtonVariant.Outline" type="button">
      Cancel
    </dui-button>
  </dui-field>
</dui-field-group>
```
