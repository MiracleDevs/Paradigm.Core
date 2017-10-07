/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

namespace Paradigm.Core.Logging
{
    /// <summary>
    /// provides different types of logs.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// For information that is valuable only to a developer debugging an issue. 
        /// These messages may contain sensitive application data and so should not be enabled in a production environment. 
        /// </summary>
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5
    }
}