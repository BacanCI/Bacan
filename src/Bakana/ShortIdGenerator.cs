using Bakana.Core;
using CSharpVitamins;

namespace Bakana
{
    public class ShortIdGenerator : IShortIdGenerator
    {
        public string Generate()
        {
            return ShortGuid.NewGuid();
        }
    }
}