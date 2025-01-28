namespace SunamoGetFolders._sunamo.SunamoCollectionsChangeContent;

internal class CAChangeContent
{
    private static void RemoveNullOrEmpty(ChangeContentArgsGetFolders a, List<string> files_in)
    {
        if (a != null)
        {
            if (a.removeNull) files_in.Remove(null);
            if (a.removeEmpty)
                for (var i = files_in.Count - 1; i >= 0; i--)
                    if (files_in[i].Trim() == string.Empty)
                        files_in.RemoveAt(i);
        }
    }

    /// <summary>
    ///     Direct edit
    ///     If not every element fullfil pattern, is good to remove null (or values returned if cant be changed) from result
    ///     Poslední číslo je počet parametrů jež se předávají do delegátu
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    internal static List<string> ChangeContent0(ChangeContentArgsGetFolders a, List<string> files_in,
        Func<string, string> func)
    {
        for (var i = 0; i < files_in.Count; i++) files_in[i] = func.Invoke(files_in[i]);
        RemoveNullOrEmpty(a, files_in);
        return files_in;
    }

    #region Vem obojí

    internal static List<string> ChangeContentSwitch12<Arg1>(List<string> files_in, Func<Arg1, string, string> func,
        Arg1 arg)
    {
        for (var i = 0; i < files_in.Count; i++) files_in[i] = func.Invoke(arg, files_in[i]);
        return files_in;
    }

    /// <summary>
    ///     Direct edit input collection
    ///     Dříve to bylo List<string> files_in, Func<string,
    /// </summary>
    /// <typeparam name="Arg1"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <param name="arg"></param>
    internal static List<string> ChangeContent<Arg1>(ChangeContentArgsGetFolders a, List<string> files_in,
        Func<string, Arg1, string> func, Arg1 arg, Func<Arg1, string, string> funcSwitch12 = null)
    {
        if (a == null) a = new ChangeContentArgsGetFolders();
        if (a.switchFirstAndSecondArg)
            files_in = ChangeContentSwitch12(files_in, funcSwitch12, arg);
        else
            for (var i = 0; i < files_in.Count; i++)
                files_in[i] = func.Invoke(files_in[i], arg);
        RemoveNullOrEmpty(a, files_in);
        return files_in;
    }

    #endregion

    #region ChangeContent for easy copy

    /// <summary>
    ///     Direct edit
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    private static List<TResult> ChangeContent<T1, TResult>(List<T1> files_in, Func<T1, TResult> func)
    {
        var result = new List<TResult>(files_in.Count);
        for (var i = 0; i < files_in.Count; i++) result.Add(func.Invoke(files_in[i]));
        return result;
    }

    /// <summary>
    ///     TResult is the same type as T1 (output collection is the same generic as input)
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    private static List<TResult> ChangeContent<T1, T2, TResult>(ChangeContentArgsGetFolders a,
        Func<T1, T2, TResult> func, List<T1> files_in, T2 t2)
    {
        var result = new List<TResult>(files_in.Count);
        for (var i = 0; i < files_in.Count; i++)
            // Fully generic - no strict string can't return the same collection
            result.Add(func.Invoke(files_in[i], t2));
        //CA.RemoveDefaultT<TResult>(result);
        return result;
    }


    #endregion
}