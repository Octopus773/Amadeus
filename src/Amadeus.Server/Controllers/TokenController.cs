// Amadeus.
// Copyright (c) Amadeus.
//
// See AUTHORS.md and LICENSE file in the project root for full license information.
//
// Amadeus is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.
//
// Amadeus is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Kyoo. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Amadeus.Server.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Amadeus.Server.Controllers
{
	/// <summary>
	/// The service that controls jwt creation and validation.
	/// </summary>
	public class TokenController
	{
		/// <summary>
		/// The options that this controller will use.
		/// </summary>
		private readonly IOptions<JwtOption> _options;

		/// <summary>
		/// Create a new <see cref="TokenController"/>.
		/// </summary>
		/// <param name="options">The options that this controller will use.</param>
		public TokenController(IOptions<JwtOption> options)
		{
			_options = options;
		}

		/// <summary>
		/// Create a new access token for the given user.
		/// </summary>
		/// <param name="user">The user to create a token for.</param>
		/// <param name="expireDate">When this token will expire.</param>
		/// <returns>A new, valid access token.</returns>
		public string CreateAccessToken([NotNull] User user, out DateTime expireDate)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			expireDate = DateTime.UtcNow.AddHours(1);

			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.Value.Secret));
			SigningCredentials credential = new(key, SecurityAlgorithms.HmacSha256Signature);
			string permissions = user.Permissions != null
				? string.Join(',', user.Permissions)
				: string.Empty;
			JwtSecurityToken token = new(
				signingCredentials: credential,
				issuer: _options.Value.Issuer.ToString(),
				audience: _options.Value.Issuer.ToString(),
				claims: new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.Role, permissions)
				},
				expires: expireDate.AddYears(3)
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		/// <summary>
		/// Create a new refresh token for the given user.
		/// </summary>
		/// <param name="user">The user to create a token for.</param>
		/// <returns>A new, valid refresh token.</returns>
		public Task<string> CreateRefreshToken([NotNull] User user)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.Value.Secret));
			SigningCredentials credential = new(key, SecurityAlgorithms.HmacSha256Signature);
			JwtSecurityToken token = new(
				signingCredentials: credential,
				issuer: _options.Value.Issuer.ToString(),
				audience: _options.Value.Issuer.ToString(),
				claims: new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
					new Claim("guid", Guid.NewGuid().ToString()),
					new Claim("type", "refresh")
				},
				expires: DateTime.UtcNow.AddYears(1)
			);
			// TODO refresh keys are unique (thanks to the guid) but we could store them in DB to invalidate them.
			return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
		}

		/// <summary>
		/// Check if the given refresh token is valid and if it is, retrieve the id of the user this token belongs to.
		/// </summary>
		/// <param name="refreshToken">The refresh token to validate.</param>
		/// <exception cref="SecurityTokenException">The given refresh token is not valid.</exception>
		/// <returns>The id of the token's user.</returns>
		public int GetRefreshTokenUser(string refreshToken)
		{
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.Value.Secret));
			JwtSecurityTokenHandler tokenHandler = new();
			try
			{
				ClaimsPrincipal a = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					ValidIssuer = _options.Value.Issuer.ToString(),
					ValidAudience = _options.Value.Issuer.ToString(),
					IssuerSigningKey = key
				}, out SecurityToken _);
				Claim identifier = a.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);
				return int.Parse(identifier.Value, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new SecurityTokenException(ex.Message);
			}
		}
	}
}
