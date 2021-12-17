using System;
using System.Windows;
using System.Windows.Controls;
using TrxViewer.Models;
using TrxViewer.Services;

namespace TrxViewer.Pages
{
    public partial class ConfigPage : Page
    {
        private readonly ConfigurationService _configurationService;
        private ConfigurationModel _model;
        public ConfigPage(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
            InitializeComponent();
            _model = _configurationService.GetConfig();
            DataContext = _model;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _configurationService.SetConfiguration(_model);
                MessageBox.Show("Успешно");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка при сохранении: {exception.Message}");
            }
        }
    }
}