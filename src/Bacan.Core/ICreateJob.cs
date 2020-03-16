namespace Bacan.Core
{
    public interface ICreateJob
    {
        string Id { get; set; }
        string Description { get; set; }
        string ArchiveUrl { get; set; }
        string ArchiveName { get; set; }
        string ProcessName { get; set; }
        string ProcessArguments { get; set; }
    }
}