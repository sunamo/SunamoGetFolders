namespace SunamoGetFolders._sunamo;

internal class SH
{
    internal static void FirstCharUpper(ref string text)
    {
        text = FirstCharUpper(text);
    }

    internal static string FirstCharUpper(string text)
    {
        if (text.Length == 1)
        {
            return text.ToUpper();
        }

        var restOfText = text.Substring(1);
        return text[0].ToString().ToUpper() + restOfText;
    }
}
