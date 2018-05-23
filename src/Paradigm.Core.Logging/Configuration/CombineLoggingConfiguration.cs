using System.Collections.Generic;

namespace Paradigm.Core.Logging.Configuration
{
    /// <summary>
    /// Provides a way to configure a combine logger.
    /// </summary>
    /// <seealso cref="LoggingConfigurationBase" />
    public class CombineLoggingConfiguration: LoggingConfigurationBase
    {
        /// <summary>
        /// Gets or sets the loggers.
        /// </summary>
        /// <value>
        /// The loggers.
        /// </value>
        public List<LoggingConfiguration> Loggers { get; set; }

        /// <summary>
        /// Creates a new instance of a logger based on the configuration.
        /// </summary>
        /// <returns>
        /// New logger.
        /// </returns>
        public override ILogging CreateLogger()
        {
            var logger = new CombineLogging();

            this.PopulateConfigurationData(logger);

            foreach(var childLogger in this.Loggers)
            {
                logger.AddLogger(childLogger.CreateLogger());
            }
            
            return logger;
        }
    }
}