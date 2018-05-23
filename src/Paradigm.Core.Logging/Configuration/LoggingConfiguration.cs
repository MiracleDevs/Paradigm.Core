using System;

namespace Paradigm.Core.Logging.Configuration
{
    /// <summary>
    /// Provides a way to configure an application logger.
    /// </summary>
    public class LoggingConfiguration
    {
        /// <summary>
        /// Gets or sets the console logger.
        /// </summary>
        /// <value>
        /// The console.
        /// </value>
        public ConsoleLoggingConfiguration Console { get; set; }

        /// <summary>
        /// Gets or sets the file logger.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public FileLoggingConfiguration File { get; set; }

        /// <summary>
        /// Gets or sets the combine logger.
        /// </summary>
        /// <value>
        /// The combine.
        /// </value>
        public CombineLoggingConfiguration Combine { get; set; }

        /// <summary>
        /// Creates a new instance of a logger based on the configuration.
        /// </summary>
        /// <returns>New logger.
        /// </returns>
        public ILogging CreateLogger()
        {
            if (this.Console != null)
                return this.Console.CreateLogger();

            if (this.File != null)
                return this.File.CreateLogger();

            if (this.Combine != null)
                return this.Combine.CreateLogger();

            throw new Exception("The type of logger is not recognized as a valid logger type.");
        }
    }
}