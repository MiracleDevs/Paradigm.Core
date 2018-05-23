using System;

namespace Paradigm.Core.Logging.Configuration
{
    /// <summary>
    /// Provides a way to configure console colors.
    /// </summary>
    public class ConsoleLoggingColor
    {
        /// <summary>
        /// Gets or sets the color of the foreground.
        /// </summary>
        /// <value>
        /// The color of the foreground.
        /// </value>
        public ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public ConsoleColor BackgroundColor { get; set; }
    }
}