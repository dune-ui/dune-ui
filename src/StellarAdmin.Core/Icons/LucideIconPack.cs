using StellarAdmin.Icons.Lucide;

namespace StellarAdmin.Icons;

public class LucideIconPack : IIconPack
{
    public IDictionary<string, List<SvgShape>> GetIcons()
    {
        return LucideIcons.IconDefinitions;
    }
}
