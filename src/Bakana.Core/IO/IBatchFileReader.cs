using Bakana.Core.Entities;

namespace Bakana.Core.IO
{
    public interface IBatchFileReader
    {
        Batch ReadFile(string path);
    }
}