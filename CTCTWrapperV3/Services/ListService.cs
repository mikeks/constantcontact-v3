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
    /// Performs all actions pertaining to the Lists Collection
    /// </summary>
    public class ListService : BaseService
    {
       

		/// <summary>
		/// Get list by name.
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token.</param>
		/// <param name="name">Exact contact list name</param>
		/// <returns></returns>
		/// <exception cref="CtctException">Exception</exception>
		public Refresh GetList(string accessToken, string name)
		{
			string url = BASE_URL + $"/contact_lists?include_count=false&name={HttpUtility.UrlEncode(name)}";
			CUrlResponse response = new RestClient().Get(url, accessToken);
			if (response.HasData) return response.Get<ContactListArray>()?.Lists.FirstOrDefault();
			if (response.IsError) throw CtctException.Create(response);
            return null;
		}



    }
}
