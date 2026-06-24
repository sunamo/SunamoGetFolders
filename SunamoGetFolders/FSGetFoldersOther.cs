namespace SunamoGetFolders;

partial class FSGetFolders
{
    public static List<string> GetFoldersEveryFolderWhichContainsFiles(ILogger logger, string folderPath, string searchPattern, SearchOption searchOption)
    {
        var folders = GetFoldersEveryFolder(logger, folderPath, "*", searchOption, new GetFoldersEveryFolderArgs { TrimA1AndLeadingBs = false });
        var result = new List<string>();
        foreach (var item in folders)
        {
            var files = Directory.GetFiles(item, searchPattern, searchOption).ToList();
            if (files.Count != 0) result.Add(item);
        }
        result = result.ConvertAll(path => path + "\\");
        return result;
    }

    public static List<string> GetFoldersEveryFolder(ILogger logger, string folderPath, string regexPattern)
    {
        var folders = GetFoldersEveryFolder(logger, folderPath);
        for (var i = 0; i < folders.Count; i++) folders[i] = folders[i].TrimEnd('\\');
        if (regexPattern != "*")
            for (var i = folders.Count - 1; i >= 0; i--)
                if (!Regex.IsMatch(Path.GetFileName(folders[i]), regexPattern))
                    folders.RemoveAt(i);
        return folders;
    }
    private static void GetFoldersEveryFolder(ILogger logger, string folderPath, List<string> resultList, SearchOption searchOption, ref DateTime lastLogTime, GetFoldersEveryFolderArgs? args = null)
    {
        List<string>? folders = null;
        try
        {
            if (args != null && args.SecondsToWriteActualFolder != -1)
            {
                var timeDifference = DateTime.Now - lastLogTime;
                if (timeDifference.TotalSeconds > args.SecondsToWriteActualFolder)
                {
                    Console.WriteLine(folderPath);
                    lastLogTime = DateTime.Now;
                }
            }
            folders = Directory.GetDirectories(folderPath).ToList();
            folders = CAChangeContent.ChangeContent0(null, folders, FS.WithEndSlash);
        }
        catch (Exception ex)
        {
            if (args != null && args.ThrowEx) ThrowEx.Custom(ex);
        }
        if (folders != null && args != null)
        {
            CA.RemoveWhichContainsList(folders, args.ExcludeFromLocationsContains, args.Wildcard);

            // Track which folders should be excluded from traversal
            var foldersToExcludeFromTraversal = new HashSet<string>();

            // Check for folders to ignore
            if (args.IgnoreFoldersWithName != null && args.IgnoreFoldersWithName.Count > 0)
            {
                for (int i = folders.Count - 1; i >= 0; i--)
                {
                    var folderName = Path.GetFileName(folders[i].TrimEnd(Path.DirectorySeparatorChar));
                    if (args.IgnoreFoldersWithName.Contains(folderName))
                    {
                        if (args.IncludeExcludedFoldersWithoutTraversing)
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

            resultList.AddRange(folders);

            if (searchOption == SearchOption.AllDirectories)
            {
                for (var i = 0; i < folders.Count; i++)
                {
                    // Only traverse if not in exclusion list
                    if (!foldersToExcludeFromTraversal.Contains(folders[i]))
                    {
                        GetFoldersEveryFolder(logger, folders[i], resultList, searchOption, ref lastLogTime, args);
                    }
                }
            }
        }
    }
}