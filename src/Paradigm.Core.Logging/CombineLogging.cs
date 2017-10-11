using System;
using System.Collections.Generic;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides a way to combine multiple logging systems at the same time.
    /// </summary>
    /// <seealso cref="ILogging" />
    public partial class CombineLogging : LoggingBase
    {
        #region Properties

        private List<ILogging> Loggers { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CombineLogging"/> class.
        /// </summary>
        public CombineLogging()
        {
            this.Loggers = new List<ILogging>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new logger to the logging collection.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <exception cref="ArgumentNullException">logging - The logger can not be null.</exception>
        public void AddLogger(ILogging logging)
        {
            if (logging == null)
                throw new ArgumentNullException(nameof(logging), "The logger can not be null.");

            this.Loggers.Add(logging);
        }

        /// <summary>
        /// Removes the logger from the logging collection.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <exception cref="ArgumentNullException">logging - The logger can not be null.</exception>
        public void RemoveLogger(ILogging logging)
        {
            if (logging == null)
                throw new ArgumentNullException(nameof(logging), "The logger can not be null.");

            this.Loggers.Remove(logging);
        }

        /// <inheritdoc />
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public override void Log(string message, LogType type = LogType.Trace, string tag = null)
        {
            foreach(var logger in this.Loggers)
            {
                logger.Log(message, type, tag);
            }
        }

        #endregion
    }
}