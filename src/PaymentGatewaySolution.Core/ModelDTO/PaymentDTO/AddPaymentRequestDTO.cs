using PaymentGateway.Core.ModelDTO.CardDTO;
using PaymentGatewaySolution.Core.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Core.ModelDTO.PaymentDTO
{
    /// <summary>
    /// DTO Model which is used to add payment details
    /// </summary>
    public class AddPaymentRequestDTO
    {
        [Required(ErrorMessage ="Please provide card details")]
        public AddCardRequestDTO? CardDetails { get; set; }


        [Range(0.1, double.MaxValue)]
        [Required(ErrorMessage ="Please provide amount which has to be deducted")]
        public double Amount { get; set; }

        [StringLength(3)]
        [Required(ErrorMessage ="Please provide the correct Currency Code.")]
        public string? Currency { get; set; }

        /// <summary>
        /// Method to convert AddPaymentRequest Model DTO to Payment Entity Model 
        /// </summary>
        /// <returns>Returns the converted Payment model</returns>
        public Payment ToPayment()
        {
            return new Payment
            {
                Amount = Amount,
                Currency = Currency,
                CardDetails = CardDetails?.ToCardDetails()
            };
        }
    }
}
