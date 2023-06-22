using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Core.Helpers
{
    /// <summary>
    /// Helper class to validate models
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Method to validate the object model
        /// </summary>
        /// <param name="obj">Model to be validate</param>
        /// <exception cref="ArgumentException">Returns Argument Exception if model is not valid</exception>
        internal static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
