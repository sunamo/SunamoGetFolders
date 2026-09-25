namespace SunamoGetFolders._sunamo;

internal class FS
{
    internal static string WithEndSlash(string path)
    {
        return WithEndSlash(ref path);
    }

    // Usage: Exceptions.FileWasntFoundInDirectory
    internal static string WithEndSlash(ref string path)
    {
        if (path != string.Empty) path = path.TrimEnd('\\') + '\\';

        SH.FirstCharUpper(ref path);
        return path;
    }
}
