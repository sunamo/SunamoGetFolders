namespace SunamoGetFolders;

/// <summary>
/// Provides methods for getting folders from the file system
/// </summary>
public partial class FSGetFolders
{
    /// <summary>
    /// Gets all folders in the specified directory with optional filtering
    /// Only this interface signature can be used - other overloads wouldn't know which method to call when only logger is provided
    /// </summary>
    /// <param name="logger">Logger instance for logging operations</param>
    /// <param name="folderPath">The folder path to search</param>
    /// <param name="searchPattern">Search pattern for folder names (supports wildcards, default is "*")</param>
    /// <param name="searchOption">Search option for top directory only or all directories</param>
    /// <param name="args">Optional arguments for folder retrieval configuration</param>
    /// <returns>List of folder paths matching the criteria</returns>
    public static List<string> GetFoldersEveryFolder(ILogger logger, string folderPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly, GetFoldersEveryFolderArgs? args = null)
    {
        if (args == null) args = new GetFoldersEveryFolderArgs();
        var resultList = new List<string>();

        DateTime lastLogTime = DateTime.Now;
        GetFoldersEveryFolder(logger, folderPath, resultList, searchOption, ref lastLogTime, args);

        if (searchPattern != "*")
            for (var i = resultList.Count - 1; i >= 0; i--)
            {
                var folderName = Path.GetFileName(resultList[i].TrimEnd(Path.DirectorySeparatorChar));
                if (!searchPattern.WildcardMatch(folderName)) resultList.RemoveAt(i);
            }

        if (args._trimA1AndLeadingBs)
            for (var i = 0; i < resultList.Count; i++)
                resultList[i] = resultList[i].Replace(folderPath, "").TrimStart('\\');

        // Only remove folders from results if IncludeExcludedFoldersWithoutTraversing is false
        if (!args.IncludeExcludedFoldersWithoutTraversing)
        {
            List<string> codeFoldersWrapped = args.IgnoreFoldersWithName.Select(folderName => "\\" + folderName + "\\").ToList();
            foreach (var item in codeFoldersWrapped)
                CA.RemoveWhichContains(resultList, item, false, null);
        }

        if (args.excludeFromLocationsCOntains != null)
            // I want to find files recursively
            foreach (var item in args.excludeFromLocationsCOntains)
                CA.RemoveWhichContains(resultList, item, args.wildcard, Regex.IsMatch);
        return resultList;
    }
}