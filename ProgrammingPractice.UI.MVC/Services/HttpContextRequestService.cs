using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingPractice.UI.MVC.Services
{
    public static class HttpContextRequestService
    {
        /// <summary>
        /// Determines if the key provided produces a parameter or not.
        /// </summary>
        /// <param name="request">The request to check.</param>
        /// <param name="key">The parameter to search for.</param>
        /// <returns>Whether there is a parameter associated with the key or not.</returns>
        public static bool HasParameter(HttpRequestBase request, string key)
        {
            return !String.IsNullOrEmpty(key)
                && request.Params.AllKeys.Select((string x) => x).Contains(key);
        }

        /// <summary>
        /// Retrieves the parameter value associated with the provided key.
        /// </summary>
        /// <param name="request">The request to retrieve the parameter from.</param>
        /// <param name="key">The parameter to search for.</param>
        /// <returns>The value of the parameter if one exists.</returns>
        /// <exception cref="NullReferenceException">If the parameter is not found.</exception>
        public static string ReadParameter(HttpRequestBase request, string key)
        {
            if (HttpContextRequestService.HasParameter(request, key))
                return request.Params[key];
            else
                throw new NullReferenceException(string.Format("{0} could not be found for the provided HttpRequestBase.", key));
        }
    }
}