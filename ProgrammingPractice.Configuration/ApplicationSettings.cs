using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace ProgrammingPractice.Configuration
{
    /// <summary>
    /// Retrieves configuration settings within the AppSettings section.
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// Retrieves all the keys within the AppSettings.
        /// </summary>
        /// <returns>All the keys within the AppSettings.</returns>
        public static string[] AllKeys()
        {
            return ConfigurationManager.AppSettings.AllKeys;
        }

        /// <summary>
        /// Checks if the provided string has a matching key in the AppSettings.
        /// </summary>
        /// <param name="key">The key to search for in the AppSettings.</param>
        /// <returns>Whether or not the key exists in the AppSettings.</returns>
        public static bool HasKey(string key)
        {
            return AllKeys().Contains(key);
        }

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="KeyNotFoundException">If the key is not found in AppSettings.</exception>
        public static string AsString(string key)
        {
            if (!HasKey(key))
                // NOTE: Throwing an exception here will raise bigger errors.  Unsure how to approach an issue of settings not being present.
                throw new KeyNotFoundException($"The following key could not be found in the AppSettings: {key}");
            return ConfigurationManager.AppSettings[key];
        }

        public static char AsChar(string key)
        {
            return char.Parse(AsString(key));
        }

        public static bool AsBool(string key)
        {
            return bool.Parse(AsString(key));
        }

        public static byte AsByte(string key)
        {
            return byte.Parse(AsString(key));
        }

        public static sbyte AsSByte(string key)
        {
            return (sbyte) AsByte(key);
        }

        public static decimal AsDecimal(string key)
        {
            return decimal.Parse(AsString(key));
        }

        public static double AsDouble(string key)
        {
            return double.Parse(AsString(key));
        }

        public static float AsFloat(string key)
        {
            return float.Parse(AsString(key));
        }

        public static int AsInt(string key)
        {
            return int.Parse(AsString(key));
        }

        public static uint AsUInt(string key)
        {
            return (uint) AsInt(key);
        }

        public static long AsLong(string key)
        {
            return long.Parse(AsString(key));
        }

        public static ulong AsULong(string key)
        {
            return (ulong) AsLong(key);
        }

        public static object AsObject(string key)
        {
            return (object) AsString(key);
        }

        public static short AsShort(string key)
        {
            return short.Parse(AsString(key));
        }

        public static ushort AsUShort(string key)
        {
            return (ushort) AsShort(key);
        }
    }
}
