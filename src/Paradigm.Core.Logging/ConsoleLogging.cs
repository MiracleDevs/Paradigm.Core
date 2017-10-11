using System;
using System.Collections.Generic;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides application logging functionality storing the logs in the file system.
    /// </summary>
    /// <seealso cref="ILogging" />
    public partial class ConsoleLogging : LoggingBase
    {
        #region Properties

        /// <summary>
        /// Gets the message foreground colors.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        private Dictionary<LogType, ConsoleColor> ForegroundColors { get; }

        /// <summary>
        /// Gets the message background colors.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        private Dictionary<LogType, ConsoleColor> BackgroundColors { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogging"/> class.
        /// </summary>
        public ConsoleLogging()
        {
            this.ForegroundColors = new Dictionary<LogType, ConsoleColor>()
            {
                { LogType.Trace, ConsoleColor.Cyan },
                { LogType.Debug, ConsoleColor.Magenta },
                { LogType.Information, ConsoleColor.Gray },
                { LogType.Warning, ConsoleColor.Yellow },
                { LogType.Error, ConsoleColor.Red },
                { LogType.Critical, ConsoleColor.White },
            };

            this.BackgroundColors = new Dictionary<LogType, ConsoleColor>()
            {
                { LogType.Trace, ConsoleColor.Black },
                { LogType.Debug, ConsoleColor.Black },
                { LogType.Information, ConsoleColor.Black },
                { LogType.Warning, ConsoleColor.Black },
                { LogType.Error, ConsoleColor.Black },
                { LogType.Critical, ConsoleColor.Red }
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the console foreground color for a specific message type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="color">The foreground color.</param>
        /// <exception cref="Exception"></exception>
        public void SetForegroundColor(LogType type, ConsoleColor color)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            this.ForegroundColors[type] = color;
        }

        /// <summary>
        /// Sets the console background color for a specific message type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="color">The background color.</param>
        /// <exception cref="Exception"></exception>
        public void SetBackgroundColor(LogType type, ConsoleColor color)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            this.BackgroundColors[type] = color;
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
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            if ((int)type < (int)this.MinimumLevel)
                return;

            var date = DateTime.Now;
            var formattedMessage = string.Format(this.Messages[type], date, type, tag, message);

            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = this.ForegroundColors[type];
            Console.BackgroundColor = this.BackgroundColors[type];

            Console.Write(formattedMessage);

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }

        #endregion
    }
}