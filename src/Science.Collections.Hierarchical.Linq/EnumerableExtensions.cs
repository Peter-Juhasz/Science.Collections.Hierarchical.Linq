using System.Collections.Generic;
using System.Linq;

namespace Science.Collections.Hierarchical.Linq
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> ReverseIf<T>(this IEnumerable<T> source, bool condition)
        {
            return condition ? source.Reverse() : source;
        }
    }
}
