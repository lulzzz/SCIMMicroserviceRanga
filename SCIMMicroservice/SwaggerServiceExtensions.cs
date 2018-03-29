﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ScimMicroservice
{
    public static class SwaggerServiceExtensions
    {
        private const string ApiName = "SCIM.API";
        private const int ApiVersionMajor = 1;
        private const int ApiVersionMinor = 0;

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1.0", new Info { Title = "Main API v1.0", Version = "v1.0" });

            //    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //    {
            //        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //        Name = "Authorization",
            //        In = "header",
            //        Type = "apiKey"
            //    });
            //});


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

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "v1.0");

                //c.DocExpansion("none");
            });

            

            return app;
        }
    }
}
