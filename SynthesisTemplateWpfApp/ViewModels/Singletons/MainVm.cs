using Noggog.WPF;
using ReactiveUI.Fody.Helpers;

namespace SynthesisTemplateWpfApp.ViewModels.Singletons;

public class MainVm : ViewModel
{
    /// <summary>
    /// Property controlling what sub-view to show in the main window
    /// </summary>
    [Reactive] public ViewModel ActivePanel { get; set; }
    
    public SettingsVm Settings { get; }

    public MainVm(SettingsVm settingsVm)
    {
        Settings = settingsVm;
        
        // Start our app showing the settings.
        ActivePanel = Settings;
    }
}