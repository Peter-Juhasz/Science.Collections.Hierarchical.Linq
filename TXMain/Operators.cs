using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Gets the root <see cref="ITreeNode{T}"/>s of a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Roots<T>(this IEnumerable<ITreeNode<T>> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));


            return source.Where(n => n.IsRoot());
        }
        /// <summary>
        /// Gets the single root <see cref="ITreeNode{T}"/> from a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static ITreeNode<T> Root<T>(this IEnumerable<ITreeNode<T>> source)
        {
            return source.Roots().Single();
        }
        

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
        public static IEnumerable<ITreeNode<T>> Leaves<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return node.DescendantsAndSelf().Where(n => n.IsLeaf());
        }


        /// <summary>
        /// Gets the ancestor <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Ancestors<T>(this ITreeNode<T> node)
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
        }

        /// <summary>
        /// Gets the ancestor <see cref="ITreeNode{T}"/>s and self of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> AncestorsAndSelf<T>(
            this ITreeNode<T> node,
            SelfPosition selfPosition = SelfPosition.SelfFirst
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            if (selfPosition == SelfPosition.SelfFirst)
                yield return node;

            foreach (ITreeNode<T> ancestor in node.Ancestors())
                yield return ancestor;

            if (selfPosition == SelfPosition.SelfLast)
                yield return node;
        }


        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Descendants<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            

            foreach (ITreeNode<T> child in node.Children())
            {
                yield return child;

                foreach (ITreeNode<T> grandChild in child.Descendants())
                    yield return grandChild;
            }
        }
        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> and self.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> DescendantsAndSelf<T>(
            this ITreeNode<T> node,
            SelfPosition selfPosition = SelfPosition.SelfFirst
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            if (selfPosition == SelfPosition.SelfFirst)
                yield return node;

            foreach (ITreeNode<T> descendant in node.Descendants())
                yield return descendant;

            if (selfPosition == SelfPosition.SelfLast)
                yield return node;

        }


        /// <summary>
        /// Gets the sibling <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Siblings<T>(this ITreeNode<T> node)
        {
            return node.SiblingsAndSelf().Except(new[] { node });
        }

        /// <summary>
        /// Gets the sibling <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> including self.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> SiblingsAndSelf<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            if (node.IsRoot())
                return Enumerable.Empty<ITreeNode<T>>();

            return node.Parent().Children();
        }


        /// <summary>
        /// Counts the depth of the <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static int Depth<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            

            return node.Ancestors().Count();
        }


        /// <summary>
        /// Executes an action on self and all descendant <see cref="ITreeNode{T}"/>s.
        /// </summary>
        /// <param name="action">Action to execute on each node.</param>
        /// <param name="excludeSelf">Determines whether to exclude the selected <see cref="ITreeNode{T}"/> or not.</param>
        public static void Traverse<T>(
            this ITreeNode<T> node,
            Action<ITreeNode<T>> action,
            bool excludeSelf = false
        )
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (action == null)
                throw new ArgumentNullException(nameof(action));


            foreach (ITreeNode<T> child in excludeSelf
                ? node.Descendants()
                : node.DescendantsAndSelf()
            )
                action(child);
        }
    }
}
