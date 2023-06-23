using Microsoft.EntityFrameworkCore;
using PaymentGateway.Core.Exceptions;
using PaymentGatewaySolution.Core.Domain.Models;
using PaymentGatewaySolution.Core.Domain.RepositoryContracts.PaymentDetailsContracts;
using PaymentGatewaySolution.Infrastructure.Context;

namespace PaymentGatewaySolution.Infrastructure.Repositories
{
    public class GetPaymentDetailsRepository : IGetPaymentDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public GetPaymentDetailsRepository(ApplicationDbContext db) {
            _db = db;
        }
        public async Task<Payment> GetPayment(Guid paymentId)
        {
            var payment = await _db.Payments.Include("CardDetails").Where(temp => temp.TransactionID == paymentId).FirstOrDefaultAsync();

            return payment;
        }
    }
}
