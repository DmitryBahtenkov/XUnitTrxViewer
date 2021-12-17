using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TrxViewer.Models;
using TrxViewer.Services;

namespace TrxViewer.Pages
{
    public partial class TestRunPage : Page
    {
        private readonly TrxReaderService _trxReaderService;
        private readonly TestRunService _testRunService;
        private List<UnitTestResult> _all;
        public TestRunPage(TrxReaderService trxReaderService, TestRunService testRunService)
        {
            _trxReaderService = trxReaderService;
            _testRunService = testRunService;
            InitializeComponent();

            _all = _trxReaderService.ReadLast();
            ListTests.ItemsSource = _all;
            TxtAllCount.Text = $"Всего: {_all.Count}";
            TxtPassedCount.Text = $"Пройдено: {_all.Count(x=>x.Outcome is "Passed")}";
            TxtFailedCount.Text = $"Не пройдено: {_all.Count(x=>x.Outcome is "Failed")}";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var test = ((Button)sender).DataContext as UnitTestResult;
            var @out = test?.Output?.StdOut;
            MessageBox.Show(
                string.IsNullOrEmpty(@out) 
                    ? "Вывод теста пуст" 
                    : @out);
        }

        private void PassedCheck_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            _all = _trxReaderService.ReadLast();
            var result = new List<UnitTestResult>();
            if (PassedCheck?.IsChecked is true)
            {
                result.AddRange(_all.Where(x=>x.Outcome is "Passed"));
            }
            if (FailedCheck?.IsChecked is true)
            {
                result.AddRange(_all.Where(x=>x.Outcome is "Failed"));
            }
            if (OtherCheck?.IsChecked is true)
            {
                result.AddRange(_all.Where(x=>x.Outcome is not "Failed" and not "Passed"));
            }

            if (ListTests is not null)
            {
                ListTests.ItemsSource = result;
            }
        }

        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateFilter();
        }

        private void BtnExec_OnClick(object sender, RoutedEventArgs e)
        {
            var items = ListTests.SelectedItems
                .Cast<UnitTestResult>()
                .Select(x=>x.TestName)
                .ToArray();

            if (items.Length == 0)
            {
                MessageBox.Show("Не выбраны тесты");
            }
            else
            {
                _testRunService.ExecuteTestsByNames(items);
            }
        }
    }
}