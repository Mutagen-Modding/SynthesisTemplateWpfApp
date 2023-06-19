using System;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Serilog;
using SynthesisTemplateWpfApp.Engine.Singletons;
using SynthesisTemplateWpfApp.ViewModels.Singletons;
using SynthesisTemplateWpfApp.Views;

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
                .SetTypicalOpen(GameRelease.SkyrimSE, "SomeModName.esp")
                // Alternatively can use this TypicalOpen to open the UI
                // .SetTypicalOpen(StandaloneOpen)
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
            try
            {
                // Create the Inversion of Control container to wire our viewmodels.
                // and get our main view model.  This is optional, and you could
                // instead instantiate things yourself if that's more comfortable
                var container = GetContainer();
                var mvm = container.Resolve<MainVm>();
                mvm.Initialize(state);
                
                var window = new MainWindow(mvm);
                window.Show();
                return 0;
            }
            catch (Exception ex)
            {
                Engine.Logger.Instance.Error(ex, "Error opening for settings");
                throw;
            }
        }

        /// <summary>
        /// Not currently used, unless the alternative UseTypicalOpen is uncommented above
        /// </summary>
        public int StandaloneOpen()
        {
            // Can instantiate your main window like Open For Settings, but without any information
            // given by Synthesis.
            
            // This can make it hard, though, as you don't know where the settings are since that's
            // usually provided by the Synthesis IOpenForSettingsState
            
            // You can still have it open the UI in standalone if you want, but you'll have to figure
            // out how it should work without any information from Synthesis

            throw new NotImplementedException();
        }

        /// <summary>
        /// Uses Inversion of Control container library Autofac
        /// to do the wiring of all the engine components for us
        /// </summary>
        private Autofac.IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            builder.RegisterInstance(Engine.Logger.Instance).As<ILogger>();
            return builder.Build();
        }
    }
}
