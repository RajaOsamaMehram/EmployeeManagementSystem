using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.ComponentModel;

namespace EmployeeManagementSystem
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}



		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});

			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; });
			services.AddSignalR();

          //  var connectionString = builder.Configuration.getConnectionString("DefaultConnection");
          //  var app = ApplicationBuilder.build();

            // Read Setting from AppSettings.json files
            services.AddOptions();
			//services.AddTransient<IEmailSender, SendMail>();


			services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

			services.AddControllersWithViews();
			services.AddControllers();
			services.AddAuthorization();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseRouting();
			app.UseCors("CorsPolicy");
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapDefaultControllerRoute();
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");

			});

			//app.UseEndpoints(endpoints =>
			//{
			//    endpoints.MapControllers();
			//    endpoints.MapRazorPages();
			//    endpoints.MapBlazorHub();
			//    endpoints.MapFallbackToPage("/_Host");
			//});
			//context.Database.Migrate();

		}

	}
}
