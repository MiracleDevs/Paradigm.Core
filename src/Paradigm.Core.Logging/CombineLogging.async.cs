/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System.Threading.Tasks;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides application logging functionality storing the logs in the file system.
    /// </summary>
    /// <seealso cref="ILogging" />
    public partial class CombineLogging
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
        public override async Task LogAsync(string message, LogType type = LogType.Trace, string tag = null)
        {
            foreach (var logger in this.Loggers)
            {
                await logger.LogAsync(message, type, tag);
            }
        }

        #endregion
    }
}