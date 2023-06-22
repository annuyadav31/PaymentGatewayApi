using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Core.Domain.BankSimulatorContracts;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Core.Domain.RepositoryContracts.PaymentDetailsContracts;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;

namespace PaymentGatewaySolution.Core.Services.PaymentService
{
    public class GetPaymentDetailsService : IGetPaymentDetailsService
    {
        #region "Private Readonly Fields"
        private readonly IGetPaymentDetailsRepository _getPaymentDetailsRepository;
        private readonly IBankSimulator _bankSimulator;
        private readonly Mock<IBankSimulator> _bankSimulatorMock;
        private readonly ILogger<GetPaymentDetailsService> _logger;
        #endregion

        public GetPaymentDetailsService(IGetPaymentDetailsRepository getPaymentDetailsRepository, IBankSimulator bankSimulator, ILogger<GetPaymentDetailsService> logger)
        {
            _getPaymentDetailsRepository = getPaymentDetailsRepository;
            _bankSimulator = bankSimulator;
            _logger = logger;
            _bankSimulatorMock = new Mock<IBankSimulator>();
            _bankSimulator = _bankSimulatorMock.Object;
        }
        public Task<PaymentResponse> GetPaymentDetails(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
