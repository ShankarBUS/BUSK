using System.Windows;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// Contains a collection of already defined axes.
    /// </summary>
    public static class DefaultAxes
    {
        /// <summary>
        /// Returns default axis
        /// </summary>
        public static AxesCollection DefaultAxis
        {
            get { return new AxesCollection {new Axis()}; }
        }

        /// <summary>
        /// Return an axis without separators at all
        /// </summary>
        public static AxesCollection CleanAxis
        {
            get
            {
                return new AxesCollection
                {
                    new Axis
                    {
                        IsEnabled = false,
                        Separator = CleanSeparator
                    }
                };
            }
        }

        /// <summary>
        /// Returns an axis that only displays a line for zero
        /// </summary>
        public static AxesCollection OnlyZerosAxis
        {
            get
            {
                return new AxesCollection
                {
                    new Axis
                    {
                        IsEnabled = true,
                        Separator = CleanSeparator
                    }
                };
            }
        }


        //Returns a clean separator
        /// <summary>
        /// Gets the clean separator.
        /// </summary>
        /// <value>
        /// The clean separator.
        /// </value>
        public static Separator CleanSeparator
        {
            get
            {
                return new Separator
                {
                    Visibility = Visibility.Collapsed
                };
            }
        }

    }
}