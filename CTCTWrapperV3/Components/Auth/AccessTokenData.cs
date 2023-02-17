using System.Runtime.Serialization;

namespace CTCT.Components.Contacts
{

	/// <summary>
	/// Data returned when access token refreshed
	/// </summary>
	public class AccessTokenData : Component
	{

		/// <summary>
		/// The value is always set to Bearer.
		/// </summary>
		[DataMember(Name = "token_type", EmitDefaultValue = false)]
		public string TokenType { get; set; }

		/// <summary>
		/// The access_token expiration timestamp, in seconds.
		/// </summary>
		[DataMember(Name = "expires_in", EmitDefaultValue = false)]
		public int ExpiresInSec { get; set; }

		/// <summary>
		/// A new access token
		/// </summary>
		[DataMember(Name = "access_token", EmitDefaultValue = false)]
		public string AccessToken { get; set; }

		/// <summary>
		/// API Scopes where you can use access token
		/// </summary>
		[DataMember(Name = "scope", EmitDefaultValue = false)]
		public string Scope { get; set; }

		/// <summary>
		/// A new refresh token
		/// </summary>
		[DataMember(Name = "refresh_token", EmitDefaultValue = false)]
		public string RefreshToken { get; set; }

	}

}
