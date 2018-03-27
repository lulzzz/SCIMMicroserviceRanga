using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScimMicroservice.BLL;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.DLL;
using ScimMicroservice.DLL.Interfaces;

namespace ScimMicroservice
{
    public class Startup
    {
        private const string ApiName = "scim";
        private const int ApiVersionMajor = 0;
        private const int ApiVersionMinor = 1;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SCIMContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();


            // Enable versioning and fall back to default when unspecified.
            //services.AddApiVersioning(options =>
            //{
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.DefaultApiVersion = new ApiVersion(ApiVersionMajor, ApiVersionMinor);
            //});


            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseETagger();
            app.UseMvc();
        }

        /// <summary>
        /// Returns the API version as a string
        /// </summary>
        /// <returns>Current API verison</returns>
        private string GetAPIVersionAsString()
        {
            return ("v" + ApiVersionMajor.ToString() + "." + ApiVersionMinor.ToString()).ToLower();
        }
    }
}
