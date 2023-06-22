using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;

namespace PaymentGatewaySolution.Api.Extensions
{
    /// <summary>
    /// Extension class for swagger configuration
    /// </summary>
    public static class SwaggerConfiguration
    {

        /// <summary>
        /// Method to configure swagger for services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>Returns Services</returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentGateway", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), configuration.GetValue<string>("ApiDocumentationPath:FilePath")));
            }
            );

            services.AddSwaggerGen(options =>
             {
                 
             });
            return services;
        }

        /// <summary>
        /// Method to configure swagger for application
        /// </summary>
        /// <param name="application"></param>
        /// <returns>Returns Application</returns>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGateway v1"));
            return application;
        }
    }
}
