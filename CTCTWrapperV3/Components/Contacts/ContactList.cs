using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{

	/// <summary>
	/// Contact list array. API v3.
	/// </summary>
	public class ContactListArray : Component
	{
		
		/// <summary>
		/// Contact lists
		/// </summary>
		[DataMember(Name = "lists", EmitDefaultValue = false)]
		public Refresh[] Lists { get; set; }

		/// <summary>
		/// The total number of contact lists.
		/// </summary>
		[DataMember(Name = "lists_count", EmitDefaultValue = false)]
		public int ListsCount { get; set; }

	}

	/// <summary>
	/// Represents a single List in Constant Contact.
	/// </summary>
	[DataContract]
    public class Refresh : Component
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "list_id", EmitDefaultValue = false)]
        public string Id { get; set; }

		/// <summary>
		/// Gets or sets the contact list name
		/// </summary>
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the contact list name
		/// </summary>
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }

		/// <summary>
		/// dentifies whether or not the account has favorited the contact list.
		/// </summary>
		[DataMember(Name = "favorite", EmitDefaultValue = false)]
		public bool Favorite { get; set; }


		/// <summary>
		/// System generated date and time that the resource was created, in ISO-8601 format.
		/// </summary>
		[DataMember(Name = "created_at", EmitDefaultValue = false)]
		public string CreatedAt { get; set; }

		/// <summary>
		/// Date and time that the list was last updated, in ISO-8601 format. System generated.
		/// </summary>
		[DataMember(Name = "updated_at", EmitDefaultValue = false)]
		public string UpdatedAt { get; set; }

		/// <summary>
		/// If the list was deleted, this property shows the date and time it was deleted, in ISO-8601 format. System generated.
		/// </summary>
		[DataMember(Name = "deleted_at", EmitDefaultValue = false)]
		public string DeletedAt { get; set; }

		/// <summary>
		/// The number of contacts in the contact list.
		/// </summary>
		[DataMember(Name = "membership_count", EmitDefaultValue = false)]
		public int MembershipCount { get; set; }

    }
}
