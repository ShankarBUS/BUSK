using System;
using System.ComponentModel;

namespace BUSK.Charting.Defaults
{
    /// <summary>
    /// An already configured chart point, this class notifies the chart to update every time a property changes
    /// </summary>
    public class PolarPoint : INotifyPropertyChanged
    {
        private double _radius;
        private double _angle;

        /// <summary>
        /// Initializes a new instance of PolarPoint class
        /// </summary>
        public PolarPoint()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of PolarPoint class, giving angle and radius
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        public PolarPoint(double radius, double angle)
        {
            Radius = radius;
            Angle = angle;
        }

        /// <summary>
        /// The radius of the point
        /// </summary>
        public double Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                OnPropertyChanged("Radius");
            }
        }

        /// <summary>
        /// The angle of the point
        /// </summary>
        public double Angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                OnPropertyChanged("Angle");
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
