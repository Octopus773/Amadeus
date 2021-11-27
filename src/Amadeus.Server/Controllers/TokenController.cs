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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Amadeus.Server.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Amadeus.Server.Controllers
{
	public class TokenController
	{
		private readonly IOptions<JwtOption> _options;

		public TokenController(IOptions<JwtOption> options)
		{
			_options = options;
		}

		public string BuildToken([NotNull] User user)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.Value.Secret));
			SigningCredentials credential = new(key, SecurityAlgorithms.HmacSha256Signature);
			JwtSecurityToken token = new(
				signingCredentials: credential,
				issuer: _options.Value.Issuer.ToString(),
				claims: new[]
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.Role, string.Join(',', user.Permissions)),
				},
				expires: DateTime.UtcNow.AddHours(1)
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
