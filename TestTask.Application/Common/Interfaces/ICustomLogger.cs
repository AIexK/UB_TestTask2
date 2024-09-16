namespace TestTask.Application.Common.Interfaces;

public interface ICustomLogger
{
    public void Debug(string message,
                      bool detailed = true,
                      string additionalFolder = "",
                      [System.Runtime.CompilerServices.CallerLineNumber] int line = default,
                      [System.Runtime.CompilerServices.CallerMemberName] string actionName = "",
                      [System.Runtime.CompilerServices.CallerFilePath] string filePath = "");

    public void Error(string message,
                      bool detailed = true,
                      string additionalFolder = "",
                      [System.Runtime.CompilerServices.CallerLineNumber] int line = default,
                      [System.Runtime.CompilerServices.CallerMemberName] string actionName = "",
                      [System.Runtime.CompilerServices.CallerFilePath] string filePath = "");
}
