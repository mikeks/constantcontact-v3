using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CTCT.Util;
using CTCT.Components.EmailCampaigns;
using CTCT.Components;
using CTCT.Exceptions;

namespace CTCT.Services
{
    /// <summary>
    /// Performs all actions pertaining to the Contacts Collection.
    /// </summary>
    public class EmailCampaignService : BaseService
    {


        /// <summary>
        /// Create a new campaign.
        /// </summary>
        /// <param name="accessToken">Constant Contact OAuth2 access token.</param>
        /// <param name="campaign">Campign to be created</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign AddCampaign(string accessToken, EmailCampaign campaign)
        {
            EmailCampaign returnCampaign = null;
            string url = BASE_URL + "/emails";
            string json = campaign.ToJSON();
            CUrlResponse response = new RestClient().Post(url, accessToken, json);
            if (response.HasData)           
            {
                returnCampaign = response.Get<EmailCampaign>();
            }
            else if (response.IsError)
            {
                throw CtctException.Create(response);
            }

            // Returned result doesn't contains most of the fields, we need to merge the result and source data
            // in order to use it in the future for campaign update.

            campaign.CreatedAt = returnCampaign.CreatedAt;
            campaign.CurrentStatus = returnCampaign.CurrentStatus;
            campaign.Id = returnCampaign.Id;
            campaign.Type= returnCampaign.Type;
            campaign.TypeCode = returnCampaign.TypeCode;
            campaign.UpdatedAt = returnCampaign.UpdatedAt;

            campaign.CampaignActivities = returnCampaign.CampaignActivities;

            var primaryEmailActivity = campaign.CampaignActivities.FirstOrDefault((x) => x.Role == "primary_email");
            var sourceEmailActivity = campaign.EmailCampaignActivities[0];

			primaryEmailActivity.CampaignId = returnCampaign.Id;
            primaryEmailActivity.FromEmail = sourceEmailActivity.FromEmail;
            primaryEmailActivity.FromName = sourceEmailActivity.FromName;
			primaryEmailActivity.HtmlContent = sourceEmailActivity.HtmlContent;
            primaryEmailActivity.MessageFooter = sourceEmailActivity.MessageFooter;
			primaryEmailActivity.Preheader = sourceEmailActivity.Preheader;
            primaryEmailActivity.ReplyToEmail = sourceEmailActivity.ReplyToEmail;
			primaryEmailActivity.Subject = sourceEmailActivity.Subject;

            campaign.EmailCampaignActivities = null;


			return campaign;
        }


		/// <summary>
		/// Update a specific email campaign.
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token.</param>
		/// <param name="campaign">Campaign to be updated.</param>
		/// <returns>Returns an update campaign activity.</returns>
		public EmailCampaignActivity UpdateCampaign(string accessToken, EmailCampaign campaign)
        {
            var activity = campaign.GetPrimaryActivity();
            if (activity == null)
                throw new Exception("Can't find primary email campaign activity (UpdateCampaign).");

			string url = BASE_URL + $"/emails/activities/{activity.Id}";
			string json = activity.ToJSON();
			CUrlResponse response = new RestClient().Put(url, accessToken, json);

			if (response.HasData) return response.Get<EmailCampaignActivity>();
			if (response.IsError) throw CtctException.Create(response);
            return null;
		}


	}
}
