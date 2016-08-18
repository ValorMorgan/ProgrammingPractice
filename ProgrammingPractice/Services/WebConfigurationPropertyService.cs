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
        public static string ReadProperty(string key)
        {
            string value = String.Empty;
            if (WebConfigurationPropertyService.HasProperty(key))
                value = ConfigurationManager.AppSettings[key];
            return value;
        }
        #endregion
    }
}