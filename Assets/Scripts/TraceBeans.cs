using System;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.IO;

//Better debugger - https://github.com/DakotahVoet/BA.Libraries/tree/master/BAUnity
public static class TraceBeans
{
    #region Fields
    // Default font size
    public static int FontSize = 12;

    // Colors for the various trace statements
    private const string COLOR_START_END = "#41a392";
    private const string COLOR_INFO = "#56d6bf";
    private const string COLOR_WARN = "#ebbe0e";
    private const string COLOR_ERROR = "#eb420e";
    private const string COLOR_EX = "#d1155d";
    #endregion

    #region Public

    // Start
    public static void Start(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.Log(
            ApplyStyles(
                FormatCallerAttributes(sourceFilePath, memberName, sourceLineNumber),
                "Start",
                COLOR_START_END));
    }

    // End
    public static void End(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.Log(
            ApplyStyles(
                FormatCallerAttributes(sourceFilePath, memberName, sourceLineNumber),
                "End",
                COLOR_START_END));
    }

    // Log
    public static void Log(
        object message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.Log(
            ApplyStyles(
                FormatCallerAttributes(sourceFilePath, memberName, sourceLineNumber),
                message));
    }

    // Info
    public static void Info(
        object message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.Log(
            ApplyStyles(
                FormatCallerAttributes(sourceFilePath, memberName, sourceLineNumber),
                message,
                COLOR_INFO));
    }

    // Warn
    public static void Warn(
        object message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.LogWarning(
            ApplyStyles(
                FormatCallerAttributes(sourceFilePath, memberName, sourceLineNumber),
                message,
                COLOR_WARN));
    }

    // Error
    public static void Error(
        object message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.LogError(
            ApplyStyles(
                FormatCallerAttributes(sourceFilePath, memberName, sourceLineNumber),
                message,
                COLOR_ERROR));
    }

    // Exception default
    public static void Exception(Exception ex)
    {
        Debug.LogException(ex);
    }

    // Exception alt
    public static void Exception(
        object message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        Debug.LogError(
            ApplyStyles(
                FormatCallerAttributes(sourceFilePath, memberName, sourceLineNumber),
                message,
                COLOR_EX));
    }
    #endregion

    #region Private

    // Apply rich text formatting
    private static object ApplyStyles(string context, object message)
    {
        object log =
            "<size=" + FontSize.ToString() + ">"
            + "<b>[" + context + "]</b>: "
            + message
            + "</size>";

        return log;
    }

    private static object ApplyStyles(string context, object message, string color)
    {
        object log =
            "<size=" + FontSize.ToString() + ">"
            + "<color=" + color + ">"
            + "<b>[" + context + "]</b>: "
            + message
            + "</color>"
            + "</size>";

        return log;
    }

    // Concatenate and format the caller attributes string
    private static string FormatCallerAttributes(string path, string member, int line)
    {
        return Path.GetFileNameWithoutExtension(path) + ":" + member + ":" + line;
    }

    #endregion
}