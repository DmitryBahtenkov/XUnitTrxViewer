using System.Windows;
using System.Windows.Controls;
using TrxViewer.Models;
using TrxViewer.Services;

namespace TrxViewer.Pages
{
    public partial class TestRunPage : Page
    {
        private readonly TrxReaderService _trxReaderService;
        public TestRunPage(TrxReaderService trxReaderService)
        {
            _trxReaderService = trxReaderService;
            InitializeComponent();

            ListTests.ItemsSource = _trxReaderService.ReadLast();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var test = ((Button)sender).DataContext as UnitTestResult;
            MessageBox.Show(test?.Output?.StdOut);
        }
    }
}