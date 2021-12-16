using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrxViewer
{
    [XmlRoot(ElementName = "TestRun")]
    public class TestRun
    {
        [XmlElement(ElementName = "Results")]
        public List<Results> Results { get; set; }
    }

    [XmlRoot(ElementName = "UnitTestResult")]
    public class UnitTestResult
    {

        [XmlAttribute(AttributeName = "executionId")]
        public string ExecutionId { get; set; }

        [XmlAttribute(AttributeName = "testId")]
        public string TestId { get; set; }

        [XmlAttribute(AttributeName = "testName")]
        public string TestName { get; set; }

        [XmlAttribute(AttributeName = "computerName")]
        public string ComputerName { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public DateTime Duration { get; set; }

        [XmlAttribute(AttributeName = "startTime")]
        public DateTime StartTime { get; set; }

        [XmlAttribute(AttributeName = "endTime")]
        public DateTime EndTime { get; set; }

        [XmlAttribute(AttributeName = "testType")]
        public string TestType { get; set; }

        [XmlAttribute(AttributeName = "outcome")]
        public string Outcome { get; set; }

        [XmlAttribute(AttributeName = "testListId")]
        public string TestListId { get; set; }

        [XmlAttribute(AttributeName = "relativeResultsDirectory")]
        public string RelativeResultsDirectory { get; set; }

        [XmlElement(ElementName = "Output")]
        public Output Output { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Output")]
    public class Output
    {

        [XmlElement(ElementName = "StdOut")]
        public string StdOut { get; set; }
    }

    [XmlRoot(ElementName = "Results")]
    public class Results
    {

        [XmlElement(ElementName = "UnitTestResult")]
        public List<UnitTestResult> UnitTestResult { get; set; }
    }
}
