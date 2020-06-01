namespace BUSK.Charting
{
    /// <summary>
    /// Defines chart updater state
    /// </summary>
    public enum UpdaterState
    {
        /// <summary>
        /// Indicates that the updater is running and listening for changes.
        /// </summary>
        Running,
        /// <summary>
        /// Indicated that the updater is paused, it won't update.
        /// </summary>
        Paused
    }
}