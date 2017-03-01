using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ProgrammingPractice.UI.MVC.Services
{

    public static class WebConfigurationPropertyService
    {
        #region METHODS
        /// <summary>
        /// Determines if the key provided produces a property or not.
        /// </summary>
        /// <param name="key">The property to search for.</param>
        /// <returns>Whether there is a property associated with the key or not.</returns>
        public static bool HasProperty(string key)
        {
            return !String.IsNullOrEmpty(key)
                && ConfigurationManager.AppSettings.AllKeys.Select((string x) => x).Contains(key);
        }

        /// <summary>
        /// Retrieves the property value associated with the provided key.
        /// </summary>
        /// <param name="key">The property to search for.</param>
        /// <returns>The value of the property if one exists. Returns an empty string otherwise.</returns>
        /// <exception cref="NullReferenceException">If the app setting is not found.</exception>
        public static string ReadProperty(string key)
        {
            if (WebConfigurationPropertyService.HasProperty(key))
                return ConfigurationManager.AppSettings[key];
            else
                throw new NullReferenceException(string.Format("{0} could not be found in the AppSettings.", key));
        }
        #endregion
    }
}