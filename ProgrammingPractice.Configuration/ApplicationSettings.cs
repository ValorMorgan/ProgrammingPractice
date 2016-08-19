using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using ProgrammingPractice.Interfaces;

namespace ProgrammingPractice.Configuration
{
    /// <summary>
    /// Retrieves configuration settings within the AppSettings section.
    /// </summary>
    public class ApplicationSettings : IApplicationSettings
    {
        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        public string this[string key]
        {
            get {
                if (!HasKey(key))
                    throw new KeyNotFoundException(string.Format("The following key could not be found in the AppSettings: {0}", key));
                return ConfigurationManager.AppSettings[key];
            }
        }

        /// <summary>
        /// Retrieves all the keys within the AppSettings.
        /// </summary>
        /// <returns>All the keys within the AppSettings.</returns>
        public string[] AllKeys()
        {
            return ConfigurationManager.AppSettings.AllKeys;
        }

        /// <summary>
        /// Checks if the provided string has a matching key in the AppSettings.
        /// </summary>
        /// <param name="key">The key to search for in the AppSettings.</param>
        /// <returns>Whether or not the key exists in the AppSettings.</returns>
        public bool HasKey(string key)
        {
            return AllKeys().Contains(key);
        }
    }
}
