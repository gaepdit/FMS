﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FMS
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a string representation of an object. If the object is null, returns an empty string.
        /// </summary>
        /// <param name="input">An object.</param>
        /// <returns>A string.</returns>
        [DebuggerStepThrough]
        public static string ForceToString(this object input) =>
            input == null || string.IsNullOrEmpty(input.ToString()) ? string.Empty : input.ToString();

        /// <summary>
        /// Implodes a collection of strings to a single string, concatenating the items using the separator,
        /// and ignoring null or empty string items.
        /// </summary>
        /// <param name="separator">The separator string to include between each item.</param>
        /// <param name="items">An enumerable collection of strings to concatenate.</param>
        /// <returns>A concatenated string separated by the specified separator. 
        /// Null or empty strings are not included.</returns>
        public static string ConcatNonEmpty(this IEnumerable<string> items, string separator) =>
            string.Join(separator, items.Where(s => !string.IsNullOrEmpty(s)));
    }
}