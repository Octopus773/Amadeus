using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Amadeus.Server.Data;
using Amadeus.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
			services.AddScoped<UserService>();


			var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("AmadeusDB"));
			// IConfigurationSection contosoPetsCredentials = Configuration.GetSection("ContosoPetsCredentials");
			builder.UserID = "ninja";
			builder.Password = "oui";
			// builder.UserID = Environment.GetEnvironmentVariable("AMADEUS_DB_USER");
			// builder.Password = Environment.GetEnvironmentVariable("AMADEUS_DB_PASSWORD");

			services.AddDbContext<ServerDB>(options => options.UseSqlServer(builder.ConnectionString));
			// .EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging")));

			// TODO check if it's not services.AddControllers();
			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();


			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
