---
component: Accordion
tags: [dui-accordion, dui-accordion-item, dui-accordion-item-content, dui-accordion-item-title]
generated: true
---

# Accordion

A vertically stacked set of collapsible items, each of which can be expanded to reveal its content.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-accordion>` | A vertically stacked set of collapsible items, each of which can be expanded to reveal its content. |
| `<dui-accordion-item>` | A single collapsible item within an accordion, rendered as a native `<details>` element with a title and content region. |
| `<dui-accordion-item-content>` | The content region of an accordion item, revealed when the item is expanded. |
| `<dui-accordion-item-title>` | The clickable header of an accordion item that toggles the item open and closed. |

## Examples

*From `Pages/Accordion/_Intro.cshtml`*

```razor
<dui-accordion>
    <dui-accordion-item>
        <dui-accordion-item-title>
            What payment methods do you accept?
        </dui-accordion-item-title>
        <dui-accordion-item-content>
            We accept all major credit and debit cards (Visa, Mastercard, Amex) and popular digital wallets like Apple
            Pay and Google Pay for your convenience.
        </dui-accordion-item-content>
    </dui-accordion-item>
    <dui-accordion-item>
        <dui-accordion-item-title>
            When will I receive my booking confirmation?
        </dui-accordion-item-title>
        <dui-accordion-item-content>
            Your confirmation and e-tickets are usually emailed within minutes. If not, please check your spam folder or
            view the details in your account's "My Trips" section.
        </dui-accordion-item-content>
    </dui-accordion-item>
    <dui-accordion-item>
        <dui-accordion-item-title>
            Can I earn loyalty points or frequent flyer miles?
        </dui-accordion-item-title>
        <dui-accordion-item-content>
            Yes, you can enter your frequent flyer number during flight booking. For hotels and packages, eligibility
            depends on the specific service provider's loyalty program rules.
        </dui-accordion-item-content>
    </dui-accordion-item>
</dui-accordion>
```

*From `Pages/Accordion/_Single.cshtml`*

```razor
<dui-accordion>
    <dui-accordion-item name="faq">
        <dui-accordion-item-title>
            What payment methods do you accept?
        </dui-accordion-item-title>
        <dui-accordion-item-content>
            We accept all major credit and debit cards (Visa, Mastercard, Amex) and popular digital wallets like Apple
            Pay and Google Pay for your convenience.
        </dui-accordion-item-content>
    </dui-accordion-item>
    <dui-accordion-item name="faq">
        <dui-accordion-item-title>
            When will I receive my booking confirmation?
        </dui-accordion-item-title>
        <dui-accordion-item-content>
            Your confirmation and e-tickets are usually emailed within minutes. If not, please check your spam folder or
            view the details in your account's "My Trips" section.
        </dui-accordion-item-content>
    </dui-accordion-item>
    <dui-accordion-item name="faq">
        <dui-accordion-item-title>
            Can I earn loyalty points or frequent flyer miles?
        </dui-accordion-item-title>
        <dui-accordion-item-content>
            Yes, you can enter your frequent flyer number during flight booking. For hotels and packages, eligibility
            depends on the specific service provider's loyalty program rules.
        </dui-accordion-item-content>
    </dui-accordion-item>
</dui-accordion>
```
