using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// A container representing the response of a login or token refresh.
	/// </summary>
	public class JwtToken
	{
		/// <summary>
		/// The type of this token (always a Bearer).
		/// </summary>
		[JsonProperty("token_type")]
		[JsonPropertyName("token_type")]
		public string TokenType => "Bearer";

		/// <summary>
		/// The access token used to authorize requests.
		/// </summary>
		[JsonProperty("access_token")]
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; }

		/// <summary>
		/// The refresh token used to retrieve a new access/refresh token when the access token has expired.
		/// </summary>
		[JsonProperty("refresh_token")]
		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; }

		/// <summary>
		/// The date when the access token will expire. After this date, the refresh token should be used to retrieve
		/// a new token.
		/// </summary>
		[JsonProperty("expire_in")]
		[JsonPropertyName("expire_in")]
		public TimeSpan ExpireIn { get; set; }
	}
}
