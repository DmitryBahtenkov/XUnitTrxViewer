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
        private const string Path = @"C:\Users\Andrew\Documents\Workspace\Sharp\TrxViewer\ДмитрийБахтенков_DESKTOP_F6SKA47_2021_12_16_16_16_28.trx";

        public MainWindow()
        {
            InitializeComponent();
            var configService = new ConfigurationService();
            TrxFrame.Navigate(new TestRunPage(new TrxReaderService(configService)));
            ConfigFrame.Navigate(new ConfigPage(configService));
        }
        
    }
}
