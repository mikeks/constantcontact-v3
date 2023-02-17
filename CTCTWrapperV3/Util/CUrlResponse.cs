using CTCT.Components;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CTCT.Util
{
    /// <summary>
    /// URL response class.
    /// </summary>
    public class CUrlResponse
    {
        /// <summary>
        /// Requests body.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Returns true if error occur.
        /// </summary>
        public bool IsError { get; set; }

		/// <summary>
		/// Returns true if access token is not valid or expired
		/// </summary>
		public bool IsAuthError => IsError && Body?.Contains("\"error_key\":\"unauthorized\"") == true;

        /// <summary>
        /// Returns true if valid data exists.
        /// </summary>
        public bool HasData { get { return !IsError && !string.IsNullOrEmpty(Body); } }
        /// <summary>
        /// Response status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
		/// <summary>
		/// Headers dictionary.
		/// </summary>
		public Dictionary<string, string> Headers { get; set; }
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        public CUrlResponse()
        {
            IsError = false;
			Headers = new Dictionary<string,string>();
        }

        /// <summary>
        /// Returns the list of errors.
        /// </summary>
        /// <returns>Returns formatted error message.</returns>
        public string GetErrorMessage() => Body;

        /// <summary>
        /// Returns the object represented by the JSON string.
        /// </summary>
        /// <typeparam name="T">Object type to return.</typeparam>
        /// <returns>Returns the object from its JSON representation.</returns>
        public T Get<T>() where T : class
        {
            T t = default(T);
            if (HasData && !String.IsNullOrEmpty(Body))
            {
                t = Component.FromJSON<T>(Body);
            }

            return t;
        }
    }
}
