using System.Linq;

namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Counts the depth of the <see cref="ITreeNode{T}"/>.
        /// </summary>
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
