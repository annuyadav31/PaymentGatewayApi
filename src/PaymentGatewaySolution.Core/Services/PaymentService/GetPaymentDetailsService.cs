using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Core.Domain.BankSimulatorContracts;
using PaymentGateway.Core.Exceptions;
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

        /// <summary>
        /// Method to get payment deatils based on transactionId
        /// </summary>
        /// <param name="transactionId">Parameter which is used to uniquely identify the transactionId</param>
        /// <returns>Returns the matched payment details</returns>
        /// <exception cref="PaymentGatewayException"></exception>
        public async Task<PaymentResponse> GetPaymentDetails(Guid transactionId)
        {
            try
            {
                // Retrieve payment from the repository
                var payment = await _getPaymentDetailsRepository.GetPayment(transactionId);
                if (payment == null)
                {
                    return null;
                }
                return payment.ToPaymentResponse();
            }
            catch (Exception ex)
            {
                throw new PaymentGatewayException("Error retrieving payment details.", ex);
            }
        }
    }
}
