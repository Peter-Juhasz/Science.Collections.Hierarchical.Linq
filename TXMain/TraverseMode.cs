namespace System.Collections.Hierarchical
{
    /// <summary>
    /// Determines the order of enumerating the nodes of a sub-tree.
    /// </summary>
    public enum TraverseMode
    {
        /// <summary>
        /// Visit the actual node first and then the child nodes.
        /// </summary>
        PreOrder,
        
        InOrder,

        /// <summary>
        /// Visit the child nodes first and then the actual node.
        /// </summary>
        PostOrder,
    }
}
