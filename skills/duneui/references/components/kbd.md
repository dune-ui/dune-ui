---
component: Kbd
tags: [dui-kbd, dui-kbd-group]
generated: true
---

# Kbd

Displays a single keyboard key or keystroke.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-kbd>` | Displays a single keyboard key or keystroke. |
| `<dui-kbd-group>` | Groups several `<dui-kbd>` elements to represent a keyboard shortcut or key sequence. |

## Example

*From `Pages/Kbd/_Intro.cshtml`*

```razor
<dui-kbd-group>
    <dui-kbd>⌘</dui-kbd>
    <dui-kbd>⇧</dui-kbd>
    <dui-kbd>⌥</dui-kbd>
    <dui-kbd>⌃</dui-kbd>
</dui-kbd-group>
<dui-kbd-group>
    <dui-kbd>Ctrl</dui-kbd>
    <span>+</span>
    <dui-kbd>B</dui-kbd>
</dui-kbd-group>
```
