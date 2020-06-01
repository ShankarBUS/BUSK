using BUSK.Charting.Definitions.Charts;

namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    public class SeparatorElementCore
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is new.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        public bool IsNew { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public SeparationState State { get; set; }
        /// <summary>
        /// Gets or sets the index of the garbage collector.
        /// </summary>
        /// <value>
        /// The index of the garbage collector.
        /// </value>
        public int GarbageCollectorIndex { get; set; }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public double Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public ISeparatorElementView View { get; set; }
    }
}