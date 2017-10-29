using System;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides custom formatting for null values.
    /// </summary>
    /// <seealso cref="System.IFormatProvider" />
    /// <seealso cref="System.ICustomFormatter" />
    internal class NullFormat : IFormatProvider, ICustomFormatter
    {
        #region Properties

        /// <summary>
        /// Gets the null string.
        /// </summary>
        private string NullString { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NullFormat"/> class.
        /// </summary>
        /// <param name="nullString">The null string.</param>
        /// <exception cref="ArgumentNullException">nullString</exception>
        public NullFormat(string nullString)
        {
            this.NullString = nullString ?? throw new ArgumentNullException(nameof(nullString));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public object GetFormat(Type service)
        {
            return service == typeof(ICustomFormatter) ? this : null;
        }

        /// <summary>
        /// Formats the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg">The argument.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider provider)
        {
            switch (arg)
            {
                case null:
                    return this.NullString;

                case IFormattable formattable:
                    return formattable.ToString(format, provider);
            }

            return arg.ToString();
        }

        #endregion
    }
}