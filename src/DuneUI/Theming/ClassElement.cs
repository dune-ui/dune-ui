namespace DuneUI.Theming;

public abstract record ClassElement
{
    public static implicit operator ClassElement(string? classes)
    {
        if (classes == null)
        {
            return new ClassList(string.Empty);
        }

        return new ClassList(classes);
    }
}

public record ClassList(string Classes) : ClassElement;

public record ComponentName(string Name) : ClassElement;
