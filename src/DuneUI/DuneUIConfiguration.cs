namespace DuneUI;

public class DuneUIConfiguration
{
    private readonly IDictionary<string, string> _registeredScripts =
        new Dictionary<string, string>();

    private readonly IDictionary<string, string> _registeredStylesheets =
        new Dictionary<string, string>();

    public string[] GetScripts()
    {
        return _registeredScripts.Values.ToArray();
    }

    internal string[] GetStylesheets()
    {
        return _registeredStylesheets.Values.ToArray();
    }

    internal void RegisterNamedScript(string name, string script)
    {
        _registeredScripts[name] = script;
    }

    internal void RegisterNamedStylesheet(string name, string stylesheet)
    {
        _registeredStylesheets[name] = stylesheet;
    }

    internal void RegisterScript(string script)
    {
        _registeredScripts.Add(Guid.CreateVersion7().ToString(), script);
    }

    internal void RegisterStylesheet(string stylesheet)
    {
        _registeredStylesheets.Add(Guid.CreateVersion7().ToString(), stylesheet);
    }
}
