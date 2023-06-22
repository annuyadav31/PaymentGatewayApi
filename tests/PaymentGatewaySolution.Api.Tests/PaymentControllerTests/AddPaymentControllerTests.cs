using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Api.Controllers.PaymentController;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace PaymentGatewaySolution.Api.Tests.PaymentControllerTests
{
    public class AddPaymentControllerTests
    {
        #region "ReadOnly Variables"
        private readonly Fixture _fixture;
        private readonly AddPaymentController _addPaymentController;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IProcessPaymentService _processPaymentService;
        private readonly Mock<IProcessPaymentService> _processPaymentServiceMock;
        private readonly Mock<ILogger<AddPaymentController>> _loggerMock;

        #endregion
        public AddPaymentControllerTests(ITestOutputHelper testOutputHelper)
        {
            _processPaymentServiceMock = new Mock<IProcessPaymentService>();
            _processPaymentService = _processPaymentServiceMock.Object;
            _loggerMock = new Mock<ILogger<AddPaymentController>>();
            _addPaymentController = new AddPaymentController(_loggerMock.Object, _processPaymentService);
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public async Task ProcessPayment_ValidRequest_ReturnsOk()
        {
            // Arrange
            var paymentRequest = _fixture.Create<AddPaymentRequestDTO>();
            var paymentResponse = _fixture.Create<PaymentResponse>();
            _processPaymentServiceMock.Setup(x => x.ProcessPayment(paymentRequest)).ReturnsAsync(paymentResponse);

            // Act
            var result = await _addPaymentController.ProcessPayment(paymentRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.Equal(paymentResponse, okResult.Value);
        }

        [Fact]
        public async Task ProcessPayment_WithInvalidPaymentRequest_ReturnsBadRequest()
        {
            // Arrange
            AddPaymentRequestDTO paymentRequest = _fixture.Create<AddPaymentRequestDTO>();
            paymentRequest.CardDetails.CardHolderName = null;

            // Act
            var result = await _addPaymentController.ProcessPayment(paymentRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
