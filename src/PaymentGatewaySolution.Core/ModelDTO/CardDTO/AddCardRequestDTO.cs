using PaymentGatewaySolution.Core.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Core.ModelDTO.CardDTO
{
    /// <summary>
    /// DTO model which is used to add card details
    /// </summary>
    public class AddCardRequestDTO
    {
        [StringLength(19)]
        [Required]
        public string? CardNumber { get; set; }

        [StringLength(100)]
        [Required]
        public string? CardHolderName { get; set; }

        [Range(1, 12)]
        [Required]
        public int ExpiryMonth { get; set; }

        [Range(2022, 2100)]
        [Required]
        public int ExpiryYear { get; set; }

        [Range(3, 4)]
        [Required]
        public int Cvv { get; set; }

        /// <summary>
        /// Function to convert AddCardDetailsDTO Model to CardDetails Object
        /// </summary>
        /// <returns>Returns the converted cardDetails Object</returns>
        public CardDetails ToCardDetails()
        {
            return new CardDetails
            {
                CardNumber = CardNumber,
                CardHolderName = CardHolderName,
                ExpiryMonth = ExpiryMonth,
                ExpiryYear = ExpiryYear,
                Cvv = Cvv
            };
        }
    }
}
