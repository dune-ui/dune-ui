namespace StellarAdmin.Icons;

internal sealed class DefaultIconManager : IIconManager
{
    private IDictionary<string, List<SvgShape>> _icons = new Dictionary<string, List<SvgShape>>();

    public static DefaultIconManager Instance { get; } = new DefaultIconManager();

    private DefaultIconManager() { }

    public void AddIcon(string name, List<SvgShape> shapes)
    {
        _icons.Add(name, shapes);
    }

    public void AddIconPack<TIconPack>()
        where TIconPack : IIconPack, new()
    {
        var iconPack = new TIconPack();

        _icons = new Dictionary<string, List<SvgShape>>([.. _icons, .. iconPack.GetIcons()]);
    }

    public string[] GetIconNames()
    {
        return _icons.Keys.ToArray();
    }

    public bool TryGetIcon(string name, out List<SvgShape>? shapes)
    {
        return _icons.TryGetValue(name, out shapes);
    }
}
