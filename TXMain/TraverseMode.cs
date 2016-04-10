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
        
        /// <summary>
        /// Visit the child nodes first and then the actual node.
        /// </summary>
        PostOrder,

        /// <summary>
        /// Visit the descendant nodes level by level.
        /// </summary>
        LevelOrder,
    }
}
