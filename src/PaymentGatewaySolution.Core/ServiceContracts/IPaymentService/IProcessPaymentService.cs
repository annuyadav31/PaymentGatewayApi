using PaymentGateway.Core.ModelDTO.PaymentDTO;

namespace PaymentGatewaySolution.Core.ServiceContracts.IPaymentService
{
    public interface IProcessPaymentService
    {
        /// <summary>
        /// Interface for process payment Service
        /// </summary>
        /// <param name="paymentRequest">Details of the payment which has to be processed</param>
        /// <returns>Returns the payment Response object indicating whether the payment is successful or not</returns>
        Task<PaymentResponse> ProcessPayment(AddPaymentRequestDTO paymentRequest);
    }
}
