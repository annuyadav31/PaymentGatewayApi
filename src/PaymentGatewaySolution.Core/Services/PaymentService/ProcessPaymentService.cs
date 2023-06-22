using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Core.Domain.BankSimulatorContracts;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Core.Domain.RepositoryContracts.PaymentDetailsContracts;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;

namespace PaymentGatewaySolution.Core.Services.PaymentService
{
    public class ProcessPaymentService : IProcessPaymentService
    {
        #region "Private Readonly Fields"
        private readonly IAddPaymentDetailsRepository _addPaymentDetailsRepository;
        private readonly IBankSimulator _bankSimulator;
        private readonly Mock<IBankSimulator> _bankSimulatorMock;
        private readonly ILogger<ProcessPaymentService> _logger;
        #endregion

        #region "Constructor"
        public ProcessPaymentService(IAddPaymentDetailsRepository addPaymentDetailsRepository, IBankSimulator bankSimulator, ILogger<ProcessPaymentService> logger)
        {
            _addPaymentDetailsRepository = addPaymentDetailsRepository;
            _bankSimulator = bankSimulator;
            _logger = logger;
            _bankSimulatorMock = new Mock<IBankSimulator>();
            _bankSimulator = _bankSimulatorMock.Object;
        }
        #endregion
        public Task<PaymentResponse> ProcessPayment(AddPaymentRequestDTO paymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
