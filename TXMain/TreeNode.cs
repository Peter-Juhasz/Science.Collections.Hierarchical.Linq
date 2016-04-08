using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Hierarchical
{
    /// <summary>
    /// Contains extensions for building trees.
    /// </summary>
    public static partial class TreeNodeExtensions
    {
        private sealed class TreeNode<T> : ITreeNode<T>
        {
            public TreeNode(T value, Func<T, IEnumerable<T>> childrenSelector, Func<T, T> parentSelector, Func<T, bool> hasParent)
            {
                _value = value;
                _childrenSelector = childrenSelector;
                _parentSelector = parentSelector;
                _hasParent = hasParent;
            }

            private readonly T _value;
            private readonly Func<T, IEnumerable<T>> _childrenSelector;
            private readonly Func<T, T> _parentSelector;
            private readonly Func<T, bool> _hasParent;

            public bool IsRoot()
            {
                return _hasParent(this.Value);
            }

            public ITreeNode<T> Parent()
            {
                if (!this.IsRoot())
                    return null;

                return new TreeNode<T>(_parentSelector(this.Value), _childrenSelector, _parentSelector, _hasParent);
            }

            public T Value
            {
                get
                {
                    return _value;
                }
            }

            public IEnumerable<ITreeNode<T>> Children()
            {
                return _childrenSelector(_value).Select(e => new TreeNode<T>(e, _childrenSelector, _parentSelector, _hasParent));
            }
        }
    }
}
