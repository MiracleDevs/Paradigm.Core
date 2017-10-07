/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Linq;
using System.Reflection;

namespace Paradigm.Core.Extensions
{
    /// <summary>
    /// Provides extensions methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the assembly of a given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The assembly where the type is contained.</returns>
        /// <exception cref="System.ArgumentNullException">The type can not be null.</exception>
        public static Assembly GetAssembly(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), $"The {nameof(type)} can not be null.");

            return type.GetTypeInfo().Assembly;
        }

        /// <summary>
        /// Gets the name of a generic type without the `N postfix.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name.</returns>
        /// <exception cref="System.ArgumentNullException">The type can not be null.</exception>
        public static string GetRealGenericName(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), $"The {nameof(type)} can not be null.");

            var name = type.Name;
            var index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }

        /// <summary>
        /// Gets a readable name from a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The readable name.</returns>
        /// <exception cref="System.ArgumentNullException">The type can not be null.</exception>
        public static string GetReadableName(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), $"The {nameof(type)} can not be null.");

            return type.GetTypeInfo().IsGenericType ? $"{type.GetRealGenericName()}<{string.Join(", ", type.GenericTypeArguments.Select(x => x.GetReadableName()))}>" : type.Name;
        }

        /// <summary>
        /// Gets the readable full name from a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The readable full name.</returns>
        /// <exception cref="System.ArgumentNullException">The type can not be null.</exception>
        public static string GetReadableFullName(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), $"The {nameof(type)} can not be null.");

            return $"{type.Namespace}.{type.GetReadableName()}";
        }

        /// <summary>
        /// Determines whether the Type is a numeric type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is numeric; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The type can not be null.</exception>
        public static bool IsNumeric(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), $"The {nameof(type)} can not be null.");

            var typeCode = Type.GetTypeCode(type);

            return typeCode == TypeCode.Byte || typeCode == TypeCode.SByte ||
                   typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 ||
                   typeCode == TypeCode.UInt64 || typeCode == TypeCode.Int16 ||
                   typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 ||
                   typeCode == TypeCode.Decimal || typeCode == TypeCode.Double ||
                   typeCode == TypeCode.Single;
        }
    }
}