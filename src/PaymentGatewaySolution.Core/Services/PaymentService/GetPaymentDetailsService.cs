using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;

namespace PaymentGatewaySolution.Core.Services.PaymentService
{
    public class GetPaymentDetailsService : IGetPaymentDetailsService
    {
        public Task<PaymentResponse> GetPaymentDetails(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
