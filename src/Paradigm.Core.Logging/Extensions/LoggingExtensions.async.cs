using System;
using System.Threading.Tasks;

namespace Paradigm.Core.Logging.Extensions
{
    public static partial class LoggingExtensions
    {
        /// <summary>
        /// Logs the specified message as trace.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static async Task TraceAsync(this ILogging logger, string message, string tag = null)
        {
            if (logger == null)
                return;

            await logger.LogAsync(message, LogType.Trace, tag);
        }

        /// <summary>
        /// Logs the specified message as debug.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static async Task DebugAsync(this ILogging logger, string message, string tag = null)
        {
            if (logger == null)
                return;

            await logger.LogAsync(message, LogType.Debug, tag);
        }

        /// <summary>
        /// Logs the specified message as information.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static async Task InformationAsync(this ILogging logger, string message, string tag = null)
        {
            if (logger == null)
                return;

            await logger.LogAsync(message, LogType.Information, tag);
        }

        /// <summary>
        /// Logs the specified message as warning.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static async Task WarningAsync(this ILogging logger, string message, string tag = null)
        {
            if (logger == null)
                return;

            await logger.LogAsync(message, LogType.Warning, tag);
        }

        /// <summary>
        /// Logs the specified message as error.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static async Task ErrorAsync(this ILogging logger, string message, string tag = null)
        {
            if (logger == null)
                return;

            await logger.LogAsync(message, LogType.Error, tag);
        }

        /// <summary>
        /// Logs the specified message as critical.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static async Task CriticalAsync(this ILogging logger, string message, string tag = null)
        {
            if (logger == null)
                return;

            await logger.LogAsync(message, LogType.Critical, tag);
        }

        /// <summary>
        /// Logs the specified error.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        public static async Task ErrorAsync(this ILogging logger, Exception exception)
        {
            if (logger == null)
                return;

            await LogAsync(logger, exception);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        public static async Task LogAsync(this ILogging logger, Exception exception)
        {
            if (logger == null)
                return;

            if (exception is AggregateException aggregateException)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    await LogExceptionAsync(logger, innerException);
                }
            }
            else
            {
                await LogExceptionAsync(logger, exception);
            }
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        private static async Task LogExceptionAsync(ILogging logger, Exception exception)
        {
            if (logger == null)
                return;

            while (exception != null)
            {
                await logger.LogAsync($"{exception.GetType().Name}: {exception.Message}", LogType.Error);
                exception = exception.InnerException;
            }
        }
    }
}