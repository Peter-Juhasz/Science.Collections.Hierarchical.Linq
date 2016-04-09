using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    public static partial class TreeNodeExtensions
    {
        /// <summary>
        /// Evaluates navigation operators only once, but still lazy. Memoizing tree nodes can increase performance.
        /// </summary>
        public static ITreeNode<T> Memoize<T>(this ITreeNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));


            return new MemoizedTreeNode<T>(node);
        }
    }

    internal sealed class MemoizedTreeNode<T> : ITreeNode<T>
    {
        internal MemoizedTreeNode(
            ITreeNode<T> node,
            MemoizedTreeNode<T> parent = null
        )
        {
            _node = node;
            _memoizedParent = parent;

            _isRoot = new Lazy<bool>(node.IsRoot);
            _parent = new Lazy<MemoizedTreeNode<T>>(() => new MemoizedTreeNode<T>(node.Parent()));
            _value = new Lazy<T>(() => node.Value);
            _children = new Lazy<IEnumerable<MemoizedTreeNode<T>>>(() =>
                node.Children()
                    .Select(c => new MemoizedTreeNode<T>(c, parent: this))
                    .ToList()
            );
        }

        private readonly ITreeNode<T> _node;
        private readonly MemoizedTreeNode<T> _memoizedParent;
        private readonly IEnumerable<MemoizedTreeNode<T>> _memoizedChildren;

        private readonly Lazy<bool> _isRoot;
        private readonly Lazy<MemoizedTreeNode<T>> _parent;
        private readonly Lazy<T> _value;
        private readonly Lazy<IEnumerable<MemoizedTreeNode<T>>> _children;

        public bool IsRoot()
        {
            return _isRoot.Value;
        }

        public ITreeNode<T> Parent()
        {
            if (!this.IsRoot())
                return null;

            return _memoizedParent ?? _parent.Value;
        }

        public T Value
        {
            get
            {
                return _value.Value;
            }
        }

        public IEnumerable<ITreeNode<T>> Children()
        {
            return _children.Value;
        }


        #region IEnumerable<ITreeNode<T>>
        public IEnumerator<ITreeNode<T>> GetEnumerator() => this.Children().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        #endregion
    }
}
