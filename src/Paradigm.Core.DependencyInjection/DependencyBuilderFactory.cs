/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Reflection;
using Paradigm.Core.DependencyInjection.Interfaces;

namespace Paradigm.Core.DependencyInjection
{
    public static class DependencyBuilderFactory
    {
        public static IDependencyBuilder Create(DependencyLibrary library, params object[] parameters)
        {
            var stateType = typeof(DependencyBuilderFactory);
            var typeName = $"{stateType.Namespace}.{library}.DependencyBuilder";
            var type = stateType.GetTypeInfo().Assembly.GetType(typeName);

            if (type == null)
                throw new ArgumentException($"The state '{typeName}' type can not be found.");

            return Activator.CreateInstance(type, parameters) as IDependencyBuilder;
        }
    }
}