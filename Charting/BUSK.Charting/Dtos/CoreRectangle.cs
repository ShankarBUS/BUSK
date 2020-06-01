using System;

namespace BUSK.Charting.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class CoreRectangle
    {
        private double _left;
        private double _top;
        private double _width;
        private double _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreRectangle"/> class.
        /// </summary>
        public CoreRectangle()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreRectangle"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public CoreRectangle(double left, double top, double width, double height) : this()
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Occurs when [set top].
        /// </summary>
        public event Action<double> SetTop;
        /// <summary>
        /// Occurs when [set left].
        /// </summary>
        public event Action<double> SetLeft;
        /// <summary>
        /// Occurs when [set width].
        /// </summary>
        public event Action<double> SetWidth;
        /// <summary>
        /// Occurs when [set height].
        /// </summary>
        public event Action<double> SetHeight;

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                if (SetLeft != null) SetLeft.Invoke(value);
            }
        }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                if (SetTop != null) SetTop.Invoke(value);
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value < 0 ? 0 : value;
                if (SetWidth != null) SetWidth.Invoke(value);
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value < 0 ? 0 : value;
                if (SetHeight != null) SetHeight.Invoke(value);
            }
        }
    }
}