using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Api.Controllers.CustomControllerBases;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;

namespace PaymentGatewaySolution.Api.Controllers
{
    /// <summary>
    /// Payment Controller to Process Payments and Get Payment Details
    /// </summary>
    public class PaymentController : CustomControllerBase
    {
        #region "Readonly fields"
        private readonly IProcessPaymentService _processPaymentService;
        private readonly IGetPaymentDetailsService _getPaymentDetailsService;
        private readonly ILogger<PaymentController> _logger;
        #endregion


        /// <summary>
        /// Constructor for Payment Controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="processPaymentService"></param>
        /// <param name="getPaymentDetailsService"></param>
        #region "Constructor"
        public PaymentController(ILogger<PaymentController> logger, IProcessPaymentService processPaymentService , IGetPaymentDetailsService getPaymentDetailsService)
        {
            _logger = logger;
            _processPaymentService = processPaymentService;
            _getPaymentDetailsService = getPaymentDetailsService;

        }
        #endregion


        #region "EndPoints"

        #region "Process Payments"

        /// <summary>
        /// Endpoint to process payment
        /// </summary>
        /// <param name="paymentRequest">Object which should be passed to the endpoint</param>
        /// <returns>Returns the payment Response indicating whether it is successful or not with a newly generated transactionId</returns>
        [HttpPost]
        public async Task<ActionResult<PaymentResponse>> ProcessPayment([FromBody] AddPaymentRequestDTO paymentRequest)
        {
            try
            {
                // Validate the payment request
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                PaymentResponse payment = await _processPaymentService.ProcessPayment(paymentRequest);

                if (payment == null)
                {
                    return BadRequest(paymentRequest);
                }

                return Ok(payment);
            }
            catch (PaymentGatewayException ex)
            {
                _logger.LogError(ex, $"Error processing payment: {ex.Message}");

                return BadRequest(new PaymentResponse
                {
                    Successful = false,
                    Message = ex.Message
                });
            }
        }
        #endregion

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
