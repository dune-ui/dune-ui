namespace StellarAdmin.Icons;

public interface IIconPack
{
    IDictionary<string, List<SvgShape>> GetIcons();
}
