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
        public static T ParentValue<T>(this ITreeNode<T> node)
        {
            return node.Parent().Value;
        }


        /// <summary>
        /// Gets the single root <see cref="ITreeNode{T}"/> from a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static ITreeNode<T> Root<T>(this IEnumerable<ITreeNode<T>> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            

            return source.Single(n => n.IsRoot());
        }

        /// <summary>
        /// Gets the single root value from a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static T RootValue<T>(this IEnumerable<ITreeNode<T>> source)
        {
            return source.Root().Value;
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
        /// Gets all leaf values of a set of <see cref="ITreeNode{T}"/>s.
        /// </summary>
        public static IEnumerable<T> LeafValues<T>(this ITreeNode<T> node)
        {
            return node.Leaves().Select(n => n.Value);
        }


        /// <summary>
        /// Gets the child values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> ChildValues<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return node.Children().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the ancestor <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Ancestors<T>(this ITreeNode<T> node) where T : class
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
        public static IEnumerable<ITreeNode<T>> AncestorsAndSelf<T>(this ITreeNode<T> node) where T : class
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            yield return node;

            foreach (ITreeNode<T> ancestor in node.Ancestors())
                yield return ancestor;
        }

        /// <summary>
        /// Gets the ancestor values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> AncestorValues<T>(this ITreeNode<T> node) where T : class
        {
            return node.Ancestors().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the ancestor values and self of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> AncestorValuesAndSelf<T>(this ITreeNode<T> node) where T : class
        {
            return node.AncestorsAndSelf().Select(n => n.Value);
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

                foreach (ITreeNode<T> grandChild in node.Descendants())
                    yield return grandChild;
            }
        }
        /// <summary>
        /// Gets the descendant <see cref="ITreeNode{T}"/>s of a given <see cref="ITreeNode{T}"/> and self.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> DescendantsAndSelf<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            yield return node;

            foreach (ITreeNode<T> descendant in node.DescendantValues())
                yield return descendant;
        }

        /// <summary>
        /// Gets the descendant values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> DescendantValues<T>(this ITreeNode<T> node)
        {
            return node.Descendants().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the descendant values of a given <see cref="ITreeNode{T}"/> and self.
        /// </summary>
        public static IEnumerable<T> DescendantValuesAndSelf<T>(this ITreeNode<T> node)
        {
            return node.DescendantsAndSelf().Select(n => n.Value);
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
        /// Gets the sibling values of a given <see cref="ITreeNode{T}"/>.
        /// </summary>
        public static IEnumerable<T> SiblingValues<T>(this ITreeNode<T> node)
        {
            return node.Siblings().Select(n => n.Value);
        }

        /// <summary>
        /// Gets the sibling values of a given <see cref="ITreeNode{T}"/> including self.
        /// </summary>
        public static IEnumerable<T> SiblingValuesAndSelf<T>(this ITreeNode<T> node)
        {
            return node.SiblingsAndSelf().Select(n => n.Value);
        }
    }
}
