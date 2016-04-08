namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Executes an action on self and all descendant <see cref="ITreeNode{T}"/>s.
        /// </summary>
        /// <param name="action">Action to execute on each node.</param>
        /// <param name="traverseMode">Determines the order of enumerating descendant nodes in the sub-tree.</param>
        /// <param name="levelTraverseDirection">Determines the order of enumerating child nodes at a single level.</param>
        /// <param name="excludeSelf">Determines whether to exclude the selected <see cref="ITreeNode{T}"/> or not.</param>
        public static void Traverse<T>(
            this ITreeNode<T> node,

            Action<ITreeNode<T>> action,

            TraverseMode traverseMode = TraverseMode.PreOrder,
            LevelTraverseDirection levelTraverseDirection = LevelTraverseDirection.LeftToRight,
            bool excludeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (action == null)
                throw new ArgumentNullException(nameof(action));


            foreach (ITreeNode<T> child in node.Descendants(
                traverseMode: traverseMode,
                levelTraverseDirection: levelTraverseDirection,
                includeSelf: !excludeSelf
            ))
                action(child);
        }
    }
}
