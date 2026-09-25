namespace SunamoGetFolders._sunamo;

internal class FS
{
    /// <summary>
    /// Ensures the path ends with a backslash and has uppercase first character
    /// </summary>
    /// <param name="path">The path to modify</param>
    /// <returns>Path with ending backslash and uppercase first character</returns>
    internal static string WithEndSlash(string path)
    {
        return WithEndSlash(ref path);
    }

    /// <summary>
    /// Ensures the path ends with a backslash and has uppercase first character
    /// Usage: Exceptions.FileWasntFoundInDirectory
    /// </summary>
    /// <param name="path">The path to modify (passed by reference)</param>
    /// <returns>Path with ending backslash and uppercase first character</returns>
    internal static string WithEndSlash(ref string path)
    {
        if (path != string.Empty) path = path.TrimEnd('\\') + '\\';

        SH.FirstCharUpper(ref path);
        return path;
    }
}