using System.ComponentModel;

namespace BUSK.Charting.Defaults
{
    /// <summary>
    /// An already configured chart point, this class notifies the chart to update every time the value property changes
    /// </summary>
    public class ObservableValue : INotifyPropertyChanged
    {
        private double _value;

        /// <summary>
        /// Initializes a new instance of ObservableValue class
        /// </summary>
        public ObservableValue()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of ObservableValue class with a given value
        /// </summary>
        /// <param name="value"></param>
        public ObservableValue(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Value in he chart
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
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
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}