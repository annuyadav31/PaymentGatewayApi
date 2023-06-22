using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGateway.Core.ServiceContracts;
using PaymentGatewaySolution.Api.Controllers.CustomControllerBases;

namespace PaymentGatewaySolution.Api.Controllers
{
    /// <summary>
    /// Payment Controller to Process Payments and Get Payment Details
    /// </summary>
    public class PaymentController : CustomControllerBase
    {
        #region "Readonly fields"
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        #endregion

        /// <summary>
        /// Constructor for Payment Controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="paymentService"></param>
        #region "Constructor"
        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;

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

                PaymentResponse payment = await _paymentService.ProcessPayment(paymentRequest);

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


        #endregion
    }
}
