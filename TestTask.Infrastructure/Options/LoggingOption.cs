﻿namespace TestTask.Infrastructure.Options;

public class LoggingOption
{
    public const string SectionName = "Logging";

    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public string Default { get; set; }
}
