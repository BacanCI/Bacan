using Bakana.DomainModels;

namespace Bakana.Core.IO
{
    public interface IBatchFileReader
    {
        Batch ReadFile(string path);
    }
}