using PaymentGateway.Core.ModelDTO.PaymentDTO;

namespace PaymentGateway.Core.ServiceContracts
{
    public interface IPaymentService
    {
        /// <summary>
        /// Interface for process payment Service
        /// </summary>
        /// <param name="paymentRequest">Details of the payment which has to be processed</param>
        /// <returns>Returns the payment Response object indicating whether the payment is successful or not</returns>
        Task<PaymentResponse> ProcessPayment(AddPaymentRequestDTO paymentRequest);

        /// <summary>
        /// Interface for get payment deatils
        /// </summary>
        /// <param name="transactionId">Parameter which is used to uniquely identify the payment</param>
        /// <returns>Returns the matching payment Response object</returns>
        Task<PaymentResponse> GetPaymentDetails(Guid transactionId);
    }
}
