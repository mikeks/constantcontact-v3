using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Util;
using System.Collections.Specialized;
using System.Web;

namespace CTCT.Services
{
    /// <summary>
    /// Super class for all services.
    /// </summary>
    public abstract class BaseService
    {
		
        /// <summary>
        /// Base URL for API v3
        /// </summary>
        protected const string BASE_URL = "https://api.cc.email/v3";

       
        /// <summary>
        /// Class constructor.
        /// </summary>
        public BaseService()
        {
        }

        /// <summary>
        /// Constructs the query with specified parameters.
        /// </summary>
        /// <param name="prms">An array of parameter name and value combinations.</param>
        /// <returns>Returns the query part of the URL.</returns>
        public static string GetQueryParameters(params object[] prms)
        {
            string query = null;
            if (prms != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < prms.Length; i += 2)
                {
                    if (prms[i + 1] == null)
                        continue;

                    if (sb.Length == 0)
                    {
                        sb.Append("?");
                    }
                    else if (sb.Length > 0)
                    {
                        sb.Append("&");
                    }
                    sb.AppendFormat("{0}={1}", prms[i].ToString(), prms[i + 1].ToString());
                }
                query = sb.ToString();
            }

            return query;
        }
    }
}
