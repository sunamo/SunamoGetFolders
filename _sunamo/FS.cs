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


    internal static void CreateUpfoldersPsysicallyUnlessThere(string nad)
    {
        CreateFoldersPsysicallyUnlessThere(Path.GetDirectoryName(nad));
    }

    internal static void CreateFoldersPsysicallyUnlessThere(string nad)
    {
        ThrowEx.IsNullOrEmpty("nad", nad);
        //ThrowEx.IsNotWindowsPathFormat("nad", nad);
        if (Directory.Exists(nad)) return;
        var slozkyKVytvoreni = new List<string>
        {
            nad
        };
        while (true)
        {
            nad = Path.GetDirectoryName(nad);

            if (Directory.Exists(nad)) break;
            var kopia = nad;
            slozkyKVytvoreni.Add(kopia);
        }

        slozkyKVytvoreni.Reverse();
        foreach (var item in slozkyKVytvoreni)
        {
            var folder = item;
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        }
    }
}