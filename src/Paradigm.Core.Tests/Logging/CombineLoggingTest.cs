using System;
using System.Globalization;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paradigm.Core.Logging;

namespace Paradigm.Core.Tests.Logging
{
    [TestClass]
    public class CombineLoggingTest
    {
        #region Constructor

        [TestMethod]
        public void ShouldCreateNewInstance()
        {
            var logger = new CombineLogging();
            logger.Should().NotBeNull();
        }

        #endregion

        #region Add Loggers

        [TestMethod]
        public void ShouldAddLoggers()
        {
            var logger = new CombineLogging();

            logger.Count.Should().Be(0);
            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());
            logger.Count.Should().Be(2);
        }

        [TestMethod]
        public void ShouldntAddNullLoggers()
        {
            var logger = new CombineLogging();
            logger.Invoking(x => x.AddLogger(null)).Should().Throw<Exception>();
        }

        #endregion

        #region Remove Loggers

        [TestMethod]
        public void ShouldRemoveLoggers()
        {
            var logger = new CombineLogging();

            var logger1 = new FileLogging();
            var logger2 = new ConsoleLogging(); 

            logger.Count.Should().Be(0);
            logger.AddLogger(logger1);
            logger.AddLogger(logger2);
            logger.Count.Should().Be(2);
            logger.RemoveLogger(logger2);
            logger.RemoveLogger(logger1);
            logger.Count.Should().Be(0);
        }

        [TestMethod]
        public void ShouldntRemoveNullLoggers()
        {
            var logger = new CombineLogging();
            logger.Invoking(x => x.RemoveLogger(null)).Should().Throw<Exception>();
        }

        [TestMethod]
        public void ShouldntRemoveALoggerThatWasntAdded()
        {
            var logger = new CombineLogging();
            logger.AddLogger(new FileLogging());
            logger.Invoking(x => x.RemoveLogger(new ConsoleLogging())).Should().Throw<Exception>();
        }

        #endregion

        #region Minimum Level

        [TestMethod]
        public void ShouldSetMinimumLevel()
        {
            var logger = new CombineLogging();
            logger.SetMinimumLevel(LogType.Warning);
        }

        [TestMethod]
        public void ShouldntSetMinimumLevelOfWrongType()
        {
            var logger = new CombineLogging();
            logger.Invoking(x => x.SetMinimumLevel((LogType)10)).Should().Throw<Exception>();
        }

        [TestMethod]
        public void ShouldSetMinimumLevelToAllLoggers()
        {
            var logger = new CombineLogging();

            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());

            logger.SetMinimumLevel(LogType.Warning, true);
        }

        [TestMethod]
        public void ShouldntSetMinimumLevelOfWrongTypeToAllLoggers()
        {
            var logger = new CombineLogging();

            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());

            logger.Invoking(x => x.SetMinimumLevel((LogType)10, true)).Should().Throw<Exception>();
        }

        #endregion

        #region Custom Messages

        [TestMethod]
        public void ShouldSetACustomMessage()
        {
            var logger = new CombineLogging();
            logger.SetCustomMessage(LogType.Critical, "message");
        }

        [TestMethod]
        public void ShouldntSetAWrongTypeInCustomMessage()
        {
            var logger = new CombineLogging();
            logger.Invoking(x => x.SetCustomMessage((LogType)10, "message")).Should().Throw<Exception>();
        }

        [TestMethod]
        public void ShouldntSetANullMessage()
        {
            var logger = new CombineLogging();
            logger.Invoking(x => x.SetCustomMessage(LogType.Critical, null)).Should().Throw<Exception>();
        }

        [TestMethod]
        public void ShouldSetACustomMessageToAllLoggers()
        {
            var logger = new CombineLogging();

            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());

            logger.SetCustomMessage(LogType.Critical, "message", true);
        }

        [TestMethod]
        public void ShouldntSetAWrongTypeInCustomMessageToAllLoggers()
        {
            var logger = new CombineLogging();

            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());

            logger.Invoking(x => x.SetCustomMessage((LogType)10, "message", true)).Should().Throw<Exception>();
        }

        [TestMethod]
        public void ShouldntSetANullMessageToAllLoggers()
        {
            var logger = new CombineLogging();

            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());

            logger.Invoking(x => x.SetCustomMessage(LogType.Critical, null, true)).Should().Throw<Exception>();
        }

        #endregion

        #region Custom Providers

        [TestMethod]
        public void ShouldSetCustomFormatProvider()
        {
            var logger = new CombineLogging();
            logger.SetCustomFormatProvider(new DateTimeFormatInfo());
        }

        [TestMethod]
        public void ShouldSetANullFormatProvider()
        {
            var logger = new CombineLogging();
            logger.SetCustomFormatProvider(null);
        }

        [TestMethod]
        public void ShouldSetCustomFormatProviderToAllLoggers()
        {
            var logger = new CombineLogging();

            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());

            logger.SetCustomFormatProvider(new DateTimeFormatInfo(), true);
        }

        [TestMethod]
        public void ShouldSetANullFormatProviderToAllLoggers()
        {
            var logger = new CombineLogging();

            logger.AddLogger(new FileLogging());
            logger.AddLogger(new ConsoleLogging());

            logger.SetCustomFormatProvider(null, true);
        }

        #endregion

        #region Logging

        [TestMethod]
        public void ShouldLogWithoutErrors()
        {
            var fileName = "test.log";
            var logger = new CombineLogging();

            var fileLogger = new FileLogging();
            fileLogger.SetFileName(fileName);

            logger.AddLogger(fileLogger);
            logger.AddLogger(new ConsoleLogging());

            logger.Log("test message", LogType.Critical);

            File.Exists(fileName).Should().BeTrue();
            File.Delete(fileName);
        }

        #endregion
    }
}
