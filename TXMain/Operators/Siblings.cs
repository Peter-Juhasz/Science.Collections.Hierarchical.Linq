using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Gets the sibling <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> including self.
        /// </summary>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        public static IEnumerable<ITreeNode<T>> Siblings<T>(
            this ITreeNode<T> node,
            LevelTraverseDirection direction = LevelTraverseDirection.LeftToRight,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            // root node has no siblings
            if (node.IsRoot())
                return Enumerable.Empty<ITreeNode<T>>();

            // siblings and self
            IEnumerable<ITreeNode<T>> result = node.Parent().Children()
                .ReverseIf(direction == LevelTraverseDirection.RightToLeft);

            // exclude self if not needed
            if (!includeSelf)
                result = result.Except(new[] { node });

            return result;
        }
    }
}
