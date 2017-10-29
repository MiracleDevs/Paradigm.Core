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

        /// <summary>
        /// Gets the loggers.
        /// </summary>
        private List<ILogging> Loggers { get; }

        /// <summary>
        /// Gets how many loggers are registered.
        /// </summary>
        public int Count => this.Loggers.Count;

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
        /// Sets the custom message for a given log type.
        /// </summary>
        /// <param name="type">The log type.</param>
        /// <param name="message">The log message.</param>
        /// <param name="modifyAll">Modifies the custom message for all the loggers.</param>
        /// <remarks>
        /// There are some predefined content placeholders the user can utilize
        /// to configure the custom messages:
        /// {0}: The time when the log was added.
        /// {1}: The type of the log (Trace, Debug , Information, etc)
        /// {2}: A custom tag value provided by the user.
        /// {3}: The log message.
        /// </remarks>
        public void SetCustomMessage(LogType type, string message, bool modifyAll)
        {
            base.SetCustomMessage(type, message);

            if (!modifyAll)
                return;

            foreach(var logger in this.Loggers)
            {
                logger.SetCustomMessage(type, message);
            }
        }

        /// <summary>
        /// Sets the minimum log level.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="modifyAll">Modifies the minimum level for all the loggers.</param>
        public void SetMinimumLevel(LogType type, bool modifyAll)
        {
            base.SetMinimumLevel(type);

            if (!modifyAll)
                return;

            foreach (var logger in this.Loggers)
            {
                logger.SetMinimumLevel(type);
            }
        }

        /// <summary>
        /// Sets the custom format provider.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="modifyAll">Modifies the custom format for all the loggers.</param>
        public void SetCustomFormatProvider(IFormatProvider formatProvider, bool modifyAll)
        {
            base.SetCustomFormatProvider(formatProvider);

            if (!modifyAll)
                return;

            foreach (var logger in this.Loggers)
            {
                logger.SetCustomFormatProvider(formatProvider);
            }
        }

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

            if (this.Loggers.IndexOf(logging) < 0)
                throw new Exception("The logger was not added to this combinator, and can not be deleted.");

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