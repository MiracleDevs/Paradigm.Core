/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;

namespace Paradigm.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the next day of the week from a given date.
        /// </summary>
        /// <remarks>
        /// This method is helpful if you need to calculate a future date based on the day of the week.
        /// For example, what date will be the next sunday, or the next monday.
        /// </remarks>
        /// <param name="date">The day from which the next week day needs to be calculated.</param>
        /// <param name="day">The desired day of the week.</param>
        /// <returns>A date time object representing the new day of week.</returns>
        /// <seealso cref="GetPreviousWeekDay(DateTime, DayOfWeek)"/>
        public static DateTime GetNextWeekDay(this DateTime date, DayOfWeek day)
        {
            date = new DateTime(date.Year, date.Month, date.Day);

            var daysUntil = ((int)day - (int)date.DayOfWeek + 7) % 7;

            return date.AddDays(daysUntil);
        }

        /// <summary>
        /// Gets the previous day of the week from a given date.
        /// </summary>
        /// <remarks>
        /// This method is helpful if you need to calculate a past date based on the day of the week.
        /// For example, what date was last sunday, or last monday.
        /// </remarks>
        /// <param name="date">The day from which the past week day needs to be calculated.</param>
        /// <param name="day">The desired day of the week.</param>
        /// <returns>A date time object representing the new day of week.</returns>
        /// <seealso cref="GetNextWeekDay(DateTime, DayOfWeek)"/>
        public static DateTime GetPreviousWeekDay(this DateTime date, DayOfWeek day)
        {
            date = new DateTime(date.Year, date.Month, date.Day);

            var daysUntil = ((int)date.DayOfWeek - (int)day + 7) % 7;

            return date.AddDays(-daysUntil);
        }
    }
}
