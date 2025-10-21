// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoGetFolders;

public partial class FSGetFolders
{
    /// <summary>
    ///     Může být jen jen toto rozhraní
    ///     U všdch ostatních pokud předám jen logger,v neví kterou metodu má použít
    ///     
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder, string masc = "*", SearchOption so = SearchOption.TopDirectoryOnly, GetFoldersEveryFolderArgs? e = null)
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
        GetFoldersEveryFolder(logger, folder, list, so, ref firstFolder, e);
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
        
        // Only remove folders from results if IncludeExcludedFoldersWithoutTraversing is false
        if (!e.IncludeExcludedFoldersWithoutTraversing)
        {
            List<string> codeFoldersWrapped = e.IgnoreFoldersWithName.Select(d => "\\" + d + "\\").ToList();
            foreach (var item in codeFoldersWrapped)
                CA.RemoveWhichContains(list, item, false, null);
        }
        
        if (e.excludeFromLocationsCOntains != null)
            // I want to find files recursively
            foreach (var item in e.excludeFromLocationsCOntains)
                CA.RemoveWhichContains(list, item, e.wildcard, Regex.IsMatch);
        return list;
    }
}