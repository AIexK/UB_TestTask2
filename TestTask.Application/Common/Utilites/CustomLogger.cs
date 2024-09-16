using NLog;
using TestTask.Application.Common.Interfaces;

namespace TestTask.Application.Common.Utilites;

using LogLevel = NLog.LogLevel;

public class PfmsLogger : ICustomLogger
{
    private readonly LogLevel _minLogLevel;
    private readonly Logger _logger;

    public PfmsLogger(Logger logger, string minLogLevel)
    {
        _logger = logger;

        try
        {
            _minLogLevel = LogLevel.FromString(minLogLevel);
        }
        catch
        {
            _minLogLevel = LogLevel.Debug;
        }
    }

    private void SetLogDirectory(string additionalFolder)
    {
        GlobalDiagnosticsContext.Set("logDirectory", additionalFolder);
    }

    private string GetFileName(string filePath)
    {
        return !string.IsNullOrWhiteSpace(filePath) ? Path.GetFileName(filePath) : string.Empty;
    }

    private string FormatLogMessage(string filename, string actionName, int line, string message)
    {
        return $"{filename}/{actionName} line {line}: {message}";
    }

    public void Debug(string message, bool detailed = true, string additionalFolder = "",
                      [System.Runtime.CompilerServices.CallerLineNumber] int line = 0,
                      [System.Runtime.CompilerServices.CallerMemberName] string actionName = "",
                      [System.Runtime.CompilerServices.CallerFilePath] string filePath = "")
    {
        Log(LogLevel.Debug, message, detailed, additionalFolder, line, actionName, filePath);
    }

     public void Error(string message, bool detailed = true, string additionalFolder = "",
                      [System.Runtime.CompilerServices.CallerLineNumber] int line = 0,
                      [System.Runtime.CompilerServices.CallerMemberName] string actionName = "",
                      [System.Runtime.CompilerServices.CallerFilePath] string filePath = "")
    {
        Log(LogLevel.Error, message, detailed, additionalFolder, line, actionName, filePath);
    }
 

    private void Log(LogLevel logLevel, string message, bool detailed = true,
                     string additionalFolder = "", [System.Runtime.CompilerServices.CallerLineNumber] int line = 0,
                     [System.Runtime.CompilerServices.CallerMemberName] string actionName = "",
                     [System.Runtime.CompilerServices.CallerFilePath] string filePath = "")
    {
        if (logLevel < _minLogLevel)
        {
            return;
        }

        SetLogDirectory(additionalFolder);

        if (!detailed)
        {
            _logger.Log(logLevel, message);
            return;
        }

        var filename = GetFileName(filePath);
        var formattedMessage = FormatLogMessage(filename, actionName, line, message);
        _logger.Log(logLevel, formattedMessage);
    }
}
