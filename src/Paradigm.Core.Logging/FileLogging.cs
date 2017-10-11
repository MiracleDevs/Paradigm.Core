/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.IO;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides application logging functionality storing the logs in the file system.
    /// </summary>
    /// <seealso cref="ILogging" />
    public partial class FileLogging : LoggingBase
    {
        #region Properties

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

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Paradigm.Core.Logging.FileLogging" /> class.
        /// </summary>
        public FileLogging()
        {
            this.FileName = "systemlog-{0:yyyyMMdd}.log";
            this.Lock = new object();
        }

        #endregion

        #region Public Methods

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
        /// <exception cref="T:System.Exception"></exception>
        public override void Log(string message, LogType type = LogType.Trace, string tag = null)
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