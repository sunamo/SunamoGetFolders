namespace SunamoGetFolders;

public partial class FSGetFolders
{
    // Only this interface signature can be used - other overloads wouldn't know which method to call when only logger is provided
    public static List<string> GetFoldersEveryFolder(ILogger logger, string folderPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly, GetFoldersEveryFolderArgs? args = null)
    {
        args ??= new GetFoldersEveryFolderArgs();
        var resultList = new List<string>();

        var lastLogTime = DateTime.Now;
        GetFoldersEveryFolder(logger, folderPath, resultList, searchOption, ref lastLogTime, args);

        if (searchPattern != "*")
            for (var i = resultList.Count - 1; i >= 0; i--)
            {
                var folderName = Path.GetFileName(resultList[i].TrimEnd(Path.DirectorySeparatorChar));
                if (!searchPattern.WildcardMatch(folderName)) resultList.RemoveAt(i);
            }

        if (args.TrimA1AndLeadingBs)
            for (var i = 0; i < resultList.Count; i++)
                resultList[i] = resultList[i].Replace(folderPath, "").TrimStart('\\');

        // Only remove folders from results if IncludeExcludedFoldersWithoutTraversing is false
        if (!args.IncludeExcludedFoldersWithoutTraversing)
        {
            var ignoredFoldersWrapped = args.IgnoreFoldersWithName.Select(folderName => "\\" + folderName + "\\").ToList();
            foreach (var item in ignoredFoldersWrapped)
                CA.RemoveWhichContains(resultList, item, false, null);
        }

        if (args.ExcludeFromLocationsContains != null)
            // I want to find files recursively
            foreach (var item in args.ExcludeFromLocationsContains)
                CA.RemoveWhichContains(resultList, item, args.Wildcard, Regex.IsMatch);
        return resultList;
    }
}
