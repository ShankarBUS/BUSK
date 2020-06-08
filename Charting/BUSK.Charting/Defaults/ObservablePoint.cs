using System.ComponentModel;

namespace BUSK.Charting.Defaults
{
    /// <summary>
    /// An already configured chart point, this class notifies a chart to update every time a property changes
    /// </summary>
    public class ObservablePoint : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        /// <summary>
        /// Initializes a new instance of ObservablePoint class
        /// </summary>
        public ObservablePoint()
        {
            
        }
        
        /// <summary>
        /// Initializes a new instance of ObservablePoint class giving the x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public ObservablePoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X coordinate
        /// </summary>
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        #region INotifyPropertyChangedImplementation

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
