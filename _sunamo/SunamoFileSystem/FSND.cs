namespace SunamoGetFolders._sunamo.SunamoFileSystem;

internal class FSND
{

    /// <summary>
    ///     Usage: Exceptions.FileWasntFoundInDirectory
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string WithEndSlash(ref string v)
    {
        if (v != string.Empty) v = v.TrimEnd('\\') + '\\';

        FirstCharUpper(ref v);
        return v;
    }

    internal static void FirstCharUpper(ref string nazevPP)
    {
        nazevPP = FirstCharUpper(nazevPP);
    }

    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1) return nazevPP.ToUpper();

        var sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }
}