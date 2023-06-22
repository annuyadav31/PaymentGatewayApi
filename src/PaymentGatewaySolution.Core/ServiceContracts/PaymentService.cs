using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGateway.Core.ServiceContracts;

namespace PaymentGatewaySolution.Core.ServiceContracts
{
    public class PaymentService : IPaymentService
    {
        public Task<PaymentResponse> GetPaymentDetails(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> ProcessPayment(AddPaymentRequestDTO paymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
