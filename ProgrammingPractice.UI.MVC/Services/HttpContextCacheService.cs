using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace ProgrammingPractice.UI.MVC.Services
{
    public static class HttpContextCacheService
    {
        /// <summary>
        /// Determines if the key provided produces a cache or not.
        /// </summary>
        /// <param name="cache">The cache to check.</param>
        /// <param name="key">The cache to search for.</param>
        /// <returns>Whether there is a cache associated with the key or not.</returns>
        /// <exception cref="Exception">If accessing the cache throws any
        /// exception outside a NullReferenceException.</exception>
        public static bool HasCache(Cache cache, string key)
        {
            bool doesExist = false;

            // Surrounded with Try...Catch to avoid throwing a NullReferenceException when accessing the key
            try
            {
                if (!String.IsNullOrEmpty(key) && cache.Count > 0)
                {
                    if (cache[key] != null)
                        doesExist = true;
                }
            }
            catch (NullReferenceException)
            {
                doesExist = false;
                /* Digest the error */
                // We expect a NullReferenceException if attempting to access cache[key] is
                // accessing a null object.  Thus, cache does not exist.
            }
            catch (Exception)
            {
                throw;
            }

            return doesExist;
        }

        /// <summary>
        /// Retrieves the cache value associated with the provided key.
        /// </summary>
        /// <param name="cache">The master cache to retrieve the child cache from.</param>
        /// <param name="key">The cache to search for.</param>
        /// <returns>The value of the cache if one exists.</returns>
        /// <exception cref="NullReferenceException">If the cache is not found.</exception>
        public static object RetrieveCache(Cache cache, string key)
        {
            if (HttpContextCacheService.HasCache(cache, key))
                return cache[key];
            else
                throw new NullReferenceException(string.Format("{0} could not be found for the provided master Cache.", key));
        }
    }
}