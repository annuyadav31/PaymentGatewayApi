using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.ModelDTO.BankDTO
{
    /// <summary>
    /// Model DTO for Bank Response returned by Bank Simulator
    /// </summary>
    public class BankResponse
    {
        public bool Successful { get; set; }
        public string? Message { get; set; }
    }
}
