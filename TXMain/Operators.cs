using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Gets the parent value of a <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static T Parent<T>(this ITreeNode<T> node)
        {
            return node.ParentNode().Value;
        }


        /// <summary>
        /// Gets the single root <see cref="ITreeNode{T}"/> from a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static ITreeNode<T> RootNode<T>(this IEnumerable<ITreeNode<T>> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));


            return source.Single(n => n.IsRoot());
        }

        /// <summary>
        /// Gets the single root value from a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static T Root<T>(this IEnumerable<ITreeNode<T>> source)
        {
            return source.RootNode().Value;
        }

        /// <summary>
        /// Determines whether a given <see cref="ITreeNode{T}"/> is a leaf node.
        /// </summary>
        public static bool IsLeaf<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return !node.ChildNodes().Any();
        }

        /// <summary>
        /// Gets all leaf <see cref="ITreeNode{T}"/>s of a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> LeafNodes<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return node.DescendantNodesAndSelf().Where(n => n.IsLeaf());
        }

        /// <summary>
        /// Gets all leaf values of a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static IEnumerable<T> Leaves<T>(this ITreeNode<T> node)
        {
            return node.LeafNodes().Select(n => n.Value);
        }


        /// <summary>
        /// Gets the child values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> Children<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return node.ChildNodes().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the ancestor <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> AncestorNodes<T>(this ITreeNode<T> node) where T : class
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            ITreeNode<T> current = node;

            while (!current.IsRoot())
            {
                ITreeNode<T> parent = current.ParentNode();
                yield return parent;
                current = parent;
            }
        }

        /// <summary>
        /// Gets the ancestor <see cref="ITreeNode{T}"/>s and self of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> AncestorNodesAndSelf<T>(this ITreeNode<T> node) where T : class
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            yield return node;

            foreach (ITreeNode<T> ancestor in node.AncestorNodes())
                yield return ancestor;
        }

        /// <summary>
        /// Gets the ancestor values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> Ancestors<T>(this ITreeNode<T> node) where T : class
        {
            return node.AncestorNodes().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the ancestor values and self of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> AncestorsAndSelf<T>(this ITreeNode<T> node) where T : class
        {
            return node.AncestorNodesAndSelf().Select(n => n.Value);
        }


        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> DescendantNodes<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            foreach (ITreeNode<T> child in node.ChildNodes())
            {
                yield return child;

                foreach (ITreeNode<T> grandChild in node.DescendantNodes())
                    yield return grandChild;
            }
        }
        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> and self.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> DescendantNodesAndSelf<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            yield return node;

            foreach (ITreeNode<T> descendant in node.Descendants())
                yield return descendant;
        }

        /// <summary>
        /// Gets the descendant values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> Descendants<T>(this ITreeNode<T> node)
        {
            return node.DescendantNodes().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the descendant values of a given <see cref="ITreeNode{T}"/> and self.
        /// </summary>
        public static IEnumerable<T> DescendantsAndSelf<T>(this ITreeNode<T> node)
        {
            return node.DescendantNodesAndSelf().Select(n => n.Value);
        }


        /// <summary>
        /// Gets the sibling <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> SiblingNodes<T>(this ITreeNode<T> node)
        {
            return node.SiblingNodesAndSelf().Except(new[] { node });
        }

        /// <summary>
        /// Gets the sibling <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> including self.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> SiblingNodesAndSelf<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            if (node.IsRoot())
                return Enumerable.Empty<ITreeNode<T>>();

            return node.ParentNode().ChildNodes();
        }

        /// <summary>
        /// Gets the sibling values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> Siblings<T>(this ITreeNode<T> node)
        {
            return node.SiblingNodes().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the sibling values of a given <see cref="ITreeNode{T}"/> including self.
        /// </summary>
        public static IEnumerable<T> SiblingsAndSelf<T>(this ITreeNode<T> node)
        {
            return node.SiblingNodesAndSelf().Select(n => n.Value);
        }
    }
}
