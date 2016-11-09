using System;
using System.Collections.Generic;

namespace Science.Collections.Hierarchical.Linq
{
    /// <summary>
    /// Contains helper methods for creating <see cref="ITreeNode{T}"/>s.
    /// </summary>
    public static class TreeNode
    {
        /// <summary>
        /// Provides tree-like navigation for a given value.
        /// </summary>
        /// <param name="value">The value represented by the node.</param>
        /// <param name="childrenSelector">Gets the child values of a given value.</param>
        /// <param name="parentSelector">Gets the parent value of a given value.</param>
        /// <param name="hasParent">Determines whether a given value has a parent value or not.</param>
        /// <returns>A <see cref="ITreeNode{T}"/> representing <paramref name="value"/>.</returns>
        public static ITreeNode<T> From<T>(
            T value,
            Func<T, IEnumerable<T>> childrenSelector,
            Func<T, T> parentSelector,
            Func<T, bool> hasParent
        )
        {
            if (childrenSelector == null)
                throw new ArgumentNullException(nameof(childrenSelector));

            if (parentSelector == null)
                throw new ArgumentNullException(nameof(parentSelector));

            if (hasParent == null)
                throw new ArgumentNullException(nameof(hasParent));

            
            return new EnumerableTreeNode<T>(value, childrenSelector, parentSelector, hasParent);
        }

        /// <summary>
        /// Provides tree-like navigation for a given value.
        /// </summary>
        /// <param name="value">The value represented by the node.</param>
        /// <param name="childrenSelector">Gets the child values of a given value.</param>
        /// <param name="parentSelector">Gets the parent value of a given value.</param>
        /// <returns>A <see cref="ITreeNode{T}"/> representing <paramref name="value"/>.</returns>
        public static ITreeNode<T> From<T>(
            T value,
            Func<T, IEnumerable<T>> childrenSelector,
            Func<T, T> parentSelector
        )
            where T : class
        {
            return From(value, childrenSelector, parentSelector, hasParent: p => parentSelector(p) != default(T));
        }
    }
}
