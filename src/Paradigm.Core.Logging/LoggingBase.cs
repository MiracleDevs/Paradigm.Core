using System;
using System.Collections.Generic;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides base logging functionality for other type of loggers.
    /// </summary>
    /// <seealso cref="ILogging" />
    public abstract partial class LoggingBase : ILogging
    {
        #region String Messages

        /// <summary>
        /// The type not recognized
        /// </summary>
        protected const string TypeNotRecognized = "The provided type is not recognized as a valida log type.";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        protected Dictionary<LogType, string> Messages { get; }

        /// <summary>
        /// Gets or sets the minimum level.
        /// </summary>
        /// <value>
        /// The minimum level.
        /// </value>
        protected LogType MinimumLevel { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBase"/> class.
        /// </summary>
        protected LoggingBase()
        {
            const string message = "[{0:MM/dd/yyyy hh:mm:ss}][{1}] - {3}{2}\n";

            this.Messages = new Dictionary<LogType, string>()
            {
                { LogType.Trace, message },
                { LogType.Debug, message },
                { LogType.Information, message },
                { LogType.Warning, message },
                { LogType.Error, message },
                { LogType.Critical, message },
            };

            this.MinimumLevel = LogType.Warning;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Sets the custom message for a given log type.
        /// </summary>
        /// <param name="type">The log type.</param>
        /// <param name="message">The log message.</param>
        /// <exception cref="T:System.Exception"></exception>
        /// <remarks>
        /// There are some predefined content placeholders the user can utilize
        /// to configure the custom messages:
        /// {0}: The time when the log was added.
        /// {1}: The type of the log (Trace, Debug , Information, etc)
        /// {2}: A custom tag value provided by the user.
        /// {3}: The log message.
        /// </remarks>
        public void SetCustomMessage(LogType type, string message)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            this.Messages[type] = message;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the minimum log level.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="T:System.Exception"></exception>
        public void SetMinimumLevel(LogType type)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            this.MinimumLevel = type;
        }

        /// <inheritdoc />
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        public abstract void Log(string message, LogType type = LogType.Trace, string tag = null);
       
        #endregion
    }
}