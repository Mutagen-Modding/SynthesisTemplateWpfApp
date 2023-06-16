using System.Threading.Tasks;
using System.Windows;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using SynthesisTemplateWpfApp.Engine;

namespace SynthesisTemplateWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The WPF equivalent of a "main" function
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Call into Synthesis to parse the args and decide which mode to start in
            SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(StandaloneOpen)
                // Alternatively can use this TypicalOpen to have it just run patcher if exe is opened
                //.SetTypicalOpen(GameRelease.SkyrimSE, "SomeModName.esp")
                .SetOpenForSettings(OpenForSettings)
                .SetForWpf()
                .Run(e.Args)
                .Wait();
        }

        /// <summary>
        /// The normal Synthesis entry point
        /// Will be called by Synthesis when users want to patch
        /// </summary>
        private async Task RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            // Have the run patch code exist in another file for organization
            await new RunPatchLogic().Run(state);
        }

        /// <summary>
        /// Will be called when users request the app to show its settings
        /// Very similar to StandaloneOpen, but you might opt to hide any "Start Patch" buttons as it will be running
        /// as a subwindow of Synthesis just for the settings
        /// </summary>
        public int OpenForSettings(IOpenForSettingsState state)
        {
            var window = new MainWindow();
            window.Show();
            return 0;
        }

        /// <summary>
        /// Will be called when users double click the exe, or is run without any args from the IDE.
        /// This is meant to be used if you want the app to open a window if run standalone.
        /// </summary>
        public int StandaloneOpen()
        {
            var window = new MainWindow();
            window.Show();
            return 0;
        }
    }
}
