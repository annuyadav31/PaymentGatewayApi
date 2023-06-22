using Serilog;

namespace PaymentGatewaySolution.Api.Extensions
{
    /// <summary>
    /// Method to add configuration for serilog
    /// </summary>
    public static class SerilogConfiguration
    {
     
        /// <summary>
        /// Extension Method to add serilog for file sink.
        /// </summary>
        /// <param name="host"></param>
        /// <returns>Returns host</returns>
        public static IHostBuilder ConfigureSerilog(this IHostBuilder host)
        {
            host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {

                loggerConfiguration
                .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
                .ReadFrom.Services(services); //read out current app's services and make them available to serilog
            });
            return host;
        }
    }
}
