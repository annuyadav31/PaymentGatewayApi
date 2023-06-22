using Microsoft.EntityFrameworkCore;
using PaymentGateway.Core.Domain.BankSimulatorContracts;
using PaymentGateway.Infrastructure.Repositories;
using PaymentGatewaySolution.Core.Domain.RepositoryContracts.PaymentDetailsContracts;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;
using PaymentGatewaySolution.Core.Services.PaymentService;
using PaymentGatewaySolution.Infrastructure.Context;
using PaymentGatewaySolution.Infrastructure.Repositories;

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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Added AutoMapper

            //Added Services
            services.AddScoped<IProcessPaymentService, ProcessPaymentService>();
            services.AddScoped<IGetPaymentDetailsService, GetPaymentDetailsService>();
            services.AddScoped<IAddPaymentDetailsRepository, AddPaymentDetailsRepository>();
            services.AddScoped<IGetPaymentDetailsRepository, GetPaymentDetailsRepository>();
            services.AddScoped<IBankSimulator, BankSimulator>();

            //Added Cors
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }
    }
}
