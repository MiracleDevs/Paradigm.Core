using System.Collections.Generic;
using System.Linq;

namespace Paradigm.Core.Logging.Configuration
{
    /// <summary>
    /// Provides a way to configure a console logger.
    /// </summary>
    /// <seealso cref="LoggingConfigurationBase" />
    public class ConsoleLoggingConfiguration: LoggingConfigurationBase
    {
        /// <summary>
        /// Gets or sets the console colors for each log type.
        /// </summary>
        /// <value>
        /// The console colors for each log type.
        /// </value>
        public Dictionary<LogType, ConsoleLoggingColor> Colors { get; set; }

        /// <summary>
        /// Creates a new instance of a logger based on the configuration.
        /// </summary>
        /// <returns>
        /// New logger.
        /// </returns>
        public override ILogging CreateLogger()
        {
            var logger = new ConsoleLogging();

            this.PopulateConfigurationData(logger);

            if (this.Colors != null && this.Colors.Keys.Any())
            {
                foreach (var logType in this.Colors.Keys)
                {
                    logger.SetBackgroundColor(logType, this.Colors[logType].BackgroundColor);
                    logger.SetForegroundColor(logType, this.Colors[logType].ForegroundColor);
                }
            }

            return logger;
        }
    }
}