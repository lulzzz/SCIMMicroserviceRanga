using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ScimMicroservice.BLL;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.DLL;
using ScimMicroservice.DLL.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
//using Swashbuckle.AspNetCore.Swagger;

namespace ScimMicroservice
{
    public class Startup
    {
        private const string ApiName = "SCIM.API";
        private const int ApiVersionMajor = 1;
        private const int ApiVersionMinor = 0;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(jwtBearerOptions =>
               {
                   jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateActor = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Issuer"],
                       ValidAudience = Configuration["Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]))
                   };
               });

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiName, new Info { Title = ApiName, Version = "1.0" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/" + ApiName + "/swagger.json", GetAPIVersionAsString());
                });
            }

            app.UseETagger();
            app.UseAuthentication();
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
