using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
	}
}
