namespace BUSK.Charting.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAxisSectionView
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        AxisSectionCore Model { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        double Value { get; set; }
        /// <summary>
        /// Gets or sets the width of the section.
        /// </summary>
        /// <value>
        /// The width of the section.
        /// </value>
        double SectionWidth { get; set; }
        /// <summary>
        /// Gets or sets the section offset.
        /// </summary>
        /// <value>
        /// The section offset.
        /// </value>
        double SectionOffset { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IAxisSectionView"/> is draggable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if draggable; otherwise, <c>false</c>.
        /// </value>
        bool Draggable { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the section is animated
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disable animations]; otherwise, <c>false</c>.
        /// </value>
        bool DisableAnimations { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the section should display a label that displays its current value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [data label]; otherwise, <c>false</c>.
        /// </value>
        bool DataLabel { get; set; }

        /// <summary>
        /// Draws the or move.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="axis">The axis.</param>
        void DrawOrMove(AxisOrientation source, int axis);
        /// <summary>
        /// Removes this instance.
        /// </summary>
        void Remove();
        /// <summary>
        /// Ases the core element.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AxisSectionCore AsCoreElement(AxisCore axis, AxisOrientation source);
    }
}