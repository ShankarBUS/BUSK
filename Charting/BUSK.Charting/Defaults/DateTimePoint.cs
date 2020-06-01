using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BUSK.Charting.Defaults
{
    /// <summary>
    /// An already configured chart point with a date time and a double properties, this class notifies the chart to update every time a property changes
    /// </summary>
    public class DateTimePoint : INotifyPropertyChanged
    {
        private DateTime _dateTime;
        private double _value;

        /// <summary>
        /// Initializes a new instance of DateTimePoint class
        /// </summary>
        public DateTimePoint()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of DateTimePoint class, giving date time and value
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="value"></param>
        public DateTimePoint(DateTime dateTime, double value)
        {
            _dateTime = dateTime;
            _value = value;
        }

        /// <summary>
        /// DateTime Property
        /// </summary>
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        /// <summary>
        /// The Value property
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
