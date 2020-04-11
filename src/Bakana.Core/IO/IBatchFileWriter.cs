using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.IO
{
    public interface IBatchFileWriter
    {
        void WriteFile(Batch batch, string path);
    }
}