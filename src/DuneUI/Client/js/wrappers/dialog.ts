export function dialog<TData = Record<string, any>>(
  selector: string | HTMLDialogElement,
  { clearReturnValueOnOpen = true }: { clearReturnValueOnOpen?: boolean } = {},
) {
  const el =
    typeof selector === "string" ? document.querySelector<HTMLDialogElement>(selector) : selector;

  if (!el) throw new Error(`Dialog not found: ${selector}`);
  if (!(el instanceof HTMLDialogElement)) throw new Error(`Element is not a dialog: ${selector}`);

  let _resolve:
    | ((result: { confirmed: boolean; returnValue: string; data: TData | null }) => void)
    | null = null;

  return {
    showAsync(): Promise<{ confirmed: boolean; returnValue: string; data: TData | null }> {
      return new Promise((resolve) => {
        let dismissed = false;
        _resolve = resolve;

        el.addEventListener(
          "cancel",
          () => {
            dismissed = true;
          },
          { once: true },
        );
        el.addEventListener(
          "close",
          () => {
            if (!_resolve) return;
            if (dismissed || el.returnValue === "cancel") {
              _resolve({ confirmed: false, returnValue: el.returnValue, data: null });
            } else {
              const form = el.querySelector("form");
              const data = form ? (Object.fromEntries(new FormData(form)) as TData) : null;

              _resolve({ confirmed: true, returnValue: el.returnValue, data });
            }
            _resolve = null;
          },
          { once: true },
        );

        if (clearReturnValueOnOpen == null || clearReturnValueOnOpen) el.returnValue = "";

        el.showModal();
      });
    },
    confirm(data?: TData) {
      if (!_resolve) throw new Error("No dialog is currently open.");

      if (!data) {
        const form = el.querySelector("form");
        data = form ? (Object.fromEntries(new FormData(form)) as TData) : undefined;
      }

      const resolve = _resolve;
      _resolve = null;

      el.close();

      resolve({ confirmed: true, returnValue: el.returnValue, data: data ?? null });
    },
    cancel() {
      if (!_resolve) throw new Error("No dialog is currently open.");

      const resolve = _resolve;
      _resolve = null;

      el.close();

      resolve({ confirmed: false, returnValue: el.returnValue, data: null });
    },
  };
}
