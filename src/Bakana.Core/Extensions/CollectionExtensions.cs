using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakana.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static async Task Iter<T>(this IEnumerable<T> col, Func<T, Task> action)
        {
            if (col == null) return;
            foreach (var item in col)
            {
                await action(item);
            }
        }
    }
}