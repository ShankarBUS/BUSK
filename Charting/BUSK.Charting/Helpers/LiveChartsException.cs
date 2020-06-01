using System;

namespace BUSK.Charting.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BUSKChartingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BUSK.ChartingException"/> class.
        /// </summary>
        public BUSKChartingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BUSK.ChartingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BUSKChartingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BUSK.ChartingException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public BUSKChartingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BUSK.ChartingException"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="args">The arguments.</param>
        public BUSKChartingException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
