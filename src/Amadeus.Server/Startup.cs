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
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Amadeus.Server.Controllers;
using Amadeus.Server.Controllers.Weather;
using Amadeus.Server.Data;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Amadeus.Server.Models.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using NSwag;

namespace Amadeus.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IRepository<User>, UserRepository>();
			services.AddScoped<IRepository<Widget>, WidgetRepository>();
			services.AddTransient<TokenController>();
			services.AddScoped<AboutController>();

			services.Configure<WeatherConfiguration>(Configuration.GetSection(nameof(WeatherConfiguration)));
			services.AddScoped<WeatherController>();

			services.AddDbContext<ServerDB>(options => options.UseNpgsql(Configuration.GetDatabaseConnection()));

			services.AddMvcCore()
				.AddAuthorization();
			services.AddControllers();

			JwtOption jwt = new();
			Configuration.GetSection(JwtOption.Path).Bind(jwt);
			services.Configure<JwtOption>(Configuration.GetSection(JwtOption.Path));

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwt.Issuer.ToString(),
						ValidAudience = jwt.Issuer.ToString(),
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
					};
				});

			services.AddOpenApiDocument(document =>
			{
				document.Title = "Amadeus API";
				// TODO use a real multi-line description in markdown.
				document.Description = "The Amadeus's public API";
				document.Version = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);
				document.DocumentName = "v1";
				document.UseControllerSummaryAsTagDescription = true;
				document.GenerateExamples = true;
				document.PostProcess = options =>
				{
					options.Info.Contact = new OpenApiContact
					{
						Name = "Amadeus's github",
						Url = "https://github.com/Octopus773/Amadeus"
					};
					// options.Servers.Add(new OpenApiServer
					// {
					// 	Url = _configuration.GetPublicUrl().ToString(),
					// 	Description = "The currently running kyoo's instance."
					// });
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
		{
			ServerDB context = provider.GetRequiredService<ServerDB>();
			context.Database.Migrate();
			using NpgsqlConnection conn = (NpgsqlConnection)context.Database.GetDbConnection();
			conn.Open();
			conn.ReloadTypes();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseOpenApi();
			app.UseSwaggerUi3();

			app.UseCors(x => x.AllowAnyOrigin());
			// app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
