using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentGatewaySolution.Core.Domain.Models
{
    /// <summary>
    /// Card Details Domain Model Class
    /// </summary>
    /// 

    [Table("CardDetails")]
    public class CardDetails
    {
        [Key]
        public Guid CardID { get; set; }

        [StringLength(19)]
        public string? CardNumber { get; set; }

        [StringLength(100)]
        public string? CardHolderName { get; set; }

        [Range(1, 12)]
        public int ExpiryMonth { get; set; }

        [Range(2022, 2100)]
        public int ExpiryYear { get; set; }

        [Range(3, 4)]
        public int Cvv { get; set; }
    }
}
