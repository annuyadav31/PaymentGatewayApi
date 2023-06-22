using PaymentGateway.Core.Domain.BankSimulatorContracts;
using PaymentGateway.Core.ModelDTO.BankDTO;
using PaymentGateway.Core.ModelDTO.PaymentDTO;

namespace PaymentGateway.Infrastructure.Repositories
{
    public class BankSimulator : IBankSimulator
    {
        public Task<BankResponse> ProcessPaymentAsync(AddPaymentRequestDTO paymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
