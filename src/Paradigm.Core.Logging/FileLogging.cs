/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides application logging functionality storing the logs in the file system.
    /// </summary>
    /// <seealso cref="ILogging" />
    public partial class FileLogging : ILogging
    {
        #region String Messages

        /// <summary>
        /// The type not recognized
        /// </summary>
        private const string TypeNotRecognized = "The provided type is not recognized as a valida log type.";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        private Dictionary<LogType, string> Messages { get; }

        /// <summary>
        /// Gets or sets the minimum level.
        /// </summary>
        /// <value>
        /// The minimum level.
        /// </value>
        private LogType MinimumLevel { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        private string FileName { get; set; }

        /// <summary>
        /// Gets the lock.
        /// </summary>
        private object Lock { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogging"/> class.
        /// </summary>
        public FileLogging()
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
            this.FileName = "systemlog-{0:yyyyMMdd}.log";
            this.Lock = new object();
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Sets the custom message for a given log type.
        /// </summary>
        /// <param name="type">The log type.</param>
        /// <param name="message">The log message.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
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
        /// <exception cref="T:System.NotImplementedException"></exception>
        public void SetMinimumLevel(LogType type)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            this.MinimumLevel = type;
        }

        /// <summary>
        /// Sets the name of the log file.
        /// </summary>
        /// <remarks>
        /// There are some predefined content placeholders the user can utilize
        /// to configure the custom messages:
        /// {0}: The time when the log was added.
        /// {1}: The type of the log (Trace, Debug , Information, etc)
        /// {2}: A custom tag value provided by the user.
        /// {3}: The log message.
        /// </remarks>
        /// <param name="fileName">Name of the file.</param>
        public void SetFileName(string fileName)
        {
            this.FileName = fileName;
        }

        /// <inheritdoc />
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public void Log(string message, LogType type = LogType.Trace, string tag = null)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            if ((int)type < (int)this.MinimumLevel)
                return;

            var date = DateTime.Now;
            var fileName = Path.GetFullPath(string.Format(this.FileName, date, type, tag, message));
            var formattedMessage = string.Format(this.Messages[type], date, type, tag, message);

            var path = Path.GetDirectoryName(fileName);

            lock (this.Lock)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                File.AppendAllText(fileName, formattedMessage);
            }
        }

        #endregion
    }
}