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

## Examples

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

*From `Pages/Kbd/_KbdGroup.cshtml`*

```razor
<dui-kbd-group>
    <dui-kbd>Ctrl</dui-kbd>
    <dui-kbd>Shift</dui-kbd>
    <dui-kbd>P</dui-kbd>
</dui-kbd-group>
```

*From `Pages/Kbd/_InputGroup.cshtml`*

```razor
<dui-input-group>
    <dui-input-group-input />
    <dui-input-group-addon>
        <dui-kbd>Space</dui-kbd>
    </dui-input-group-addon>
</dui-input-group>
```
