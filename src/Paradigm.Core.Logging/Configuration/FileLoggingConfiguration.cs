namespace Paradigm.Core.Logging.Configuration
{
    /// <summary>
    /// Provides a way to configure a file logger.
    /// </summary>
    /// <seealso cref="LoggingConfigurationBase" />
    public class FileLoggingConfiguration: LoggingConfigurationBase
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file header.
        /// </summary>
        /// <value>
        /// The file header.
        /// </value>
        public string FileHeader { get; set; }

        /// <summary>
        /// Creates a new instance of a logger based on the configuration.
        /// </summary>
        /// <returns>
        /// New logger.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ILogging CreateLogger()
        {
            var logger = new FileLogging();

            this.PopulateConfigurationData(logger);

            logger.SetFileName(this.FileName);
            logger.SetFileHeader(this.FileHeader);

            return logger;
        }
    }
}