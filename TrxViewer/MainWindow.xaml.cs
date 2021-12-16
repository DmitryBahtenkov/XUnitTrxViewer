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
            GetTests();
        }

        public string ReadFile()
        {
            return File.ReadAllText(Path);
        }

        public void GetTests()
        {
            var xml = ReadFile();
            xml = new string(xml.SkipWhile(x => x != '\n').ToArray());

            var regex = new Regex("xmlns=\".*\"");

            xml = regex.Replace(xml, "");
            XmlSerializer serializer = new XmlSerializer(typeof(TestRun));
            using (StringReader reader = new StringReader(xml))
            {
                var test = (TestRun)serializer.Deserialize(reader);
                var testNameRegexp = new Regex(@"\(.*\)");
                var tests = test.Results.First().UnitTestResult.Select(x=>
                {
                    x.TestName = x.TestName.Replace("Skillaz.Tests.", "");
                    x.TestName = testNameRegexp.Replace(x.TestName, "").Split(".").Last();

                    return x;
                }).ToList();
                ListTests.ItemsSource = tests;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
