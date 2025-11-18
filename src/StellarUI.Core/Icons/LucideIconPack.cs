namespace StellarUI.Icons.Lucide;

public class LucideIconPack : IIconPack
{
    public IDictionary<string, List<SvgShape>> GetIcons()
    {
        return LucideIcons.IconDefinitions;
    }
}
