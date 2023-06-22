using PaymentGatewaySolution.Core.Domain.Models;

namespace PaymentGateway.Core.ModelDTO.PaymentDTO
{
    /// <summary>
    /// Represents DTO model class that is used as a return type of most of the methods of payment service
    /// </summary>
    public class PaymentResponse
    {
        public Guid TransactionID { get; set; }
        public double Amount { get; set; }
        public string? Currency { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Successful { get; set; }
        public string? Message { get; set; }
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int Cvv { get; set; }

        /// <summary>
        /// Compares the given object with the paymentResponse object
        /// </summary>
        /// <param name="obj">Response object to compare</param>
        /// <returns>True or False indicating whether all payment details are matched with the specified paramter object</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            { return false; }

            if (obj.GetType() != typeof(PaymentResponse)) { return false; }

            PaymentResponse paymentResponse = (PaymentResponse)obj;
            return TransactionID == paymentResponse.TransactionID &&
                Amount == paymentResponse.Amount &&
                Currency == paymentResponse.Currency &&
                CreatedOn == paymentResponse.CreatedOn &&
                Successful == paymentResponse.Successful &&
                Message == paymentResponse.Message &&
                CardNumber == paymentResponse.CardNumber &&
                CardHolderName == paymentResponse.CardHolderName &&
                ExpiryMonth == paymentResponse.ExpiryMonth &&
                ExpiryYear == paymentResponse.ExpiryYear &&
                Cvv == paymentResponse.Cvv;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"TransactionID: {TransactionID}, Amount: {Amount}, Currency:{Currency} , Successful : {Successful}, Message:{Message}, CardNumber:{CardNumber},"+
                $"CardHolderName:{CardHolderName},ExpiryMonth:{ExpiryMonth}, ExpiryYear:{ExpiryYear}, Cvv:{Cvv}";
        }
    }

    public static class PaymentExtensions
    {
        /// <summary>
        /// An extension method to convert an object of Payment class to PaymentResponse class
        /// </summary>
        /// <param name="payment">The payment object to convert</param>
        /// <returns>Returns the converted paymentResponse class</returns>
        public static PaymentResponse ToPaymentResponse(this Payment payment)
        {
            return new PaymentResponse()
            {
                TransactionID = payment.TransactionID,
                Amount = payment.Amount,
                Currency = payment.Currency,
                CreatedOn = payment.CreatedOn,
                Successful = payment.Successful,
                Message = payment.Message,
                CardNumber = (payment.CardDetails != null) ? $"**** **** **** {payment.CardDetails.CardNumber?.Substring(payment.CardDetails.CardNumber.Length - 4)}" : null,
                CardHolderName = (payment.CardDetails != null) ? payment.CardDetails.CardHolderName : null,
                ExpiryMonth = (payment.CardDetails != null) ? payment.CardDetails.ExpiryMonth : 0,
                ExpiryYear = (payment.CardDetails != null) ? payment.CardDetails.ExpiryYear : 0,
                Cvv = (payment.CardDetails != null) ? payment.CardDetails.Cvv : 0
            };
        }
    }
}
