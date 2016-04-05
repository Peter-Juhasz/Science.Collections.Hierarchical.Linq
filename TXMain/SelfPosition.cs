namespace System.Collections.Hierarchical
{
    /// <summary>
    /// Determines the position of including the selected node.
    /// </summary>
    public enum SelfPosition
    {
        /// <summary>
        /// Returns the selected node as first and then the other nodes.
        /// </summary>
        SelfFirst,

        /// <summary>
        /// Returns all the other nodes first and then the selected node at last.
        /// </summary>
        SelfLast
    }
}
