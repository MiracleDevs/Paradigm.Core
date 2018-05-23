using System;
using System.Globalization;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paradigm.Core.Logging;

namespace Paradigm.Core.Tests.Logging
{
    [TestClass]
    public class FileLoggingTest
    {
        #region Constructor

        [TestMethod]
        public void ShouldCreateNewInstance()
        {
            var logger = new FileLogging();
            logger.Should().NotBeNull();
        }

        #endregion

        #region Minimum Level

        [TestMethod]
        public void ShouldSetMinimumLevel()
        {
            var logger = new FileLogging();
            logger.SetMinimumLevel(LogType.Warning);
        }

        [TestMethod]
        public void ShouldntSetMinimumLevelOfWrongType()
        {
            var logger = new FileLogging();
            logger.Invoking(x => x.SetMinimumLevel((LogType)10)).Should().Throw<Exception>();
        }

        #endregion

        #region Custom Messages

        [TestMethod]
        public void ShouldSetACustomMessage()
        {
            var logger = new FileLogging();
            logger.SetCustomMessage(LogType.Critical, "message");
        }

        [TestMethod]
        public void ShouldntSetAWrongTypeInCustomMessage()
        {
            var logger = new FileLogging();
            logger.Invoking(x => x.SetCustomMessage((LogType)10, "message")).Should().Throw<Exception>();
        }

        [TestMethod]
        public void ShouldntSetANullMessage()
        {
            var logger = new FileLogging();
            logger.Invoking(x => x.SetCustomMessage(LogType.Critical, null)).Should().Throw<Exception>();
        }

        #endregion

        #region Custom Providers

        [TestMethod]
        public void ShouldSetCustomFormatProvider()
        {
            var logger = new FileLogging();
            logger.SetCustomFormatProvider(new DateTimeFormatInfo());
        }

        [TestMethod]
        public void ShouldSetANullFormatProvider()
        {
            var logger = new FileLogging();
            logger.SetCustomFormatProvider(null);
        }

        #endregion

        #region File Header

        [TestMethod]
        public void ShouldSetFileHeader()
        {
            var logger = new FileLogging();
            logger.SetFileHeader("File Header");
        }

        [TestMethod]
        public void ShouldntSetNullFileHeader()
        {
            var logger = new FileLogging();
            logger.Invoking(x => x.SetFileHeader(null)).Should().Throw<Exception>();
        }

        #endregion

        #region File Name

        [TestMethod]
        public void ShouldSetFileName()
        {
            var logger = new FileLogging();
            logger.SetFileName("File.txt");
        }

        [TestMethod]
        public void ShouldntSetNullFileName()
        {
            var logger = new FileLogging();
            logger.Invoking(x => x.SetFileName(null)).Should().Throw<Exception>();
        }

        #endregion

        #region Logging

        [TestMethod]
        public void ShouldCreateFileIfNotExists()
        {
            var fileName = "test.log";
            
            File.Exists(fileName).Should().BeFalse();

            var logger = new FileLogging();
            logger.SetFileName(fileName);
            logger.Log("test message", LogType.Critical);

            File.Exists(fileName).Should().BeTrue();
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShouldLogEvenIfFileExists()
        {
            var fileName = "test.log";

            File.AppendAllText(fileName, "creating");

            var logger = new FileLogging();
            logger.SetFileName(fileName);
            logger.Log("test message", LogType.Critical);

            File.Exists(fileName).Should().BeTrue();
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShouldCreateDirectoryIfNotExists()
        {
            var fileName = "log/test.log";

            Directory.Exists("log").Should().BeFalse();

            var logger = new FileLogging();
            logger.SetFileName(fileName);
            logger.Log("test message", LogType.Critical);

            Directory.Exists("log").Should().BeTrue();

            File.Delete(fileName);
            Directory.Delete("log");
        }

        [TestMethod]
        public void ShouldntLogIfMinimumLevelIsHigherThanLog()
        {
            var fileName = "test.log";

            var logger = new FileLogging();
            logger.SetFileName(fileName);
            logger.SetMinimumLevel(LogType.Critical);

            logger.Log("test message", LogType.Debug);

            File.Exists(fileName).Should().BeFalse();
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShouldCreateCsvLogger()
        {
            var fileName = "test.csv";

            var logger = FileLogging.CreateCsv();
            logger.SetFileName(fileName);

            logger.SetMinimumLevel(LogType.Trace);
            logger.Log("This is a log trace");
            logger.Log("This is a log debug", LogType.Debug);
            logger.Log("This is a log information", LogType.Information);
            logger.Log("This is a log warning", LogType.Warning);
            logger.Log("This is a log error", LogType.Error);
            logger.Log("This is a log critical", LogType.Critical);

            File.Exists(fileName).Should().BeTrue();
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShouldCreateJsonLogger()
        {
            var fileName = "test.json";

            var logger = FileLogging.CreateJson();
            logger.SetFileName(fileName);

            logger.SetMinimumLevel(LogType.Trace);
            logger.Log("This is a log trace");
            logger.Log("This is a log debug", LogType.Debug);
            logger.Log("This is a log information", LogType.Information);
            logger.Log("This is a log warning", LogType.Warning);
            logger.Log("This is a log error", LogType.Error);
            logger.Log("This is a log critical", LogType.Critical);

            File.Exists(fileName).Should().BeTrue();
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShouldCreateXmlLogger()
        {
            var fileName = "test.xml";

            var logger = FileLogging.CreateXml();
            logger.SetFileName(fileName);

            logger.SetMinimumLevel(LogType.Trace);
            logger.Log("This is a log trace");
            logger.Log("This is a log debug", LogType.Debug);
            logger.Log("This is a log information", LogType.Information);
            logger.Log("This is a log warning", LogType.Warning);
            logger.Log("This is a log error", LogType.Error);
            logger.Log("This is a log critical", LogType.Critical);

            File.Exists(fileName).Should().BeTrue();
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShouldCreateSimpleLogger()
        {
            var fileName = "test.log";

            var logger = new FileLogging();
            logger.SetFileName(fileName);

            logger.SetMinimumLevel(LogType.Trace);
            logger.Log("This is a log trace");
            logger.Log("This is a log debug", LogType.Debug);
            logger.Log("This is a log information", LogType.Information);
            logger.Log("This is a log warning", LogType.Warning);
            logger.Log("This is a log error", LogType.Error);
            logger.Log("This is a log critical", LogType.Critical);

            File.Exists(fileName).Should().BeTrue();
            File.Delete(fileName);
        }

        #endregion
    }
}
