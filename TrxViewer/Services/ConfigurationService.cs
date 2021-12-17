using System.IO;
using System.Text.Json;
using TrxViewer.Models;

namespace TrxViewer.Services
{
    public class ConfigurationService
    {
        public const string Path = "config.json";
        public ConfigurationModel GetConfig()
        {
            return File.Exists(Path) 
                ? GetConfigInternal() 
                : SetDefault();
        }
        
        private ConfigurationModel GetConfigInternal()
        {
            using var reader = new StreamReader(Path);
            return JsonSerializer.Deserialize<ConfigurationModel>(reader.ReadToEnd());
        }
        
        private ConfigurationModel SetDefault()
        {
            var model = new ConfigurationModel
            {
                TrxDirectory = @"C:\App.Tests\TestResults",
                TestNamespace = "App.Tests"
            };

            return SetConfiguration(model);
        }

        public ConfigurationModel SetConfiguration(ConfigurationModel model)
        {
            using var writer = new StreamWriter(File.Open(Path, FileMode.OpenOrCreate));
            writer.Write(JsonSerializer.Serialize(model));

            return model;
        }
    }
}