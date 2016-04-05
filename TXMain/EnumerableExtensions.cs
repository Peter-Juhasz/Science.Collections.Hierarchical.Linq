using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> ReverseIf<T>(this IEnumerable<T> source, bool condition)
        {
            return condition ? source.Reverse() : source;
        }
    }
}
