namespace StellarUI.Icons;

public interface IIconPack
{
    IDictionary<string, List<SvgShape>> GetIcons();
}
