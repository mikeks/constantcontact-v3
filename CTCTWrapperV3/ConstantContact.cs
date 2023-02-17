using CTCT.Components.Contacts;
using CTCT.Components.EmailCampaigns;
using CTCT.Services;
using System;

namespace CTCT
{
    /// <summary>
    /// Main class meant to be used by users to access Constant Contact API functionality.
    /// Constant Contact API v.3
    /// Mike Keskinov, 2023
    /// </summary>
    public class ConstantContact
    {
        #region Fields

        private string AccessToken { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the List service.
        /// </summary>
        protected ListService ListService { get; }

        /// <summary>
        /// Gets the Campaign Schedule service.
        /// </summary>
        protected CampaignScheduleService CampaignScheduleService { get; }

        /// <summary>
        /// Gets the Email Campaign service.
        /// </summary>
        protected EmailCampaignService EmailCampaignService { get; }

        /// <summary>
        /// Authentication services
        /// </summary>
        protected AuthService AuthService { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new ConstantContact object using provided apiKey and accessToken parameters
        /// </summary>
        /// <param name="accessToken">Access token</param>
        public ConstantContact(string accessToken)
        {
			AccessToken = accessToken;
			ListService = new ListService();
			CampaignScheduleService = new CampaignScheduleService();
			EmailCampaignService = new EmailCampaignService();
            AuthService = new AuthService();
        }

        #endregion


		#region Public methods

        /// <summary>
        /// Refresh access token using valid Refresh Token
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <param name="refreshToken">Refresh Token</param>
        /// <returns>Access data including new Access Token and Refresh Token</returns>
        public AccessTokenData RefreshAccessToken(string clientId, string clientSecret, string refreshToken)
        {
		    return AuthService.RefreshAccessToken(clientId, clientSecret, refreshToken);  	
		}

		#region List service

		/// <summary>
		/// Get an individual list.
		/// </summary>
		/// <param name="name">Exact name of the contact list</param>
		/// <returns>Returns contact list.</returns>
		public Refresh GetList(string name)
        {
            return ListService.GetList(AccessToken, name);
        }


        #endregion


		#region CampaignSchedule service

		/// <summary>
		/// Schedule campaign to be send immidiately.
		/// </summary>
		/// <param name="campaignActivityId">Campaign activity ID to be scheduled.</param>
		public void ScheduleToSendImmidiately(string campaignActivityId)
        {
            CampaignScheduleService.ScheduleToSendImmidiately(AccessToken, campaignActivityId);
        }

		/// <summary>
		/// Schedule campaign to be send immidiately.
		/// </summary>
		/// <param name="campaignActivityId">Campaign activity ID to be scheduled.</param>
		/// <param name="sendAt">Date/time when the emails should be send out.</param>
		public void ScheduleToSend(string campaignActivityId, DateTime sendAt)
        {
            CampaignScheduleService.Schedule(AccessToken, campaignActivityId, sendAt);
        }

        #endregion

        #region EmailCampaign service

        /// <summary>
        /// Create a new campaign. 
        /// </summary>
        /// <param name="campaign">Campign to be created</param>
        /// <returns>Returns a campaign.</returns>
        public EmailCampaign AddCampaign(EmailCampaign campaign)
        {
            return EmailCampaignService.AddCampaign(AccessToken, campaign);
        }


		/// <summary>
		/// Update a specific email campaign.
		/// </summary>
		/// <param name="campaign">Campaign to be updated.</param>
		/// <returns>Returns updated primary activity.</returns>
		public EmailCampaignActivity UpdateCampaign(EmailCampaign campaign)
        {
            return EmailCampaignService.UpdateCampaign(AccessToken, campaign);
        }

        #endregion
        #endregion

        
    }
}
