using System;
using System.Collections.Generic;

namespace Science.Collections.Hierarchical.Linq
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Gets the ancestor <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        /// <param name="includeSelf">Determines whether to include the selected <see cref="ITreeNode{T}"/> or not.</param>
        public static IEnumerable<ITreeNode<T>> Ancestors<T>(
            this ITreeNode<T> node,
            bool includeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            ITreeNode<T> current = node;

            while (!current.IsRoot())
            {
                ITreeNode<T> parent = current.Parent();
                yield return parent;
                current = parent;
            }

            if (includeSelf)
                yield return node;
        }
    }
}
