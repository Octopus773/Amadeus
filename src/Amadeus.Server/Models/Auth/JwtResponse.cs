using System;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// A container representing the response of a login or token refresh.
	/// </summary>
	public class JwtResponse
	{
		/// <summary>
		/// The access token used to authorize requests.
		/// </summary>
		public string AccessToken { get; set; }

		/// <summary>
		/// The refresh token used to retrieve a new access/refresh token when the access token has expired.
		/// </summary>
		public string RefreshToken { get; set; }

		/// <summary>
		/// The date when the access token will expire. After this date, the refresh token should be used to retrieve
		/// a new token.
		/// </summary>
		public DateTime ExpireTime { get; set; }
	}
}
