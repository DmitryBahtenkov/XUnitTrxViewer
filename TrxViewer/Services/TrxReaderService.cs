using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TrxViewer.Helpers;
using TrxViewer.Models;

namespace TrxViewer.Services
{
    public class TrxReaderService
    {
        private readonly ConfigurationService _configurationService;

        public TrxReaderService(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public List<UnitTestResult> ReadLast()
        {
            var result = new List<UnitTestResult>();
            try
            {
                var config = _configurationService.GetConfig();
                var directory = new DirectoryInfo(config.TrxDirectory);
                var file = directory.GetFiles()
                    .Where(x => x.FullName.Contains(".trx"))
                    .OrderBy(x => x.CreationTime)
                    .LastOrDefault();

                if (file is not null)
                {
                    var testRun = ConvertXml(file);
                    if (testRun is not null)
                    {
                        var testNameRegexp = new Regex(@"\(.*\)");
                        result = testRun.Results.First().UnitTestResult
                            .OrderBy(x=>x.TestName)
                            .ThenBy(x=>x.Outcome)
                            .Select(x =>
                        {
                            x.TestName = x.TestName.Replace("Skillaz.Tests.", "");
                            x.TestName = testNameRegexp.Replace(x.TestName, "").Split(".").Last();

                            return x;
                        }).ToList();
                    }
                }
            }
            catch
            {
                //ignore
            }

            return result;
        }

        public TestRun? ConvertXml(FileInfo fileInfo)
        {
            using var reader = new StreamReader(fileInfo.Open(FileMode.Open));
            var text = reader.ReadToEnd();
            text = new string(text.SkipWhile(x => x != '\n').ToArray());
            var xmlnsRegex = new Regex("xmlns=\".*\"");
            text = xmlnsRegex.Replace(text, "");

            return text.TryDeserializeFromXml<TestRun>(out var result) 
                ? result 
                : null;
        }
    }
}