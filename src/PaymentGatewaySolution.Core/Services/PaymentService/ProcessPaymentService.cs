using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;

namespace PaymentGatewaySolution.Core.Services.PaymentService
{
    public class ProcessPaymentService : IProcessPaymentService
    {
        public Task<PaymentResponse> ProcessPayment(AddPaymentRequestDTO paymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
