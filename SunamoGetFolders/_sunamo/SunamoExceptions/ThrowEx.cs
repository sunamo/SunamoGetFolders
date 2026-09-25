namespace SunamoGetFolders._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    /// <summary>
    /// Throws a custom exception with the exception text
    /// </summary>
    /// <param name="exception">The exception to process</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception</param>
    /// <returns>True if exception would be thrown, false otherwise</returns>
    internal static bool Custom(Exception exception, bool isReallyThrowing = true)
    { return Custom(Exceptions.TextOfExceptions(exception), isReallyThrowing); }

    /// <summary>
    /// Throws a custom exception with the specified message
    /// </summary>
    /// <param name="message">The exception message</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception</param>
    /// <param name="secondMessage">Optional second message to append</param>
    /// <returns>True if exception would be thrown, false otherwise</returns>
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joinedMessage = string.Join(" ", message, secondMessage);
        string? exceptionText = Exceptions.Custom(FullNameOfExecutedCode(), joinedMessage);
        return ThrowIsNotNull(exceptionText, isReallyThrowing);
    }

    /// <summary>
    /// Throws a custom exception with stack trace information
    /// </summary>
    /// <param name="exception">The exception to process</param>
    /// <returns>True if exception would be thrown, false otherwise</returns>
    internal static bool CustomWithStackTrace(Exception exception) { return Custom(Exceptions.TextOfExceptions(exception)); }

    #region Other
    /// <summary>
    /// Gets the full name of the currently executed code (type and method)
    /// </summary>
    /// <returns>Full name in format Type.Method</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Gets the full name of the executed code from type and method name
    /// </summary>
    /// <param name="type">The type (can be Type, MethodBase, string, or any object)</param>
    /// <param name="methodName">The method name</param>
    /// <param name="isFromThrowEx">Whether called from ThrowEx (affects stack depth)</param>
    /// <returns>Full name in format Type.Method</returns>
    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type concreteType)
        {
            typeFullName = concreteType.FullName ?? "Type cannot be get via type is Type";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type objectType = type.GetType();
            typeFullName = objectType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws exception if the exception text is not null
    /// </summary>
    /// <param name="exceptionText">The exception text to check</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception</param>
    /// <returns>True if exception would be thrown, false otherwise</returns>
    internal static bool ThrowIsNotNull(string? exceptionText, bool isReallyThrowing = true)
    {
        if (exceptionText != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exceptionText);
            }
            return true;
        }
        return false;
    }
    #endregion
}