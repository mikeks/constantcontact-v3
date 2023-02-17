using CTCT.Components.EmailCampaigns;
using CTCT.Exceptions;
using CTCT.Util;
using System;

namespace CTCT.Services
{
	/// <summary>
	/// Performs all actions pertaining to scheduling Constant Contact Campaigns.
	/// </summary>
	public class CampaignScheduleService : BaseService
    {


		/// <summary>
		/// Schedule to send campaign immidiately
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token.</param>
		/// <param name="campaignActivityId">Campaign activity ID to be scheduled.</param>
		public void ScheduleToSendImmidiately(string accessToken, string campaignActivityId)
		{
            Schedule(accessToken, campaignActivityId, new Schedule()
            {
                SendImmidiately = true
            });
		}

		/// <summary>
		/// Schedule to send campaign at given time
		/// </summary>
		/// <param name="accessToken">Constant Contact OAuth2 access token.</param>
		/// <param name="campaignActivityId">Campaign activity ID to be scheduled.</param>
		/// <param name="sendAt">Date/time to send emails</param>
		public void Schedule(string accessToken, string campaignActivityId, DateTime sendAt)
		{
            Schedule(accessToken, campaignActivityId, new Schedule()
            {
                ScheduledDate= sendAt
            });
		}


		private void Schedule(string accessToken, string campaignActivityId, Schedule schedule)
		{
			string json = schedule.ToJSON();
			string url = BASE_URL + $"/emails/activities/{campaignActivityId}/schedules";
			CUrlResponse response = new RestClient().Post(url, accessToken, json);
			if (response.IsError) throw CtctException.Create(response);
		}

	}
}
