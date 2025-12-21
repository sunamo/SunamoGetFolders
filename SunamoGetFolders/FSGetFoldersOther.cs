namespace SunamoGetFolders;

partial class FSGetFolders
{
    public static List<string> GetFoldersEveryFolderWhichContainsFiles(ILogger logger, string d, string masc, SearchOption topDirectoryOnly)
    {
        var f = GetFoldersEveryFolder(logger, d, "*", topDirectoryOnly, new GetFoldersEveryFolderArgs { _trimA1AndLeadingBs = false });
        var result = new List<string>();
        foreach (var item in f)
        {
            var files = Directory.GetFiles(item, masc, topDirectoryOnly).ToList();
            if (files.Count != 0) result.Add(item);
        }
        result = result.ConvertAll(d => d + "\\");
        return result;
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
    /// <summary>
    ///     A3 must be GetFilesArgs, not GetFoldersEveryFolder because is calling from GetFiles
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="list"></param>
    /// <param name="e"></param>
    private static void GetFoldersEveryFolder(ILogger logger, string folder, List<string> list, SearchOption so, ref DateTime dtLastLogActualFolder, GetFoldersEveryFolderArgs e = null)
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
            
            // Track which folders should be excluded from traversal
            var foldersToExcludeFromTraversal = new HashSet<string>();
            
            // Check for folders to ignore
            if (e.IgnoreFoldersWithName != null && e.IgnoreFoldersWithName.Count > 0)
            {
                for (int i = folders.Count - 1; i >= 0; i--)
                {
                    var folderName = Path.GetFileName(folders[i].TrimEnd(Path.DirectorySeparatorChar));
                    if (e.IgnoreFoldersWithName.Contains(folderName))
                    {
                        if (e.IncludeExcludedFoldersWithoutTraversing)
                        {
                            // Mark for exclusion from traversal but keep in list
                            foldersToExcludeFromTraversal.Add(folders[i]);
                        }
                        else
                        {
                            // Remove completely
                            folders.RemoveAt(i);
                        }
                    }
                }
            }
            
            // Check for junction points
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
                for (var i = 0; i < folders.Count; i++)
                {
                    // Only traverse if not in exclusion list
                    if (!foldersToExcludeFromTraversal.Contains(folders[i]))
                    {
                        GetFoldersEveryFolder(logger, folders[i], list, so, ref dtLastLogActualFolder, e);
                    }
                }
            }
        }
    }
}