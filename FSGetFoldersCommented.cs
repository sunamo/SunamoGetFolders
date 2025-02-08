//using Microsoft.Extensions.Logging;

//namespace SunamoGetFolders;
//internal class FSGetFoldersCommented
//{
//    [Obsolete("Nabízelo mi to primárně tuto variantu s bool místo s GetFoldersEveryFolder a to dokonce i při napsání new - nabídlo new bool. ")]
//    /// <summary>
//    ///     Return only subfolder if A3, a1 not include
//    ///     Must have backslash on end - is folder
//    /// </summary>
//    /// <param name="folder"></param>
//    /// <param name="masc"></param>
//    /// <param name="so"></param>
//    /// <param name="_trimA1"></param>
//    public static List<string> GetFoldersEveryFolder(ILogger logger, string folder, string masc, SearchOption so, bool _trimA1AndLeadingBs = false)
//    {
//        return GetFoldersEveryFolder(logger, folder, masc, so, new() { _trimA1AndLeadingBs = _trimA1AndLeadingBs });

//        //List<string> dirs = null;
//        //try
//        //{
//        //    dirs = Directory.GetDirectories(folder, masc, so).ToList();
//        //}
//        //catch (Exception ex)
//        //{
//        //    ThrowEx.CustomWithStackTrace(ex);
//        //}

//        //if (dirs == null) return new List<string>();
//        ////CAChangeContent.ChangeContent0(null, dirs, d => );
//        //for (var i = 0; i < dirs.Count; i++) dirs[i] = SH.FirstCharUpper(dirs[i]);
//        //if (_trimA1AndLeadingBs)
//        //{
//        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = SH.FirstCharUpper(dirs[i]);
//        //    //CA.Replace(dirs, folder, string.Empty);
//        //    //CA.TrimEnd(dirs, new Char[] { '\\' });
//        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = dirs[i].Replace(folder, string.Empty);
//        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = dirs[i].TrimEnd('\\');
//        //}
//        //else
//        //{
//        //    for (var i = 0; i < dirs.Count; i++) dirs[i] = dirs[i].TrimEnd('\\') + "\\";
//        //    // Must have backslash on end - is folder
//        //    //if (CA.PostfixIfNotEnding != null)
//        //    //{
//        //    //    CA.PostfixIfNotEnding(@"\"", dirs);
//        //    //}
//        //}

//        //return dirs;
//    }
//}
