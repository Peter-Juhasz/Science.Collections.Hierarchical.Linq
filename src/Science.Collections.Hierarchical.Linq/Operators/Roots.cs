using System;
using System.Collections.Generic;
using System.Linq;

namespace Science.Collections.Hierarchical.Linq
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Selects all root <see cref="ITreeNode{T}"/>s of a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Roots<T>(this IEnumerable<ITreeNode<T>> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));


            return source.Where(n => n.IsRoot());
        }

        /// <summary>
        /// Finds the single root <see cref="ITreeNode{T}"/> of a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static ITreeNode<T> Root<T>(this IEnumerable<ITreeNode<T>> source)
        {
            return source.Roots().Single();
        }
    }
}
