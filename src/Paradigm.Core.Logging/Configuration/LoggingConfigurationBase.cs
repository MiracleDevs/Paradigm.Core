using System.Collections.Generic;
using System.Linq;

namespace Paradigm.Core.Logging.Configuration
{
    /// <summary>
    /// Provides the base configuration for all type of loggers.
    /// </summary>
    public abstract class LoggingConfigurationBase
    {
        /// <summary>
        /// Gets or sets the default message.
        /// </summary>
        /// <value>
        /// The default message.
        /// </value>
        public string DefaultMessage { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public Dictionary<LogType, string> Messages { get; set; }

        /// <summary>
        /// Gets or sets the minimum level.
        /// </summary>
        /// <value>
        /// The minimum level.
        /// </value>
        public LogType MinimumLevel { get; set; }

        /// <summary>
        /// Creates a new instance of a logger based on the configuration.
        /// </summary>
        /// <returns>New logger.
        /// </returns>
        public abstract ILogging CreateLogger();

        /// <summary>
        /// Populates the configuration data.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected void PopulateConfigurationData(ILogging logger)
        {
            logger.SetMinimumLevel(this.MinimumLevel);

            if (this.DefaultMessage != null)
            {
                logger.SetCustomMessage(LogType.Trace, this.DefaultMessage);
                logger.SetCustomMessage(LogType.Debug, this.DefaultMessage);
                logger.SetCustomMessage(LogType.Information, this.DefaultMessage);
                logger.SetCustomMessage(LogType.Warning, this.DefaultMessage);
                logger.SetCustomMessage(LogType.Error, this.DefaultMessage);
                logger.SetCustomMessage(LogType.Critical, this.DefaultMessage);
            }
            else if (this.Messages != null && this.Messages.Keys.Any())
            {
                foreach(var logType in this.Messages.Keys)
                {
                    logger.SetCustomMessage(logType, this.Messages[logType]);
                }
            }
        }
    }
}
