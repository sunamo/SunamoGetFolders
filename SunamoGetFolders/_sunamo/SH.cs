namespace SunamoGetFolders._sunamo;

internal class SH
{
    /// <summary>
    /// Converts the first character of the text to uppercase
    /// </summary>
    /// <param name="text">The text to modify (passed by reference)</param>
    internal static void FirstCharUpper(ref string text)
    {
        text = FirstCharUpper(text);
    }

    /// <summary>
    /// Converts the first character of the text to uppercase
    /// </summary>
    /// <param name="text">The text to modify</param>
    /// <returns>Text with uppercase first character</returns>
    internal static string FirstCharUpper(string text)
    {
        if (text.Length == 1)
        {
            return text.ToUpper();
        }

        string restOfText = text.Substring(1);
        return text[0].ToString().ToUpper() + restOfText;
    }
}