using CTCT.Components;
using CTCT.Components.Contacts;
using CTCT.Components.EmailCampaigns;
using CTCT.Exceptions;
using CTCT.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CTCT.Services
{
    /// <summary>
    /// Authentication services
    /// </summary>
    public class AuthService : BaseService
    {

		/// <summary>
		/// Retrieve new Access Token
		/// </summary>
		/// <param name="clientId">Client ID</param>
		/// <param name="clientSecret">Client Secret</param>
		/// <param name="refreshToken">Valid Refresh Token</param>
		/// <returns>New tokens</returns>
		public AccessTokenData RefreshAccessToken(string clientId, string clientSecret, string refreshToken)
		{
			string url = $"https://authz.constantcontact.com/oauth2/default/v1/token?refresh_token={refreshToken}&grant_type=refresh_token";
			CUrlResponse response = new RestClient()
			{
				ContentType = "application/x-www-form-urlencoded"
			}.Post(url, clientId, clientSecret, null);
			if (response.HasData) return response.Get<AccessTokenData>();
			if (response.IsError) throw CtctException.Create(response);
			return null;
		}




	}
}
