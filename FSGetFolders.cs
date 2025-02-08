namespace SunamoGetFolders;

using Microsoft.Extensions.Logging;
using SunamoGetFolders._sunamo;
using WildcardMatch;

public class FSGetFolders
{
    public static List<string> GetFoldersEveryFolderWhichContainsFiles(ILogger logger, string d, string masc, SearchOption topDirectoryOnly)
    {
        var f = GetFoldersEveryFolder(logger, d, "*", topDirectoryOnly, false);
        var result = new List<string>();
        foreach (var item in f)
        {
            var files = Directory.GetFiles(item, masc, topDirectoryOnly).ToList();
            if (files.Count != 0) result.Add(item);
        }

        result = result.ConvertAll(d => d + "\\");
        return result;
    }

    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder, SearchOption so)
    {
        return GetFoldersEveryFolder(logger, folder, "*", new() { SearchOption = so });
        //return GetFolders(folder, "*", so);
    }

    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder, SearchOption so, GetFoldersEveryFolderArgs? e = null)
    {
        if (e == null)
        {
            e = new();
        }

        e.SearchOption = so;

        return GetFoldersEveryFolder(logger, folder, "*", e);
        //return GetFolders(folder, "*", so);
    }

    public static List<string> GetFoldersEveryFolder(ILogger logger, string v, string contains)
    {
        var folders = GetFoldersEveryFolder(logger, v);
        for (var i = 0; i < folders.Count; i++) folders[i] = folders[i].TrimEnd('\\');
        //CA.TrimEnd(folders, new char[] { '\\' });
        for (var i = folders.Count - 1; i >= 0; i--)
            if (!Regex.IsMatch(Path.GetFileName(folders[i]), contains))
                folders.RemoveAt(i);
        return folders;
    }

    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder)
    {
        return GetFoldersEveryFolder(logger, folder);
        //return GetFolders(folder, SearchOption.TopDirectoryOnly);
    }

    [Obsolete("Nabízelo mi to primárně tuto variantu s bool místo s GetFoldersEveryFolder a to dokonce i při napsání new - nabídlo new bool")]
    /// <summary>
    ///     Return only subfolder if A3, a1 not include
    ///     Must have backslash on end - is folder
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="masc"></param>
    /// <param name="so"></param>
    /// <param name="_trimA1"></param>
    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder, string masc, SearchOption so, bool _trimA1AndLeadingBs = false)
    {
        return GetFoldersEveryFolder(logger, folder, masc, new() { SearchOption = so, _trimA1AndLeadingBs = _trimA1AndLeadingBs });

        //List<string> dirs = null;
        //try
        //{
        //    dirs = Directory.GetDirectories(folder, masc, so).ToList();
        //}
        //catch (Exception ex)
        //{
        //    ThrowEx.CustomWithStackTrace(ex);
        //}

        //if (dirs == null) return new List<string>();
        ////CAChangeContent.ChangeContent0(null, dirs, d => );
        //for (var i = 0; i < dirs.Count; i++) dirs[i] = SH.FirstCharUpper(dirs[i]);
        //if (_trimA1AndLeadingBs)
        //{
        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = SH.FirstCharUpper(dirs[i]);
        //    //CA.Replace(dirs, folder, string.Empty);
        //    //CA.TrimEnd(dirs, new Char[] { '\\' });
        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = dirs[i].Replace(folder, string.Empty);
        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = dirs[i].TrimEnd('\\');
        //}
        //else
        //{
        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = dirs[i].TrimEnd('\\') + "\\";
        //    // Must have backslash on end - is folder
        //    //if (CA.PostfixIfNotEnding != null)
        //    //{
        //    //    CA.PostfixIfNotEnding(@"\"", dirs);
        //    //}
        //}

        //return dirs;
    }

    /// <summary>
    ///     A3 must be GetFilesArgs, not GetFoldersEveryFolder because is calling from GetFiles
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="list"></param>
    /// <param name="e"></param>
    private static void GetFoldersEveryFolder(ILogger logger, string folder, List<string> list, SearchOption so, ref DateTime dtLastLogActualFolder, GetFilesArgsGetFolders e = null)
    {
        List<string> folders = null;

        try
        {
            if (e.SecondsToWriteActualFolder != -1)
            {
                var dtDiff = DateTime.Now - dtLastLogActualFolder;
                if (dtDiff.TotalSeconds > e.SecondsToWriteActualFolder)
                {
                    Console.WriteLine(folder);
                    dtLastLogActualFolder = DateTime.Now;
                }
            }

            folders = Directory.GetDirectories(folder).ToList();
            folders = CAChangeContent.ChangeContent0(null, folders, FS.WithEndSlash);
            //#if DEBUG
            //            if (e.writeToDebugEveryLoadedFolder)
            //            {
            //                DebugLogger.Instance.WriteLine("GetFoldersEveryFolder: " + folder);
            //            }
            //#endif
        }
        catch (Exception ex)
        {
            if (e.throwEx) ThrowEx.Custom(ex);

            // Not throw exception, it's probably Access denied  on Documents and Settings etc
            //throw new Exception("GetFoldersEveryFolder with path: " + folder, ex);
        }

        if (folders != null)
        {
            CA.RemoveWhichContainsList(folders, e.excludeFromLocationsCOntains, e.wildcard);

            for (int i = folders.Count - 1; i >= 0; i--)
            {
                if (JunctionPoint.IsJunctionPoint(logger, folders[i]))
                {
                    folders.RemoveAt(i);
                }
            }

            list.AddRange(folders);
            if (so == SearchOption.AllDirectories)
            {
                for (var i = 0; i < folders.Count; i++) GetFoldersEveryFolder(logger, folders[i], list, so, ref dtLastLogActualFolder, e);
            }
        }
    }

    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder, string masc = "*", SearchOption so = SearchOption.TopDirectoryOnly, GetFoldersEveryFolderArgs? e = null)
    {
        if (e == null)
        {
            e = new GetFoldersEveryFolderArgs();
        }

        e.SearchOption = so;

        return GetFoldersEveryFolder(logger, folder, masc, e);
    }

    //private static void GetFoldersEveryFolder(string folder, string mask, List<string> list)
    //{
    //    try
    //    {
    //        var folders = Directory.GetDirectories(folder, mask, SearchOption.TopDirectoryOnly);
    //        list.AddRange(folders);
    //        foreach (var item in folders) GetFoldersEveryFolder(item, mask, list);
    //    }
    //    catch (Exception ex)
    //    {
    //        ThrowEx.Custom(ex);
    //        // Not throw exception, it's probably Access denied  on Documents and Settings etc
    //        //throw new Exception("GetFoldersEveryFolder with path: " + folder, ex);
    //    }
    //}

    /// <summary>
    ///     
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder, string masc = "*", GetFoldersEveryFolderArgs? e = null)
    {
        if (e == null) e = new GetFoldersEveryFolderArgs();
        var list = new List<string>();
        // zde progress bar nedává smysl. načítám to rekurzivně, tedy nevím na začátku kolik těch složek bude
        //IProgressBarHelper pbh = null;
        //if (a.progressBarHelper != null)
        //{
        //    pbh = a.progressBarHelper.CreateInstance(a.pb, files.Count, this);
        //}
        DateTime firstFolder = DateTime.Now;
        GetFoldersEveryFolder(logger, folder, list, e.SearchOption, ref firstFolder, e);

        if (masc != "*")
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var fn = Path.GetFileName(list[i].TrimEnd(Path.DirectorySeparatorChar));
                if (!masc.WildcardMatch(fn)) list.RemoveAt(i);
            }

        if (e._trimA1AndLeadingBs)
            //list = CAChangeContent.ChangeContent0(null, list, d => d = d.Replace(folder, "").TrimStart('\\'));
            for (var i = 0; i < list.Count; i++)
                list[i] = list[i].Replace(folder, "").TrimStart('\\');
        if (e.excludeFromLocationsCOntains != null)
            // I want to find files recursively
            foreach (var item in e.excludeFromLocationsCOntains)
                CA.RemoveWhichContains(list, item, e.wildcard, Regex.IsMatch);

        return list;
    }
}