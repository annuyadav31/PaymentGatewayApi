using PaymentGatewaySolution.Core.Domain.Models;

namespace PaymentGatewaySolution.Core.Domain.RepositoryContracts.PaymentDetailsContracts
{
    public interface IAddPaymentDetailsRepository
    {
        /// <summary>
        /// Method to add payment
        /// </summary>
        /// <param name="payment">Payment object which should be added</param>
        /// <returns>Returns the payment object with newly generated paymentID</returns>
        Task<Payment> AddPayment(Payment payment);
    }
}
