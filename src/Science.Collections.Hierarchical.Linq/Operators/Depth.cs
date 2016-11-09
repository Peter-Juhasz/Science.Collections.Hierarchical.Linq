using System;
using System.Linq;

namespace Science.Collections.Hierarchical.Linq
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Counts the depth of the <see cref="ITreeNode{T}"/>.
        /// </summary>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        public static int Depth<T>(
            this ITreeNode<T> node,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return node.Ancestors(includeSelf: includeSelf).Count();
        }
    }
}
