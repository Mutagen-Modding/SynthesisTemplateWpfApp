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
        }
    }
}
