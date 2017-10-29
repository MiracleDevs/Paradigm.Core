/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;

namespace Paradigm.Core.Logging
{
    public partial interface ILogging
    {
        /// <summary>
        /// Sets the custom message for a given log type.
        /// </summary>
        /// <remarks>
        /// There are some predefined content placeholders the user can utilize
        /// to configure the custom messages:
        /// {0}: The time when the log was added.
        /// {1}: The type of the log (Trace, Debug , Information, etc)
        /// {2}: A custom tag value provided by the user.
        /// {3}: The log message.
        /// </remarks>
        /// <param name="type">The log type.</param>
        /// <param name="message">The log message.</param>
        void SetCustomMessage(LogType type, string message);

        /// <summary>
        /// Sets a custom format provider.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        void SetCustomFormatProvider(IFormatProvider formatProvider);

        /// <summary>
        /// Sets the minimum log level.
        /// </summary>
        /// <param name="type">The type.</param>
        void SetMinimumLevel(LogType type);

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        void Log(string message, LogType type = LogType.Trace, string tag = null);
    }
}