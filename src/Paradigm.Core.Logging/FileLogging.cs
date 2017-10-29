/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.IO;

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// Provides application logging functionality storing the logs in the file system.
    /// </summary>
    /// <seealso cref="ILogging" />
    public partial class FileLogging : LoggingBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        private string FileName { get; set; }

        /// <summary>
        /// Gets the lock.
        /// </summary>
        private object Lock { get; }

        /// <summary>
        /// Gets or sets the file header.
        /// </summary>
        private string FileHeader { get; set; }

        #endregion

        #region Constructor

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Paradigm.Core.Logging.FileLogging" /> class.
        /// </summary>
        public FileLogging()
        {
            this.FileHeader = "========================================================================\n" +
                              " System Log File\n" +
                              " Created at: {0:MM/dd/yyyy hh:mm:ss}\n" +
                              "========================================================================\n";

            this.FileName = "systemlog-{0:yyyyMMdd}.log";
            this.Lock = new object();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a File Logger configured to produce csv files.
        /// </summary>
        /// <returns>Preconfigured file logger.</returns>
        public static FileLogging CreateCsv()
        {
            var message = @"""{0:MM/dd/yyyy hh:mm:ss.fff}"", ""{1}"", ""{2}"", ""{3}""" + Environment.NewLine;
            var logger = new FileLogging
            {
                FileName = "systemlog-{0:yyyyMMdd}.csv",
                FileHeader = "date, type, tag, message" + Environment.NewLine
            };

            logger.SetCustomFormatProvider(new NullFormat("NULL"));
            logger.SetCustomMessage(LogType.Trace, message);
            logger.SetCustomMessage(LogType.Debug, message);
            logger.SetCustomMessage(LogType.Information, message);
            logger.SetCustomMessage(LogType.Warning, message);
            logger.SetCustomMessage(LogType.Error, message);
            logger.SetCustomMessage(LogType.Critical, message);

            return logger;
        }

        /// <summary>
        /// Creates a File Logger configured to produce json files.
        /// </summary>
        /// <returns>Preconfigured file logger.</returns>
        public static FileLogging CreateJson()
        {
            string message = "\t\t{{ \"date\": \"{0:MM/dd/yyyy hh:mm:ss.fff}\", \"type\": \"{1}\", \"tag\": \"{2}\", \"message\": \"{3}\" }}," + Environment.NewLine;

            var logger = new FileLogging
            {
                FileName = "systemlog-{0:yyyyMMdd}.json",
                FileHeader = "{{" + Environment.NewLine +
                             "\t\"createdAt\": \"{0:yyyy-MM-ddThh:mm:ss}\"" + Environment.NewLine +
                             "\t\"logs\": [" + Environment.NewLine
            };

            logger.SetCustomFormatProvider(new NullFormat("null"));
            logger.SetCustomMessage(LogType.Trace, message);
            logger.SetCustomMessage(LogType.Debug, message);
            logger.SetCustomMessage(LogType.Information, message);
            logger.SetCustomMessage(LogType.Warning, message);
            logger.SetCustomMessage(LogType.Error, message);
            logger.SetCustomMessage(LogType.Critical, message);

            return logger;
        }


        /// <summary>
        /// Creates a File Logger configured to produce xml files.
        /// </summary>
        /// <returns>Preconfigured file logger.</returns>
        public static FileLogging CreateXml()
        {
            var message = "\t<log date=\"{0:yyyy-MM-dd hh:mm:ss.fff}\" type=\"{1}\" tag=\"{2}\" message=\"{3}\" />" + Environment.NewLine;

            var logger = new FileLogging
            {
                FileName = "systemlog-{0:yyyyMMdd}.xml",
                FileHeader = "<logs createdAt=\"{0:yyyy-MM-dd hh:mm:ss}\">" + Environment.NewLine
            };

            logger.SetCustomMessage(LogType.Trace, message);
            logger.SetCustomMessage(LogType.Debug, message);
            logger.SetCustomMessage(LogType.Information, message);
            logger.SetCustomMessage(LogType.Warning, message);
            logger.SetCustomMessage(LogType.Error, message);
            logger.SetCustomMessage(LogType.Critical, message);

            return logger;
        }

        /// <summary>
        /// Sets the name of the log file.
        /// </summary>
        /// <remarks>
        /// There are some predefined content placeholders the user can utilize
        /// to configure the custom messages:
        /// {0}: The time when the log was added.
        /// {1}: The type of the log (Trace, Debug, Information, etc)
        /// {2}: A custom tag value provided by the user.
        /// {3}: The log message.
        /// </remarks>
        /// <param name="fileName">Name of the file.</param>
        public void SetFileName(string fileName)
        {
            this.FileName = fileName ?? throw new Exception("File name can not be null.");
        }

        /// <summary>
        /// Sets the file header.
        /// </summary>
        /// <param name="header">The header.</param>
        public void SetFileHeader(string header)
        {
            this.FileHeader = header ?? throw new Exception("File header can not be null.");
        }

        /// <inheritdoc />
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <remarks>
        /// The system will provide a date to the header if required using the content placeholder {0}.
        /// </remarks>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">A custom tag value provided by the user.</param>
        /// <exception cref="T:System.Exception"></exception>
        public override void Log(string message, LogType type = LogType.Trace, string tag = null)
        {
            if (!this.Messages.ContainsKey(type))
                throw new Exception(TypeNotRecognized);

            if ((int)type < (int)this.MinimumLevel)
                return;

            var fileName = Path.GetFullPath(this.FormatMessage(this.FileName, message, type, tag));

            lock (this.Lock)
            {
                if (!File.Exists(fileName))
                    this.CreateFile(fileName);

                File.AppendAllText(fileName, this.FormatMessage(this.Messages[type], message, type, tag));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        private string FormatMessage(string format, string message, LogType type, string tag)
        {
            var date = DateTime.Now;
            return this.FormatProvider == null
            ? string.Format(format, date, type, tag, message)
            : string.Format(this.FormatProvider, format, date, type, tag, message);
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void CreateFile(string fileName)
        {
            var path = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.AppendAllText(fileName, string.Format(this.FileHeader, DateTime.Now));
        }

        #endregion
    }
}