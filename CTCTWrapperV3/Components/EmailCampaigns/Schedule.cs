using System;
using System.Runtime.Serialization;
using CTCT.Util;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Represents a campaign Schedule in Constant Contact class. API v3.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Schedule : Component
    {
		/// <summary>
		/// The scheduled start date/time in ISO 8601 format.
		/// Use "0" as the date to have Constant Contact immediately send the email campaign activity to contacts.
		/// </summary>
		[DataMember(Name = "scheduled_date", EmitDefaultValue = false)]
        private string ScheduledDateString { get; set; }

        /// <summary>
        /// Gets or sets the scheduled date.
        /// </summary>
        public DateTime? ScheduledDate
        {
            get { return ScheduledDateString.FromISO8601String(); }
            set { ScheduledDateString = value.ToISO8601String(); }
        }

        /// <summary>
        /// Schedule to send immidiately
        /// </summary>
        public bool SendImmidiately
        {
            get
            {
                return ScheduledDateString == "0";
            }
            set
            {
                if (value) ScheduledDateString = "0";
            }
        }

    }
}
