using System.Diagnostics;
using System.Linq;

namespace TrxViewer.Services
{
    public class TestRunService
    {
        private const string FilterKey = "FullyQualifiedName";
        private const string DisplayNameKey = "DisplayName";

        private readonly ConfigurationService _configurationService;

        public TestRunService(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public void ExecuteTestsByNames(params string[] names)
        {
            var sList = names
                .Select(name => $"{FilterKey}~{name}|{DisplayNameKey}~{name}")
                .ToList();

            var testCommand = $"test --logger trx --filter \"{string.Join('|', sList)}\"";

            var p = new Process();
            p.StartInfo.FileName = "dotnet";
            p.StartInfo.WorkingDirectory = $"{_configurationService.GetConfig()?.TestNamespace}";
            p.StartInfo.Arguments = testCommand;
            
            p.Start();
            p.WaitForExit();
        }
    }
}