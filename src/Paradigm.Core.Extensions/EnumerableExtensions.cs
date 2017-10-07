/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Paradigm.Core.Extensions
{
    /// <summary>
    /// Provides extensions methods for Enumerable collections.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action for each one of the elements of the collection.
        /// </summary>
        /// <typeparam name="T">The collection item type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="System.ArgumentNullException">The collection can not be null.</exception>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), $"The {nameof(collection)} can not be null.");

            foreach (var element in collection)
                action(element);
        }

        /// <summary>
        /// Performs the specified action for each one of the elements of the collection.
        /// </summary>
        /// <typeparam name="T">The collection item type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="System.ArgumentNullException">The collection can not be null.</exception>
        public static void ForEach<T>(this IEnumerable collection, Action<T> action)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), $"The {nameof(collection)} can not be null.");

            foreach (var element in collection.Cast<T>())
                action(element);
        }

        /// <summary>
        /// Performs the specified action for each one of the elements of the collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="System.ArgumentNullException">The collection can not be null.</exception>
        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), $"The {nameof(collection)} can not be null.");

            foreach (var element in collection)
                action(element);
        }
    }
}