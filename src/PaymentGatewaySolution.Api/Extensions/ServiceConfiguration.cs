namespace PaymentGatewaySolution.Api.Extensions
{
    /// <summary>
    /// Extension Method to Configure Services
    /// </summary>
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Method to configure services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>Returns Services</returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Added EndPoint and Controller
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            //Added ApplicationDbContext

            //Added AutoMapper

            //Added Services

            //Added Cors
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }
    }
}
