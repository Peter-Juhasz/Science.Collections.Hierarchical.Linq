using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    /// <summary>
    /// Contains extensions for building trees.
    /// </summary>
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Realizes trees using essential operators.
        /// </summary>
        /// <param name="childrenSelector">Gets the child values of a given value.</param>
        /// <param name="parentSelector">Gets the parent value of a given value.</param>
        /// <param name="hasParent">Determines whether a given value has a parent value or not.</param>
        /// <returns>Root nodes of the realized trees.</returns>
        public static IEnumerable<ITreeNode<T>> ToForest<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> childrenSelector,
            Func<T, T> parentSelector,
            Func<T, bool> hasParent
        )
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (childrenSelector == null)
                throw new ArgumentNullException(nameof(childrenSelector));

            if (parentSelector == null)
                throw new ArgumentNullException(nameof(parentSelector));

            if (hasParent == null)
                throw new ArgumentNullException(nameof(hasParent));


            return source.Select(e => new TreeNode<T>(e, childrenSelector, parentSelector, hasParent)).Roots();
        }

        /// <summary>
        /// Builds trees using a <paramref name="childrenSelector"/> and <paramref name="parentSelector"/>. Null value for parent value indicates a root node.
        /// </summary>
        /// <param name="childrenSelector">Gets the child values of a given value.</param>
        /// <param name="parentSelector">Gets the parent value of a given value.</param>
        /// <returns>Root nodes of the realized trees.</returns>
        public static IEnumerable<ITreeNode<T>> ToForest<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> childrenSelector,
            Func<T, T> parentSelector
        )
            where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (childrenSelector == null)
                throw new ArgumentNullException(nameof(childrenSelector));

            if (parentSelector == null)
                throw new ArgumentNullException(nameof(parentSelector));
            

            return source.ToForest(
                childrenSelector,
                parentSelector,
                hasParent: e2 => parentSelector(e2) != default(T)
            );
        }

        /// <summary>
        /// Realizes trees based on a foreign key relationship.
        /// </summary>
        /// <typeparam name="T">Type of an entity.</typeparam>
        /// <typeparam name="TKey">Type of the key of an entity.</typeparam>
        /// <param name="keySelector">Selector for the primary key of an entity.</param>
        /// <param name="parentKeySelector">Selector for the foreign key of an entity.</param>
        /// <returns>Root nodes of the realized trees.</returns>
        /// <example>categories.ToTree(c => c.Id, c => c.ParentId)</example>
        public static IEnumerable<ITreeNode<T>> ToForest<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> keySelector,
            Func<T, TKey> parentKeySelector
        )
            where TKey : IEquatable<TKey>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            if (parentKeySelector == null)
                throw new ArgumentNullException(nameof(parentKeySelector));
            

            return source.ToForest(
                childrenSelector: c => source.Where(c2 => parentKeySelector(c2).Equals(keySelector(c))),
                parentSelector: c => source.Single(c2 => keySelector(c2).Equals(parentKeySelector(c))),
                hasParent: c => source.Any(c2 => keySelector(c2).Equals(parentKeySelector(c)))
            );
        }

        /// <summary>
        /// Realizes trees based on only a children selector. Note: findig parent nodes can have a high performance impact.
        /// </summary>
        /// <param name="childrenSelector">Selector for selecting child values.</param>
        /// <returns>Root nodes of the realized trees.</returns>
        public static IEnumerable<ITreeNode<T>> ToForest<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> childrenSelector
        )
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (childrenSelector == null)
                throw new ArgumentNullException(nameof(childrenSelector));


            return source.ToForest(
                childrenSelector,
                parentSelector: e => source.SingleOrDefault(e2 => childrenSelector(e2).Contains(e)),
                hasParent: e => source.Any(e2 => childrenSelector(e2).Contains(e))
            );
        }


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


        private sealed class TreeNode<T> : ITreeNode<T>
        {
            public TreeNode(T value, Func<T, IEnumerable<T>> childrenSelector, Func<T, T> parentSelector, Func<T, bool> hasParent)
            {
                _value = value;
                _childrenSelector = childrenSelector;
                _parentSelector = parentSelector;
                _hasParent = hasParent;
            }

            private readonly T _value;
            private readonly Func<T, IEnumerable<T>> _childrenSelector;
            private readonly Func<T, T> _parentSelector;
            private readonly Func<T, bool> _hasParent;

            public bool IsRoot()
            {
                return _hasParent(this.Value);
            }

            public ITreeNode<T> Parent()
            {
                if (!this.IsRoot())
                    return null;

                return new TreeNode<T>(_parentSelector(this.Value), _childrenSelector, _parentSelector, _hasParent);
            }

            public T Value
            {
                get
                {
                    return _value;
                }
            }

            public IEnumerable<ITreeNode<T>> Children()
            {
                return _childrenSelector(_value).Select(e => new TreeNode<T>(e, _childrenSelector, _parentSelector, _hasParent));
            }
        }
    }
}
