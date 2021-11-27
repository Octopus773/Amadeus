namespace Amadeus.Server.Models
{
	/// <summary>
	/// A login request to retrieve an access and a refresh token.
	/// </summary>
	public class LoginRequest
	{
		/// <summary>
		/// The username of the user to log.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The password of the user.
		/// </summary>
		public string Password { get; set; }
	}
}
