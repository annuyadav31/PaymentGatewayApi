using Microsoft.EntityFrameworkCore;
using PaymentGateway.Core.Exceptions;
using PaymentGatewaySolution.Core.Domain.Models;
using PaymentGatewaySolution.Core.Domain.RepositoryContracts.PaymentDetailsContracts;
using PaymentGatewaySolution.Infrastructure.Context;

namespace PaymentGateway.Infrastructure.Repositories
{
    public class AddPaymentDetailsRepository : IAddPaymentDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public AddPaymentDetailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Add Payment to Database
        /// </summary>
        /// <param name="payment">Object which has to be added</param>
        /// <returns>Returns payment object</returns>
        public async Task<Payment> AddPayment(Payment payment)
        {
            _db.Payments?.Add(payment);
            if (payment.CardDetails != null)
            {
                _db.CardDetails?.Add(payment.CardDetails);
            }
            await _db.SaveChangesAsync();
            return payment;
        }
    }
}
