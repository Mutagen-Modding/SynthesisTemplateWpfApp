using System.Threading.Tasks;
using System.Windows;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynthesisTemplateWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(StandaloneOpen)
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
        }

        /// <summary>
        /// Will be called when users double click the exe, or is run without any args from the IDE
        /// </summary>
        public int StandaloneOpen()
        {
            var window = new MainWindow();
            window.Show();
            return 0;
        }

        /// <summary>
        /// Will be called when users request the app to show its settings
        /// Very similar to StandaloneOpen, but you might opt to hide any "Start Patch" buttons
        /// that you might show when run truly standalone.
        /// </summary>
        public int OpenForSettings(IOpenForSettingsState state)
        {
            var window = new MainWindow();
            window.Show();
            return 0;
        }
    }
}
