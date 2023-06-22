using PaymentGateway.Core.ModelDTO.BankDTO;
using PaymentGateway.Core.ModelDTO.PaymentDTO;

namespace PaymentGateway.Core.Domain.BankSimulatorContracts
{
    public interface IBankSimulator
    {
        /// <summary>
        /// Interface for Bank Simulator to process payments
        /// </summary>
        /// <param name="paymentRequest">Object to pass as request while processing payments</param>
        /// <returns>Returns bank response object with successful or not with an message</returns>
        Task<BankResponse> ProcessPaymentAsync(AddPaymentRequestDTO paymentRequest);
    }
}
