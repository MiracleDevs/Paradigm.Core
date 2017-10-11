using System.Threading.Tasks;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides base logging functionality for other type of loggers.
    /// </summary>
    /// <seealso cref="ILogging" />
    public abstract partial class LoggingBase
    {
        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        public abstract Task LogAsync(string message, LogType type = LogType.Trace, string tag = null);

        #endregion
    }
}