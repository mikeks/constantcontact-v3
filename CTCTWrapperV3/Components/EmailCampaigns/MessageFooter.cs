using System;
using System.Runtime.Serialization;

namespace CTCT.Components.EmailCampaigns
{
    /// <summary>
    /// Email footer details
    /// </summary>
    [Serializable]
    [DataContract]
    public class MessageFooter : Component
    {
        /// <summary>
        /// Gets or sets the addrese line 1.
        /// </summary>
        [DataMember(Name = "address_line1", EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }
        /// <summary>
        /// Gets or sets the addrese line 2.
        /// </summary>
        [DataMember(Name = "address_line2", EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }
        /// <summary>
        /// Gets or sets the addrese line 3.
        /// </summary>
        [DataMember(Name = "address_line3", EmitDefaultValue = false)]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// An optional address field for the organization. API v3.
        /// </summary>
        [DataMember(Name = "address_optional", EmitDefaultValue = false)]
        public string AddressOptional { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// The uppercase two letter ISO 3166-1 code for the organization's country.
        /// </summary>
        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the organization name.
        /// </summary>
        [DataMember(Name = "organization_name", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// The uppercase two letter ISO 3166-1 code for the organization's state (USA only).
        /// </summary>
        [DataMember(Name = "state_code", EmitDefaultValue = false)]
        public string StateCode { get; set; }


        /// <summary>
        /// Class constructor.
        /// </summary>
        public MessageFooter() { }
    }
}
