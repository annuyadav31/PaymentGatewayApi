using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Api.Controllers.CustomControllerBases;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;

namespace PaymentGatewaySolution.Api.Controllers.PaymentController
{
    /// <summary>
    /// Payment Controller to Process Payments and Get Payment Details
    /// </summary>
    public class AddPaymentController : CustomControllerBase
    {
        #region "Readonly fields"
        private readonly IProcessPaymentService _processPaymentService;
        private readonly ILogger<AddPaymentController> _logger;
        #endregion


        /// <summary>
        /// Constructor for Add Payment Controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="processPaymentService"></param>
        #region "Constructor"
        public AddPaymentController(ILogger<AddPaymentController> logger, IProcessPaymentService processPaymentService)
        {
            _logger = logger;
            _processPaymentService = processPaymentService;

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

        #endregion
    }
}
