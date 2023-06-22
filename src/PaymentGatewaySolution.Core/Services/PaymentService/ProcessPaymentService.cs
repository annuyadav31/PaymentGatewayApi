using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Core.Domain.BankSimulatorContracts;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.Helpers;
using PaymentGateway.Core.ModelDTO.BankDTO;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Core.Domain.Models;
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
        public async Task<PaymentResponse> ProcessPayment(AddPaymentRequestDTO paymentRequest)
        {
            try
            {
                if (paymentRequest == null)
                {
                    _logger.LogWarning("Invalid payment request");
                    throw new ArgumentNullException(nameof(paymentRequest));
                }

                //Model Validations
                ValidationHelper.ModelValidation(paymentRequest);

                // Send the payment request to the bank simulator
                var bankResponse1 = new BankResponse
                {
                    Successful = true,
                    Message = "Successful Payment"
                };
                _bankSimulatorMock.Setup(x => x.ProcessPaymentAsync(paymentRequest)).ReturnsAsync(bankResponse1);
                BankResponse bankResponse = await _bankSimulator.ProcessPaymentAsync(paymentRequest);

                //Convert the AddPaymentRequest to payment
                Payment payment = paymentRequest.ToPayment();
                payment.Successful = bankResponse.Successful;
                payment.Message = bankResponse.Message;
                payment.CreatedOn = DateTime.UtcNow;

                Payment paymentResponse = await _addPaymentDetailsRepository.AddPayment(payment);

                return paymentResponse.ToPaymentResponse();
            }
            catch (PaymentGatewayException ex)
            {
                _logger.LogError(ex, "Error processing payment");
                throw new PaymentGatewayException("Error processing payment");
            }
        }
    }
}
