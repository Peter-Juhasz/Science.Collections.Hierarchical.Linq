using System.Collections.Generic;

namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
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

                default: throw new NotSupportedException($"Traverse mode not supported: {traverseMode}");
            }
        }


        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> in pre-order.
        /// </summary>
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
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> in pre-order.
        /// </summary>
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
    }
}
