using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Represents a single Campaign in Constant Contact.
    /// </summary>
    [DataContract]
    public class EmailCampaign : Component
    {
        /// <summary>
        /// The unique and descriptive name that you specify for the email campaign.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The unique ID used to identify the email campaign (UUID format). 
        /// Response only.
        /// </summary>
        [DataMember(Name = "campaign_id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The system generated date and time that this email campaign was created. 
        /// This string is readonly and is in ISO-8601 format.
        /// Response only.
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        /// The system generated date and time showing when the campaign was last updated.
        /// This string is readonly and is in ISO-8601 format.
        /// Response only.
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// The current status of the email campaign.
        /// Response only.
        /// </summary>
        [DataMember(Name = "current_status", EmitDefaultValue = false)]
        public string CurrentStatus { get; set; }

        /// <summary>
        /// Identifies the type of campaign that you select when creating the campaign. 
        /// Response only.
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }


        /// <summary>
        /// The code used to identify the email campaign `type`.
        /// Response only.
        /// </summary>
        [DataMember(Name = "type_code", EmitDefaultValue = false)]
        public string TypeCode { get; set; }

        /// <summary>
        /// A single activity to pass when Creating email campaign (POST)
        /// </summary>
        [DataMember(Name = "email_campaign_activities", EmitDefaultValue = false)]
        public IList<EmailCampaignActivity> EmailCampaignActivities { get; set; }

        /// <summary>
        /// List of all activities, returned by POST Response
        /// </summary>
        [DataMember(Name = "campaign_activities", EmitDefaultValue = false)]
        public IList<EmailCampaignActivity> CampaignActivities { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EmailCampaignActivity GetPrimaryActivity()
			=> CampaignActivities.FirstOrDefault((x) => x.Role == "primary_email");


	}


    /// <summary>
    /// Email campaign activities
    /// </summary>
    [DataContract]
    public class EmailCampaignActivity : Component
    {

        /// <summary>
        /// The ID (UUID) that uniquely identifies a campaign activity.
        /// Used in: POST Response, PUT Request
        /// </summary>
        [DataMember(Name = "campaign_activity_id", EmitDefaultValue = false)]
        public string Id { get; set; }


        /// <summary>
        /// The ID (UUID) that uniquely identifies a campaign activity.
        /// Used in: PUT Request
        /// </summary>
        [DataMember(Name = "campaign_id", EmitDefaultValue = false)]
        public string CampaignId { get; set; }

		/// <summary>
		/// The purpose of the individual campaign activity in the larger email campaign effort.
		/// Used in: POST Response, PUT Request (set to "primary_email").
		/// </summary>
		[DataMember(Name = "role", EmitDefaultValue = false)]
        public string Role { get; set; }



		/// <summary>
		/// The contacts that Constant Contact sends the email campaign activity to as an array of contact list_id values.
		/// Used in: PUT Request 
		/// </summary>
		[DataMember(Name = "contact_list_ids", EmitDefaultValue = false)]
		public string[] ContactListIds { get; set; }


		/// <summary>
		/// Must be 5.
		/// </summary>
		[DataMember(Name = "format_type", EmitDefaultValue = false)]
        public int FormatType => 5;

        /// <summary>
        /// Gets or sets the name from.
        /// </summary>
        [DataMember(Name = "from_name", EmitDefaultValue = false)]
        public string FromName { get; set; }
        /// <summary>
        /// Gets or sets the email from.
        /// </summary>
        [DataMember(Name = "from_email", EmitDefaultValue = false)]
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the reply email address.
        /// </summary>
        [DataMember(Name = "reply_to_email", EmitDefaultValue = false)]
        public string ReplyToEmail { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [DataMember(Name = "subject", EmitDefaultValue = false)]
        public string Subject { get; set; }

        /// <summary>
        /// The email preheader for the email campaign activity. API v3.
        /// </summary>
        [DataMember(Name = "preheader", EmitDefaultValue = false)]
        public string Preheader { get; set; }

        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        [DataMember(Name = "html_content", EmitDefaultValue = false)]
        public string HtmlContent { get; set; }


        /// <summary>
        /// Gets or sets the message footer.
        /// </summary>
        [DataMember(Name = "physical_address_in_footer")]
        public MessageFooter MessageFooter { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public EmailCampaignActivity() { }
    }


}
