using System.Windows;
using TrxViewer.Pages;
using TrxViewer.Services;

namespace TrxViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var configService = new ConfigurationService();
            TrxFrame.Navigate(new TestRunPage(new TrxReaderService(configService), new TestRunService(configService)));
            ConfigFrame.Navigate(new ConfigPage(configService));
        }
        
    }
}
