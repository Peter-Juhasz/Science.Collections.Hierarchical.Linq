using System.Collections.Generic;

namespace System.Collections.Hierarchical
{
    /// <summary>
    /// Provides navigation for a value of type <see cref="T"/> in a hierarchy.
    /// </summary>
    /// <typeparam name="T">Type of the value represented by the <see cref="ITreeNode{T}"/>.</typeparam>
    public interface ITreeNode<T>
    {
        /// <summary>
        /// Determines whether this <see cref="ITreeNode{T}"/> is a root.
        /// </summary>
        bool IsRoot();

        /// <summary>
        /// Gets the parent <see cref="ITreeNode{T}"/> of a <see cref="ITreeNode{T}"/>.
        /// </summary>
        /// <returns></returns>
        ITreeNode<T> Parent();

        /// <summary>
        /// Gets the value represented by a <see cref="ITreeNode{T}"/>.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Gets the children <see cref="ITreeNode{T}"/>s of a <see cref="ITreeNode{T}"/>.
        /// </summary>
        IEnumerable<ITreeNode<T>> ChildNodes();
    }
}
