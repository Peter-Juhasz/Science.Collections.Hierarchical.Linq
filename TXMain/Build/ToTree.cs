using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Builds a tree using the essential operations and returns the root node.
        /// </summary>
        /// <param name="childrenSelector">Gets the child values of a given value.</param>
        /// <param name="parentSelector">Gets the parent value of a given value.</param>
        /// <param name="hasParent">Determines whether a given value has a parent value or not.</param>
        /// <returns>Root <see cref="ITreeNode{T}"/> of the tree.</returns>
        public static ITreeNode<T> ToTree<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> childrenSelector,
            Func<T, T> parentSelector,
            Func<T, bool> hasParent
        )
        {
            return source.ToForest(childrenSelector, parentSelector, hasParent).Single();
        }

        /// <summary>
        /// Build a tree using a <paramref name="childrenSelector"/> and <paramref name="parentSelector"/>. <code>null</code> value for parent value indicates a root node.
        /// </summary>
        /// <param name="childrenSelector">Gets the child values of a given value.</param>
        /// <param name="parentSelector">Gets the parent value of a given value.</param>
        public static ITreeNode<T> ToTree<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> childrenSelector,
            Func<T, T> parentSelector
        )
            where T : class
        {
            return source.ToForest(childrenSelector, parentSelector).Single();
        }

        /// <summary>
        /// Builds a tree based on foreign key relationships.
        /// </summary>
        /// <typeparam name="T">Type of an entity.</typeparam>
        /// <typeparam name="TKey">Type of the key of an entity.</typeparam>
        /// <param name="keySelector">Selector for the primary key of an entity.</param>
        /// <param name="parentKeySelector">Selector for the foreign key of an entity.</param>
        /// <example>categories.ToTree(c => c.Id, c => c.ParentId)</example>
        public static ITreeNode<T> ToTree<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> keySelector,
            Func<T, TKey> parentKeySelector
        )
            where TKey : IEquatable<TKey>
        {
            return source.ToForest(keySelector, parentKeySelector).Single();
        }

        /// <summary>
        /// Builds a tree based on only a children selector. Note: findig parent nodes can have a high performance impact.
        /// </summary>
        /// <param name="childrenSelector">Selector for selecting child values.</param>
        public static ITreeNode<T> ToTree<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> childrenSelector
        )
        {
            return source.ToForest(childrenSelector).Single();
        }
    }
}
