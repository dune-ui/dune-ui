namespace StellarAdmin.Icons;

public interface IIconManager
{
    string[] GetIconNames();

    bool TryGetIcon(string name, out List<SvgShape>? shapes);
}
