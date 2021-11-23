using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Amadeus.Server.Authentification;
using Amadeus.Server.Controllers;
using Amadeus.Server.Data;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic.CompilerServices;

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

			services.AddDbContext<ServerDB>(options => options.UseNpgsql(Configuration.GetDatabaseConnection()));

			// configure identity server with in-memory stores, keys, clients and resources
			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
				.AddInMemoryClients(IdentityServerConfig.GetClients());
			services.AddControllers();
			services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{
					options.Authority = "https://localhost:5001";
					options.RequireHttpsMetadata = false;

					options.Audience = "testapi";
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseIdentityServer();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();


			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
