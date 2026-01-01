namespace SunamoGetFolders._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Checks if the before text is null or whitespace and formats it as a prefix
    /// </summary>
    /// <param name="prefixText">The prefix text to check</param>
    /// <returns>Formatted prefix with colon or empty string</returns>
    internal static string CheckBefore(string prefixText)
    {
        return string.IsNullOrWhiteSpace(prefixText) ? string.Empty : prefixText + ": ";
    }

    /// <summary>
    /// Gets the text representation of exception messages
    /// </summary>
    /// <param name="exception">The exception to process</param>
    /// <param name="isIncludingInner">Whether to include inner exception messages</param>
    /// <returns>Formatted exception text</returns>
    internal static string TextOfExceptions(Exception exception, bool isIncludingInner = true)
    {
        if (exception == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(exception.Message);
        if (isIncludingInner)
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                stringBuilder.AppendLine(exception.Message);
            }
        var result = stringBuilder.ToString();
        return result;
    }

    /// <summary>
    /// Gets the place where exception occurred from stack trace
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to fill also first two values (type and method name)</param>
    /// <returns>Tuple containing type name, method name, and stack trace lines</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var item = lines[i];
            if (isFillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out typeName, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts type name and method name from a stack trace line
    /// </summary>
    /// <param name="stackTraceLine">The stack trace line to parse</param>
    /// <param name="typeName">Output parameter for type name</param>
    /// <param name="methodName">Output parameter for method name</param>
    internal static void TypeAndMethodName(string stackTraceLine, out string typeName, out string methodName)
    {
        var methodPart = stackTraceLine.Split("at ")[1].Trim();
        var fullMethodName = methodPart.Split("(")[0];
        var nameParts = fullMethodName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = nameParts[^1];
        nameParts.RemoveAt(nameParts.Count - 1);
        typeName = string.Join(".", nameParts);
    }

    /// <summary>
    /// Gets the name of the calling method from stack trace
    /// </summary>
    /// <param name="frameIndex">The frame index in stack trace (default is 1)</param>
    /// <returns>The method name or error message</returns>
    internal static string CallingMethod(int frameIndex = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(frameIndex)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    internal readonly static StringBuilder AdditionalInfoInnerStringBuilder = new();
    internal readonly static StringBuilder AdditionalInfoStringBuilder = new();
    #endregion

    #region OnlyReturnString
    /// <summary>
    /// Creates a custom message with optional prefix
    /// </summary>
    /// <param name="prefixText">Optional prefix text</param>
    /// <param name="message">The main message</param>
    /// <returns>Formatted message</returns>
    internal static string? Custom(string prefixText, string message)
    {
        return CheckBefore(prefixText) + message;
    }
    #endregion
}