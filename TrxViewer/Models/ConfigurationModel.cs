namespace TrxViewer.Models
{
    public record ConfigurationModel
    {
        public string TrxDirectory { get; set; }
        public string TestNamespace { get; set; }
    }
}