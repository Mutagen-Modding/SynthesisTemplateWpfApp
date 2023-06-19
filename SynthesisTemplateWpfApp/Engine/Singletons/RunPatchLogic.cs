using System;
using System.IO;
using System.Threading.Tasks;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynthesisTemplateWpfApp.Engine.Singletons;

/// <summary>
/// Class housing the run patcher logic
/// </summary>
public class RunPatchLogic
{
    private readonly SettingsLoader _settingsLoader;

    public RunPatchLogic(SettingsLoader settingsLoader)
    {
        _settingsLoader = settingsLoader;
    }
    
    public async Task Run(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
    {
        // Normal patcher logic you would have in a patcher without a custom UI
        var settings = _settingsLoader.LoadSettings(state.ExtraSettingsDataPath);
        Console.WriteLine($"{nameof(MySettings.SomeBoolean)}: {settings.SomeBoolean}");
        Console.WriteLine($"{nameof(MySettings.SomeString)}: {settings.SomeString}");
    }
}