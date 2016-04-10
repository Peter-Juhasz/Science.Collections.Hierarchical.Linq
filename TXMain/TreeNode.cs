using System.Collections.Generic;

namespace System.Collections.Hierarchical
{
    /// <summary>
    /// Data structure for representing hierarchical data.
    /// </summary>
    public sealed class TreeNode<T> : ITreeNode<T>
    {
        public TreeNode(T value)
        {
            _value = value;

            _children = new TreeNodeCollection<T>(this);
        }

        private T _value;
        private TreeNode<T> _parent;
        private TreeNodeCollection<T> _children;


        /// <summary>
        /// Gets the parent node of the node.
        /// </summary>
        public TreeNode<T> ParentNode => _parent;

        /// <summary>
        /// Gets the child nodes of the node.
        /// </summary>
        public ICollection<TreeNode<T>> ChildNodes => _children;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }


        public bool IsRoot()
        {
            return _parent == null;
        }
        
        public ITreeNode<T> Parent()
        {
            return _parent;
        }

        internal void SetParent(TreeNode<T> parent)
        {
            _parent = parent;
        }


        public IEnumerable<ITreeNode<T>> Children()
        {
            return this.ChildNodes;
        }


        #region IEnumerable<ITreeNode<T>>
        public IEnumerator<ITreeNode<T>> GetEnumerator() => this.Children().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        #endregion


        private sealed class TreeNodeCollection<TValue> : ICollection<TreeNode<TValue>>
        {
            public TreeNodeCollection(TreeNode<TValue> container)
            {
                if (container == null)
                    throw new ArgumentNullException(nameof(container));

                _container = container;
            }

            private readonly TreeNode<TValue> _container;
            private readonly ICollection<TreeNode<TValue>> _innerCollection = new List<TreeNode<TValue>>();


            public int Count => _innerCollection.Count;

            public bool IsReadOnly => false;


            public void Add(TreeNode<TValue> item)
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (item.Parent() != null)
                    throw new InvalidOperationException();

                _innerCollection.Add(item);
                item.SetParent(_container);
            }

            public void Clear()
            {
                foreach (TreeNode<TValue> node in _innerCollection)
                    node.SetParent(null);

                _innerCollection.Clear();
            }

            public bool Contains(TreeNode<TValue> item) => _innerCollection.Contains(item);

            public void CopyTo(TreeNode<TValue>[] array, int arrayIndex) => _innerCollection.CopyTo(array, arrayIndex);

            public bool Remove(TreeNode<TValue> item)
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                bool removed = _innerCollection.Remove(item);
                if (removed)
                    item.SetParent(null);

                return removed;
            }


            public IEnumerator<TreeNode<TValue>> GetEnumerator() => _innerCollection.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
