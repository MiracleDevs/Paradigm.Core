/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.IO;
using System.Threading.Tasks;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides application logging functionality storing the logs in the file system.
    /// </summary>
    /// <seealso cref="ILogging" />
    public partial class FileLogging
    {
        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public Task LogAsync(string message, LogType type = LogType.Trace, string tag = null)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            if ((int)type < (int)this.MinimumLevel)
                return Task.FromResult(new object());

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

            return Task.FromResult(new object()); 
        }

        #endregion
    }
}