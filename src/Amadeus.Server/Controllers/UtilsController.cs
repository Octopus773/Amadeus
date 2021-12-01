using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Amadeus.Server.Controllers
{
	/// <summary>
	/// Controller used to store utils function.
	/// </summary>
	public static class UtilsController
	{
		/// <summary>
		/// Slugify a string (Replace spaces by -, Uniformize accents)
		/// </summary>
		/// <param name="str">The string to slugify</param>
		/// <returns>The slug version of the given string</returns>
		public static string ToSlug([NotNull] string str)
		{
			if (str == null)
				return null;

			str = str.ToLowerInvariant();

			string normalizedString = str.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new();
			foreach (char c in normalizedString)
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
					stringBuilder.Append(c);
			}
			str = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

			str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled);
			str = Regex.Replace(str, @"[^\w\s\p{Pd}]", string.Empty, RegexOptions.Compiled);
			str = str.Trim('-', '_');
			str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);
			return str;
		}

		/// <summary>
		/// get local ip address.
		/// </summary>
		/// <returns>The local ip address.</returns>
		/// <exception cref="Exception">If.</exception>
		public static string GetLocalIpAddress()
		{
			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("No network adapters with an IPv4 address in the system!");
		}
	}
}
