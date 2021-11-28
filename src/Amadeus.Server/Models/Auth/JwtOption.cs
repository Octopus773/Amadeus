using System;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// A typed option model for the certificate.
	/// </summary>
	public class JwtOption
	{
		/// <summary>
		/// The path to get this option from the root configuration.
		/// </summary>
		public const string Path = "jwt";

		/// <summary>
		/// The public URL of the issuer (this server).
		/// </summary>
		public Uri Issuer { get; set; }

		/// <summary>
		/// The secret used to encrypt the jwt.
		/// </summary>
		public string Secret { get; set; }
	}
}
