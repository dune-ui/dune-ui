namespace DuneUI.Icons;

public class LucideIconPack : IIconPack
{
    public IDictionary<string, IconDefinition> GetIcons()
    {
        return LucideIcons.IconDefinitions;
    }
}
