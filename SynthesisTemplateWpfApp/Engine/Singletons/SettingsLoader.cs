using System.IO;
using Newtonsoft.Json;
using Noggog;
using Serilog;

namespace SynthesisTemplateWpfApp.Engine.Singletons;

/// <summary>
/// Logic for loading and saving settings
/// </summary>
public class SettingsLoader
{
    private readonly ILogger _logger;
    
    public const string SettingsFileName = "Settings.json";

    public SettingsLoader(ILogger logger)
    {
        _logger = logger;
    }
    
    public MySettings LoadSettings(DirectoryPath? folder)
    {
        folder ??= Directory.GetCurrentDirectory();
        var path = Path.Combine(folder, SettingsFileName);
        if (!File.Exists(path))
        {
            _logger.Information("Settings folder did not exist.  Returning default settings.  {Path}", path);
            return new MySettings();
        }
        return JsonConvert.DeserializeObject<MySettings>(File.ReadAllText(path))!;
    }
    
    public void SaveSettings(DirectoryPath? folder, MySettings settings)
    {
        folder ??= Directory.GetCurrentDirectory();
        var path = Path.Combine(folder, SettingsFileName);

        Directory.CreateDirectory(folder);
        
        File.WriteAllText(path, JsonConvert.SerializeObject(settings, Formatting.Indented));
    }
}