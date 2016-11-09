using System;
using System.Collections.Generic;
using System.Linq;

namespace Science.Collections.Hierarchical.Linq
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Determines whether a given <see cref="ITreeNode{T}"/> is a leaf node.
        /// </summary>
        public static bool IsLeaf<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return !node.Children().Any();
        }

        /// <summary>
        /// Gets all leaf <see cref="ITreeNode{T}"/>s of a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        public static IEnumerable<ITreeNode<T>> Leaves<T>(
            this ITreeNode<T> node,

            LevelTraverseDirection levelTraverseDirection = LevelTraverseDirection.LeftToRight,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return node.Descendants(
                levelTraverseDirection: levelTraverseDirection,
                includeSelf: includeSelf
            ).Where(n => n.IsLeaf());
        }
    }
}
