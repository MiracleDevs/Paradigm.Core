using System;
using System.Globalization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paradigm.Core.Logging;

namespace Paradigm.Core.Tests.Logging
{
    [TestClass]
    public class ConsoleLoggingTest
    {
        #region Constructor

        [TestMethod]
        public void ShouldCreateNewInstance()
        {
            var logger = new ConsoleLogging();
            logger.Should().NotBeNull();
        }

        #endregion

        #region Minimum Level

        [TestMethod]
        public void ShouldSetMinimumLevel()
        {
            var logger = new ConsoleLogging();
            logger.SetMinimumLevel(LogType.Warning);
        }

        [TestMethod]
        public void ShouldntSetMinimumLevelOfWrongType()
        {
            var logger = new ConsoleLogging();
            logger.Invoking(x => x.SetMinimumLevel((LogType)10)).ShouldThrow<Exception>();
        }

        #endregion

        #region Custom Messages

        [TestMethod]
        public void ShouldSetACustomMessage()
        {
            var logger = new ConsoleLogging();
            logger.SetCustomMessage(LogType.Critical, "message");
        }

        [TestMethod]
        public void ShouldntSetAWrongTypeInCustomMessage()
        {
            var logger = new ConsoleLogging();
            logger.Invoking(x => x.SetCustomMessage((LogType)10, "message")).ShouldThrow<Exception>();
        }

        [TestMethod]
        public void ShouldntSetANullMessage()
        {
            var logger = new ConsoleLogging();
            logger.Invoking(x => x.SetCustomMessage(LogType.Critical, null)).ShouldThrow<Exception>();
        }

        #endregion

        #region Custom Providers

        [TestMethod]
        public void ShouldSetCustomFormatProvider()
        {
            var logger = new ConsoleLogging();
            logger.SetCustomFormatProvider(new DateTimeFormatInfo());
        }

        [TestMethod]
        public void ShouldSetANullFormatProvider()
        {
            var logger = new ConsoleLogging();
            logger.SetCustomFormatProvider(null);
        }

        #endregion

        #region Logging

        [TestMethod]
        public void ShouldLogWithoutErrors()
        { 
            var logger = new ConsoleLogging();
            logger.Log("test message", LogType.Critical);
        }

        #endregion
    }
}
