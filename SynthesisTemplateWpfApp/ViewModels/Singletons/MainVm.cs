using System.Windows;
using Mutagen.Bethesda.Synthesis;
using Noggog.WPF;
using ReactiveUI.Fody.Helpers;

namespace SynthesisTemplateWpfApp.ViewModels.Singletons;

/// <summary>
/// Main View Model controlling what sub-view is shown in our window,
/// as well as being an entry point for handling startup/shutdown calls
/// </summary>
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

    public void Initialize(IOpenForSettingsState openForSettingsState)
    {
        Settings.LoadSettings(openForSettingsState);
    }

    public void Shutdown()
    {
        Settings.SaveSettings();

        // Exit app
        Application.Current.Shutdown();
    }
}