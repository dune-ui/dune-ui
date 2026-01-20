namespace DuneUI.Icons;

internal sealed class DefaultIconManager : IIconManager
{
    private IDictionary<string, IconDefinition> _icons = new Dictionary<string, IconDefinition>();

    public static DefaultIconManager Instance { get; } = new DefaultIconManager();

    private DefaultIconManager() { }

    public void AddIcon(string name, IconDefinition iconDefinition)
    {
        _icons.Add(name, iconDefinition);
    }

    public void AddIconPack<TIconPack>()
        where TIconPack : IIconPack, new()
    {
        var iconPack = new TIconPack();

        _icons = iconPack
            .GetIcons()
            .UnionBy(_icons, x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);
    }

    public string[] GetIconNames()
    {
        return _icons.Keys.ToArray();
    }

    public bool TryGetIcon(string name, out IconDefinition? iconDefinition)
    {
        return _icons.TryGetValue(name, out iconDefinition);
    }
}
