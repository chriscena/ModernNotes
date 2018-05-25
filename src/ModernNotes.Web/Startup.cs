using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace ModernNotes.Web
{
	/// <summary>
	/// 
	/// </summary>
    public class Startup
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		/// <summary>
		/// 
		/// </summary>
        public IConfiguration Configuration { get; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
			services.AddSwaggerGen(options =>
	        {
				options.SwaggerDoc("v1", new Info {Title="Modern Notes API", Version = "v1"});
				
		        var basePath = PlatformServices.Default.Application.ApplicationBasePath;
		        var xmlPath = Path.Combine(basePath, "ModernNotes.Web.xml");
				options.IncludeXmlComments(xmlPath);
	        });
	        services.AddTransient<INotesRepository, InMemoryNotesRepository>();
        }

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Modern Notes Api V1"));
        }
    }
}
