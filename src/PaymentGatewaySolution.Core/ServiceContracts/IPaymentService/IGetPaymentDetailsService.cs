using PaymentGateway.Core.ModelDTO.PaymentDTO;

namespace PaymentGatewaySolution.Core.ServiceContracts.IPaymentService
{
    public interface IGetPaymentDetailsService
    {

        /// <summary>
        /// Interface for get payment deatils
        /// </summary>
        /// <param name="transactionId">Parameter which is used to uniquely identify the payment</param>
        /// <returns>Returns the matching payment Response object</returns>
        Task<PaymentResponse> GetPaymentDetails(Guid transactionId);
    }
}
