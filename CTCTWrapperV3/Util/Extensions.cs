using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CTCT.Util
{
    /// <summary>
    /// Extansions class.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// ISO-8601 date time format string.
        /// </summary>
        public const string ISO8601 = "yyyy-MM-ddTHH:mm:ss.sZ";

        /// <summary>
        /// Converts a DateTime object to an ISO8601 representation.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <returns>Returns the ISO8601 string representation for the provided datetime.</returns>
        public static string ToISO8601String(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToUniversalTime().ToString(ISO8601, CultureInfo.InvariantCulture) : null;
        }

        /// <summary>
        /// Gets the DateTime from an ISO8601 string.
        /// </summary>
        /// <param name="str">String.</param>
        /// <returns>Returns a datetime object.</returns>
        public static DateTime? FromISO8601String(this string str)
        {
            DateTime dt;

            return DateTime.TryParseExact(str, ISO8601, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) ? dt : (DateTime?)null;
        }

    }
}
