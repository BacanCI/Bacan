using Bakana.Options;

namespace Bakana
{
    public interface IOptions
    {
        Mode Mode { get; set; }
        string Host { get; set; }
        string Batch { get; set; }
        BatchFileFormat Format { get; set; }
    }
}