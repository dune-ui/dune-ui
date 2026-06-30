import { dialog } from "./dialog";

/**
 * Confirmation-focused wrapper around {@link dialog} for `<dui-alert-dialog>` elements.
 *
 * An alert dialog's whole purpose is "await a confirmation before doing something", so this
 * exposes a boolean-returning `confirmAsync()` on top of the shared dialog primitive:
 *
 * ```ts
 * if (await window.duneui.alertDialog("#confirm-delete").confirmAsync()) {
 *   await fetch("/items/42", { method: "DELETE" });
 * }
 * ```
 *
 * `confirmAsync()` resolves `true` when the user activates the action button
 * (`returnValue !== "cancel"`) and `false` on cancel or Esc dismissal. `confirm()` / `cancel()`
 * are passed through for manual resolution.
 */
export function alertDialog(selector: string | HTMLDialogElement) {
  const d = dialog(selector);

  return {
    confirmAsync: () => d.showAsync().then((r) => r.confirmed),
    confirm: d.confirm,
    cancel: d.cancel,
  };
}
