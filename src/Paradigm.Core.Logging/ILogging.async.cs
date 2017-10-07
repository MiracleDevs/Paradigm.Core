/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System.Threading.Tasks;

namespace Paradigm.Core.Logging
{
    public partial interface ILogging
    {

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        Task LogAsync(string message, LogType type = LogType.Trace, string tag = null);

    }
}