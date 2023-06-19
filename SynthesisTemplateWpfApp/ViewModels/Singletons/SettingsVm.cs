using System.Reactive.Linq;
using Mutagen.Bethesda.Synthesis;
using Noggog;
using Noggog.WPF;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SynthesisTemplateWpfApp.Engine.Singletons;

namespace SynthesisTemplateWpfApp.ViewModels.Singletons;

public class SettingsVm : ViewModel
{
    private readonly SettingsLoader _loader;
    private DirectoryPath? _settingsFolder;
    
    [Reactive] public bool SomeBoolean { get; set; }

    [Reactive] public string SomeString { get; set; } = string.Empty;

    private readonly ObservableAsPropertyHelper<string> _derivativeSummary;
    public string DerivativeSummary => _derivativeSummary.Value;

    public SettingsVm(SettingsLoader loader)
    {
        _loader = loader;
        
        // Showing off RxUI's ability to combine other properties and make derivative properties
        _derivativeSummary = Observable.CombineLatest(
                this.WhenAnyValue(x => x.SomeBoolean),
                this.WhenAnyValue(x => x.SomeString),
                (b, s) => $"Bool is {b}, String is {s}")
            .ToProperty(this, nameof(DerivativeSummary));
    }

    public void LoadSettings(IOpenForSettingsState openForSettingsState)
    {
        var settingsDto = _loader.LoadSettings(openForSettingsState.ExtraSettingsDataPath);
        
        SomeBoolean = settingsDto.SomeBoolean;
        SomeString = settingsDto.SomeString;
        
        // Save target folder for later
        _settingsFolder = openForSettingsState.ExtraSettingsDataPath;
    }

    public void SaveSettings()
    {
        _loader.SaveSettings(
            _settingsFolder,
            new MySettings()
            {
                SomeBoolean = SomeBoolean,
                SomeString = SomeString,
            });
    }
}