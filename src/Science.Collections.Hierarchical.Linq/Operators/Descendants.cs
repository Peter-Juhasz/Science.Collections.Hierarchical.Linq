using System;
using System.Collections.Generic;
using System.Linq;

namespace Science.Collections.Hierarchical.Linq
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        /// <param name="traverseMode">Determines the order of enumerating descendant nodes in the sub-tree.</param>
        /// <param name="levelTraverseDirection">Determines the order of enumerating child nodes at a single level.</param>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        public static IEnumerable<ITreeNode<T>> Descendants<T>(
            this ITreeNode<T> node,
            TraverseMode traverseMode = TraverseMode.PreOrder,
            LevelTraverseDirection levelTraverseDirection = LevelTraverseDirection.LeftToRight,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            switch (traverseMode)
            {
                case TraverseMode.PreOrder:
                    return node.DescendantsPreOrder(levelTraverseDirection: levelTraverseDirection, includeSelf: includeSelf);

                case TraverseMode.PostOrder:
                    return node.DescendantsPostOrder(levelTraverseDirection: levelTraverseDirection, includeSelf: includeSelf);

                case TraverseMode.LevelOrder:
                    return node.DescendantsLevelOrder(levelTraverseDirection: levelTraverseDirection, includeSelf: includeSelf);

                default: throw new NotSupportedException($"Traverse mode '{traverseMode}' is not supported.");
            }
        }


        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> in pre-order.
        /// </summary>
        /// <param name="levelTraverseDirection">Determines the order of enumerating child nodes at a single level.</param>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        internal static IEnumerable<ITreeNode<T>> DescendantsPreOrder<T>(
            this ITreeNode<T> node,
            LevelTraverseDirection levelTraverseDirection = LevelTraverseDirection.LeftToRight,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            if (includeSelf)
                yield return node;

            foreach (ITreeNode<T> child in node.Children()
                .ReverseIf(levelTraverseDirection == LevelTraverseDirection.RightToLeft)
            )
            {
                yield return child;

                foreach (ITreeNode<T> grandChild in child.DescendantsPreOrder(levelTraverseDirection: levelTraverseDirection, includeSelf: false))
                    yield return grandChild;
            }
        }

        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> in post-order.
        /// </summary>
        /// <param name="levelTraverseDirection">Determines the order of enumerating child nodes at a single level.</param>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        internal static IEnumerable<ITreeNode<T>> DescendantsPostOrder<T>(
            this ITreeNode<T> node,
            LevelTraverseDirection levelTraverseDirection = LevelTraverseDirection.LeftToRight,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            foreach (ITreeNode<T> child in node.Children()
                .ReverseIf(levelTraverseDirection == LevelTraverseDirection.RightToLeft)
            )
            {
                foreach (ITreeNode<T> grandChild in child.DescendantsPostOrder(levelTraverseDirection: levelTraverseDirection, includeSelf: false))
                    yield return grandChild;

                yield return child;
            }

            if (includeSelf)
                yield return node;
        }


        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> in post-order.
        /// </summary>
        /// <param name="levelTraverseDirection">Determines the order of enumerating child nodes at a single level.</param>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        internal static IEnumerable<ITreeNode<T>> DescendantsLevelOrder<T>(
            this ITreeNode<T> node,
            LevelTraverseDirection levelTraverseDirection = LevelTraverseDirection.LeftToRight,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            if (includeSelf)
                yield return node;

            ICollection<ITreeNode<T>> level = new List<ITreeNode<T>> { node };

            while (level.Any())
            {
                level = level.SelectMany(n =>
                    n.Children()
                        .ReverseIf(levelTraverseDirection == LevelTraverseDirection.RightToLeft)
                ).ToList();

                foreach (ITreeNode<T> n in level)
                    yield return n;
            }
        }
    }
}
