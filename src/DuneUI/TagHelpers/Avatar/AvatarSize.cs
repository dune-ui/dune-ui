namespace DuneUI.TagHelpers;

public enum AvatarSize
{
    Default,
    Small,
    Large,
}

internal static class GetAvatarSizeAttributeText
{
    extension(AvatarSize size)
    {
        public string GetDataAttributeText()
        {
            return size switch
            {
                AvatarSize.Default => "default",
                AvatarSize.Small => "sm",
                AvatarSize.Large => "lg",
                _ => string.Empty
            };
        }
    }
}
