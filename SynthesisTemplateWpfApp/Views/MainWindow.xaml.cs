using System.Windows;
using SynthesisTemplateWpfApp.ViewModels.Singletons;

namespace SynthesisTemplateWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainVm mvm)
        {
            InitializeComponent();
            DataContext = mvm;
        }
    }
}
