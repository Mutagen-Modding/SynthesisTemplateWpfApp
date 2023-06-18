using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using SynthesisTemplateWpfApp.Engine.Singletons;

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
            // Create the Inversion of Control container to wire our engine
            // and get our patch run logic.  This is optional, and you could
            // instead instantiate things yourself if that's more comfortable
            var container = GetContainer();
            var runLogic = container.Resolve<RunPatchLogic>();
            
            // Have the run patch code exist in another file for organization
            await runLogic.Run(state);
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

        /// <summary>
        /// Uses Inversion of Control container library Autofac
        /// to do the wiring of all the engine components for us
        /// </summary>
        private Autofac.IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            return builder.Build();
        }
    }
}
