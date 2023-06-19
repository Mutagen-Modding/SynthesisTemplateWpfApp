using SynthesisTemplateWpfApp.ViewModels.Singletons;

namespace SynthesisTemplateWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(MainVm mvm)
        {
            InitializeComponent();
            DataContext = mvm;
            Closing += (_, args) =>
            {
                // Hide the window
                Visibility = System.Windows.Visibility.Collapsed;
                
                // Tell the system to not kill the app.  We'll do it.
                args.Cancel = true;
                
                mvm.Shutdown();
            };
        }
    }
}
