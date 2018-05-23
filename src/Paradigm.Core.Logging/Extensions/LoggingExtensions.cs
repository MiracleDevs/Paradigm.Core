using System;

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
        public static void Trace(this ILogging logger, string message, string tag = null)
        {
            logger?.Log(message, LogType.Trace, tag);
        }

        /// <summary>
        /// Logs the specified message as debug.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static void Debug(this ILogging logger, string message, string tag = null)
        {
            logger?.Log(message, LogType.Debug, tag);
        }

        /// <summary>
        /// Logs the specified message as information.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static void Information(this ILogging logger, string message, string tag = null)
        {
            logger?.Log(message, LogType.Information, tag);
        }

        /// <summary>
        /// Logs the specified message as warning.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static void Warning(this ILogging logger, string message, string tag = null)
        {
            logger?.Log(message, LogType.Warning, tag);
        }

        /// <summary>
        /// Logs the specified message as error.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static void Error(this ILogging logger, string message, string tag = null)
        {
            logger?.Log(message, LogType.Error, tag);
        }

        /// <summary>
        /// Logs the specified message as critical.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public static void Critical(this ILogging logger, string message, string tag = null)
        {
            logger?.Log(message, LogType.Critical, tag);
        }

        /// <summary>
        /// Logs the specified error.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        public static void Error(this ILogging logger, Exception exception)
        {
            Log(logger, exception);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        public static void Log(this ILogging logger, Exception exception)
        {
            if (exception is AggregateException aggregateException)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    LogException(logger, innerException);
                }
            }
            else
            {
                LogException(logger, exception);
            }
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        private static void LogException(ILogging logger, Exception exception)
        {
            while (exception != null)
            {
                logger.Log($"{exception.GetType().Name}: {exception.Message}", LogType.Error);
                exception = exception.InnerException;
            }
        }
    }
}