namespace ComponentPlayground.Pages.Shared;

internal record DemoGroup(string Title, DemoItem[] Items);

internal record DemoItem(string Title, string Page);
