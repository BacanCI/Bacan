using Bakana.DomainModels;

namespace Bakana.Core.IO
{
    public interface IBatchFileWriter
    {
        void WriteFile(Batch batch, string path);
    }
}