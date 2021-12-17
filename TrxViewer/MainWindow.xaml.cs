using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
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
