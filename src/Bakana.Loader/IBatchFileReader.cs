using Bakana.DomainModels;

namespace Bakana.Loader
{
    public interface IBatchFileReader
    {
        Batch ReadFile(string path);
    }
}