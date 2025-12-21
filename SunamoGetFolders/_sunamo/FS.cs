namespace SunamoGetFolders._sunamo;

internal class FS
{
    internal static string WithEndSlash(string v)
    {
        return WithEndSlash(ref v);
    }

    /// <summary>
    ///     Usage: Exceptions.FileWasntFoundInDirectory
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string WithEndSlash(ref string v)
    {
        if (v != string.Empty) v = v.TrimEnd('\\') + '\\';

        SH.FirstCharUpper(ref v);
        return v;
    }



}