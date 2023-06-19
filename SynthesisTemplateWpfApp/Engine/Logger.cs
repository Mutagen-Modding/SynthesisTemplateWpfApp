using System;
using System.IO;
using Serilog;

namespace SynthesisTemplateWpfApp.Engine;

/// <summary>
/// Doesn't need to be Serilog.  Just what I'm used to
/// </summary>
public class Logger
{
    public static ILogger Instance { get; } = GetLogger();
    
    private static ILogger GetLogger()
    {
        var startDt = DateTime.Now;
        var startTime = $"{startDt:HH_mm_ss}";
        startTime = startTime.Remove(5, 1);
        startTime = startTime.Remove(2, 1);
        startTime = startTime.Insert(2, "h");
        startTime = startTime.Insert(5, "m");
        startTime += "s";
        var logFileName = $"{startDt:MM-dd-yyyy}_{startTime}.log";

        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(Path.Combine("logs", logFileName))
            .CreateLogger();

        return Serilog.Log.Logger;
    }
}