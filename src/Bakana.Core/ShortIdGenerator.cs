using CSharpVitamins;

namespace Bakana.Core
{
    public class ShortIdGenerator : IShortIdGenerator
    {
        public string Generate()
        {
            return ShortGuid.NewGuid();
        }
    }
}