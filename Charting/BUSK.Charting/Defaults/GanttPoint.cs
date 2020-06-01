using System;
using System.ComponentModel;

namespace BUSK.Charting.Defaults
{
    /// <summary>
    /// Defines a Gantt point in a cartesian chart
    /// </summary>
    public class GanttPoint : INotifyPropertyChanged
    {
        private double _startPoint;
        private double _endPoint;

        /// <summary>
        /// Initializes a new instance of GanttPoint class.
        /// </summary>
        public GanttPoint()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of GanttPoint class with given start and end points.
        /// </summary>
        public GanttPoint(double startPoint, double endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
         
        /// <summary>
        /// Gets or sets point start
        /// </summary>
        public double StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                OnPropertyChanged("StartPoint");
            }
        }

        /// <summary>
        /// Gets or sets point end
        /// </summary>
        public double EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                OnPropertyChanged("EndPoint");
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