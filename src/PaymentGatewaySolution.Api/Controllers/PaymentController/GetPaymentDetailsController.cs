using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Api.Controllers.CustomControllerBases;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;

namespace PaymentGatewaySolution.Api.Controllers.PaymentController
{
    /// <summary>
    /// Payment Controller to Get Payment Details through TransactionId
    /// </summary>
    public class GetPaymentDetailsController : CustomControllerBase
    {
        #region "ReadOnly Fields"
        private readonly IGetPaymentDetailsService _getPaymentDetailsService;
        private readonly ILogger<GetPaymentDetailsController> _logger;
        #endregion

        /// <summary>
        /// Constructor for Get Payment Details Controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="getPaymentDetailsService"></param>
        public GetPaymentDetailsController(ILogger<GetPaymentDetailsController> logger, IGetPaymentDetailsService getPaymentDetailsService)
        {
            _logger = logger;
            _getPaymentDetailsService = getPaymentDetailsService;
        }

        #region "EndPoint"

        #region "GetPaymentDetails"

        /// <summary>
        /// Endpoint to get payment details based on transactionId
        /// </summary>
        /// <param name="transactionId">Parameter used to uniquely identify a transaction.</param>
        /// <returns>Returns matching payment details</returns>
        [HttpGet("{transactionId}")]
        public async Task<ActionResult<PaymentResponse>> GetPaymentDetails(string transactionId)
        {
            try
            {
                var payment = await _getPaymentDetailsService.GetPaymentDetails(Guid.Parse(transactionId));

                return Ok(payment);
            }
            catch (PaymentGatewayException ex)
            {
                _logger.LogError(ex, $"Error retrieving payment details: {ex.Message}");

                return BadRequest(new PaymentResponse
                {
                    TransactionID = Guid.Parse(transactionId),
                    Successful = false,
                    Message = ex.Message
                });
            }
        }
        #endregion
        #endregion

    }
}
