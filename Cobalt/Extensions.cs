﻿using System;
using System.Collections.Generic;

namespace Cobalt
{
    /// <summary>
    /// Adds extension methods for utility.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Makes a for each safe via reverse iterating. Modifying the list won't end in an exception.
        /// </summary>
        /// <param name="list">The list to be iterated</param>
        /// <param name="callback">The callback which gets called on iterate, returning true ends in a break</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        public static void SafeForEach<T>(this IReadOnlyList<T> list, Func<T, bool> callback)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (callback.Invoke(list[i]))
                    break;
            }
        }

        /// <summary>
        /// Makes a for each safe via reverse iterating. Modifying the list won't end in an exception.
        /// </summary>
        /// <param name="list">The list to be iterated</param>
        /// <param name="callback">The callback which gets called on iterate, returning true ends in a break</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        public static void SafeForEach<T>(this List<T> list, Func<T, bool> callback)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (callback.Invoke(list[i]))
                    break;
            }
        }

        /// <summary>
        /// Makes a for each safe via reverse iterating. Modifying the list won't end in an exception.
        /// </summary>
        /// <param name="list">The list to be iterated</param>
        /// <param name="callback">The callback which gets called on iterate</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        public static void SafeForEach<T>(this IReadOnlyList<T> list, Action<T> callback)
        {
            list.SafeForEach(item =>
            {
                callback.Invoke(item);
                return false;
            });
        }

        /// <summary>
        /// Makes a for each safe via reverse iterating. Modifying the list won't end in an exception.
        /// </summary>
        /// <param name="list">The list to be iterated</param>
        /// <param name="callback">The callback which gets called on iterate</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        public static void SafeForEach<T>(this List<T> list, Action<T> callback)
        {
            list.SafeForEach(item =>
            {
                callback.Invoke(item);
                return false;
            });
        }

        /// <summary>
        /// Makes a for each safe via reverse iterating. Modifying the list won't end in an exception.
        /// </summary>
        /// <param name="list">The list to be iterated</param>
        /// <param name="callback">The callback which gets called on iterate</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        public static void SafeForEach<T>(this Array list, Action<T> callback)
        {
            list.SafeForEach<T>(item =>
            {
                callback.Invoke(item);
                return false;
            });
        }

        /// <summary>
        /// Makes a for each safe via reverse iterating. Modifying the list won't end in an exception.
        /// </summary>
        /// <param name="list">The list to be iterated</param>
        /// <param name="callback">The callback which gets called on iterate, returning true ends in a break</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        public static void SafeForEach<T>(this Array list, Func<T, bool> callback)
        {
            for (int i = list.Length - 1; i >= 0; i--)
            {
                object val = list.GetValue(i);
                if (val is T t)
                {
                    if (callback.Invoke(t))
                        break;
                }
            }
        }

        /// <summary>
        /// Selects all items fitting to the given predicate.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="check">The predicate which filters</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        /// <returns>The list containing all fitting elements</returns>
        public static List<T> SelectAll<T>(this IReadOnlyList<T> list, Predicate<T> check)
        {
            var newList = new List<T>();
            list.SafeForEach(item =>
            {
                if (check.Invoke(item))
                    newList.Add(item);
            });
            return newList;
        }

        /// <summary>
        /// Selects all items fitting to the given predicate.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="check">The predicate which filters</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        /// <returns>The list containing all fitting elements</returns>
        public static List<T> SelectAll<T>(this List<T> list, Predicate<T> check)
        {
            var newList = new List<T>();
            list.SafeForEach(item =>
            {
                if (check.Invoke(item))
                    newList.Add(item);
            });
            return newList;
        }

        /// <summary>
        /// Selects the first item fitting to the given predicate.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="check">The predicate which filters</param>
        /// <param name="default">The value which will be returned if no item is found</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        /// <returns>The list containing all fitting elements</returns>
        public static T SelectFirst<T>(this IReadOnlyList<T> list, Predicate<T> check, T @default = default)
        {
            T item = @default;
            list.SafeForEach(o =>
            {
                if (check.Invoke(o))
                {
                    item = o;
                    return true;
                }

                return false;
            });
            return item;
        }

        /// <summary>
        /// Selects the first item fitting to the given predicate.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="check">The predicate which filters</param>
        /// <param name="default">The value which will be returned if no item is found</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        /// <returns>The list containing all fitting elements</returns>
        public static T SelectFirst<T>(this List<T> list, Predicate<T> check, T @default = default)
        {
            T item = @default;
            list.SafeForEach(o =>
            {
                if (check.Invoke(o))
                {
                    item = o;
                    return true;
                }

                return false;
            });
            return item;
        }

        /// <summary>
        /// Joins all strings of the list to one big string merged with the given delimiter.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="delimiter">The delimiter which merges the strings</param>
        /// <returns>The list containing all fitting elements</returns>
        public static string Join(this List<string> list, string delimiter)
        {
            return string.Join(delimiter, list.ToArray());
        }

        /// <summary>
        /// Selects all items fitting to the given predicate.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="collector">The function which converts the given type to the wanted type</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        /// <typeparam name="TN">The type which gets collected</typeparam>
        /// <returns>The list containing all fitting elements</returns>
        public static List<TN> Collect<T, TN>(this List<T> list, Func<T, TN> collector)
        {
            var newList = new List<TN>();
            list.SafeForEach(item => { newList.Add(collector.Invoke(item)); });
            return newList;
        }

        /// <summary>
        /// Selects all items fitting to the given predicate.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="collector">The function which converts the given type to the wanted type</param>
        /// <typeparam name="T">The type of the list elements</typeparam>
        /// <typeparam name="TN">The type which gets collected</typeparam>
        /// <returns>The list containing all fitting elements</returns>
        public static List<TN> Collect<T, TN>(this IReadOnlyList<T> list, Func<T, TN> collector)
        {
            var newList = new List<TN>();
            list.SafeForEach(item => { newList.Add(collector.Invoke(item)); });
            return newList;
        }

        /// <summary>
        /// Checks if the given string value is in the given list without considering the case.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="value">The string value</param>
        /// <returns>True if it is containing the string value</returns>
        public static bool ContainsIgnoreCase(this Array list, string value)
        {
            bool b = false;
            list.SafeForEach<string>(other =>
            {
                if (string.Equals(other, value, StringComparison.CurrentCultureIgnoreCase))
                {
                    b = true;
                    return true;
                }

                return false;
            });
            return b;
        }

        /// <summary>
        /// Checks if the given string value is in the given list without considering the case.
        /// </summary>
        /// <param name="list">The list to be checked</param>
        /// <param name="value">The string value</param>
        /// <returns>True if it is containing the string value</returns>
        public static bool ContainsIgnoreCase(this List<string> list, string value)
        {
            bool b = false;
            list.SafeForEach(other =>
            {
                if (string.Equals(other, value, StringComparison.CurrentCultureIgnoreCase))
                {
                    b = true;
                    return true;
                }

                return false;
            });
            return b;
        }
    }
}