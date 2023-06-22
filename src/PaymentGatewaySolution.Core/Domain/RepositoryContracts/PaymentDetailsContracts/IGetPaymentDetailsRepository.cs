using PaymentGatewaySolution.Core.Domain.Models;

namespace PaymentGatewaySolution.Core.Domain.RepositoryContracts.PaymentDetailsContracts
{
    public interface IGetPaymentDetailsRepository
    {
        /// <summary>
        /// Method to retrieve the payment details based on paymentId
        /// </summary>
        /// <param name="paymentId">paymentId which will be used to retrieve the details </param>
        /// <returns>Returns the matching payment details</returns>
        Task<Payment> GetPayment(Guid paymentId);
    }
}
