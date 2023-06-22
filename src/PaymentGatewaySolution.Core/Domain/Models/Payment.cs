using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentGatewaySolution.Core.Domain.Models
{
    /// <summary>
    /// Payment Domain Model Class
    /// </summary>
    /// 

    [Table("Payments")]
    public class Payment
    {
        //Primary Key
        [Key]
        public Guid TransactionID { get; set; }

        [Range(0, double.MaxValue)]
        public double Amount { get; set; }

        [StringLength(3)]
        public string? Currency { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Successful { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }

        //Foreign Key
        public Guid CardID { get; set; }

        [ForeignKey(nameof(CardID))]
        public CardDetails? CardDetails { get; set; }

    }
}
