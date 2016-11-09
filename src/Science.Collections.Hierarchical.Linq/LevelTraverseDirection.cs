namespace Science.Collections.Hierarchical.Linq
{
    /// <summary>
    /// Determines the order of enumerating the child nodes of a node..
    /// </summary>
    public enum LevelTraverseDirection
    {
        /// <summary>
        /// Enumerat child nodes from left to right.
        /// </summary>
        LeftToRight,

        /// <summary>
        /// Enumerat child nodes from right to left, in a reversed order.
        /// </summary>
        RightToLeft
    }
}
