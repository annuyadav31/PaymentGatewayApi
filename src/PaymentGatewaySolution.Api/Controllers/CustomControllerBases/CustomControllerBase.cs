using Microsoft.AspNetCore.Mvc;

namespace PaymentGatewaySolution.Api.Controllers.CustomControllerBases
{
    /// <summary>
    /// Controller Base for common Information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController] 
    public class CustomControllerBase : ControllerBase
    {
    }
}
