using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using CTCT.Components;
using Newtonsoft.Json.Linq;

namespace CTCT.Util
{


    /// <summary>
    /// Class implementation of REST client.
    /// </summary>
    public class RestClient 
    {



		private interface IRequestAuth
		{
			void Apply(HttpWebRequest request);
		}

		private class BearerAuth : IRequestAuth
		{
			private string Token { get; }

			public BearerAuth(string token)
			{
				Token = token;
			}

			public void Apply(HttpWebRequest request)
			{
				request.Headers.Add("Authorization", "Bearer " + Token);
			}
		}

		private class BasicAuth : IRequestAuth
		{
			private string Id { get; }
			private string Password { get; }

			public BasicAuth(string id, string password)
			{
				Id = id;
				Password = password;
			}

			public void Apply(HttpWebRequest request)
			{
				request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Id}:{Password}")));
			}
		}





		/// <summary>
		/// Make an Http GET request.
		/// </summary>
		/// <param name="url">Request URL.</param>
		/// <param name="accessToken">Constant Contact OAuth2 access token</param>
		/// <returns>The response body, http info, and error (if one exists).</returns>
		public CUrlResponse Get(string url, string accessToken)
        {
            return HttpRequest(url, WebRequestMethods.Http.Get, accessToken, null);
        }

        /// <summary>
        /// Make an Http POST request.
        /// </summary>
        /// <param name="url">Request URL</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="data">Data to send with request</param>
        /// <returns>The response body, http info, and error (if one exists)</returns>
        public CUrlResponse Post(string url, string accessToken, string data)
            => Post(url, new BearerAuth(accessToken), data);

		/// <summary>
		/// Make an Http POST request using Basic authentication.
		/// </summary>
		/// <param name="url">Request URL</param>
		/// <param name="id">ID or Username</param>
		/// <param name="password">Password</param>
		/// <param name="data">Data to send with request</param>
		/// <returns>The response body, http info, and error (if one exists)</returns>
		public CUrlResponse Post(string url, string id, string password, string data)
			=> Post(url, new BasicAuth(id, password), data);


		private CUrlResponse Post(string url, IRequestAuth auth, string data)
        {
			byte[] bytes = null;

			if(!string.IsNullOrEmpty(data))
			{
				// Convert the request contents to a byte array
				bytes = Encoding.UTF8.GetBytes(data);
			}

            return HttpRequest(url, WebRequestMethods.Http.Post, auth, bytes);
        }



        /// <summary>
        /// Make an Http PUT request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="data">Data to send with request.</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public CUrlResponse Put(string url, string accessToken, string data)
        {
			byte[] bytes = null;

			if(!string.IsNullOrEmpty(data))
			{
				// Convert the request contents to a byte array 
				bytes = Encoding.UTF8.GetBytes(data);
			}

            return HttpRequest(url, WebRequestMethods.Http.Put, accessToken, bytes);
        }

        /// <summary>
        /// Make an Http DELETE request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public CUrlResponse Delete(string url, string accessToken)
        {
            return HttpRequest(url, "DELETE", accessToken, null);
        }

		private CUrlResponse HttpRequest(string url, string method, string accessToken, byte[] data)
            => HttpRequest(url, method, new BearerAuth(accessToken), data);

        /// <summary>
        /// Accept HTTP header
        /// </summary>
        public string Accept { get; set; } = "application/json";

		/// <summary>
		/// ContentType HTTP header
		/// </summary>
		public string ContentType { get; set; } = "application/json";

		private CUrlResponse HttpRequest(string url, string method, IRequestAuth auth, byte[] data)
        {
            // Initialize the response
            HttpWebResponse response = null;
            CUrlResponse urlResponse = new CUrlResponse();

			// TLS 1.2 - https://stackoverflow.com/questions/37869135/is-that-possible-to-send-httpwebrequest-using-tls1-2-on-net-4-0-framework
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			ServicePointManager.DefaultConnectionLimit = 9999;

			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            if (request != null)
            {
                request.Method = method;
                request.Accept = Accept;
                request.ContentType = ContentType;

                auth?.Apply(request);

                if (data != null)
                {
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                // Now try to send the request
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                    // Expect the unexpected
                    if (request.HaveResponse && response == null)
                    {
                        throw new WebException("Response was not returned or is null");
                    }
                    foreach(string header in response.Headers.AllKeys)
                    {
                        urlResponse.Headers.Add(header, response.GetResponseHeader(header));
                    }

                    urlResponse.StatusCode = response.StatusCode;
                    if (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new WebException("Response with status: " + response.StatusCode + " " + response.StatusDescription);
                    }
                }
                catch (WebException e)
                {
                    if (e.Response != null)
                    {
                        response = (HttpWebResponse)e.Response;
                        urlResponse.IsError = true;
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        // Get the response content
                        string responseText;
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            responseText = reader.ReadToEnd();
                        }
                        response.Close();
                        urlResponse.Body = responseText;
                    }
                }
            }

            return urlResponse;
        }
    }
}
