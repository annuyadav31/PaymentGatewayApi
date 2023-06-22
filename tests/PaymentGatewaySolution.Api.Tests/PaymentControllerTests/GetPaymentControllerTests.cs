using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentGateway.Core.ModelDTO.PaymentDTO;
using PaymentGatewaySolution.Api.Controllers.PaymentController;
using PaymentGatewaySolution.Core.ServiceContracts.IPaymentService;
using Xunit.Abstractions;
using FluentAssertions;

namespace PaymentGatewaySolution.Api.Tests.PaymentControllerTests
{
    public class GetPaymentControllerTests
    {
        #region "ReadOnly Variables"
        private readonly Fixture _fixture;
        private readonly GetPaymentDetailsController _getPaymentController;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IGetPaymentDetailsService _getPaymentService;
        private readonly Mock<IGetPaymentDetailsService> _getPaymentServiceMock;
        private readonly Mock<ILogger<GetPaymentDetailsController>> _loggerMock;

        #endregion
        public GetPaymentControllerTests(ITestOutputHelper testOutputHelper)
        {
            _getPaymentServiceMock = new Mock<IGetPaymentDetailsService>();
            _getPaymentService = _getPaymentServiceMock.Object;
            _loggerMock = new Mock<ILogger<GetPaymentDetailsController>>();
            _getPaymentController = new GetPaymentDetailsController(_loggerMock.Object, _getPaymentService);
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task GetPaymentDetails_WithValidTransactionId_ReturnsOk()
        {
            var transactionId = _fixture.Create<Guid>();
            var paymentDetails = _fixture.Create<PaymentResponse>();
            _getPaymentServiceMock.Setup(x => x.GetPaymentDetails(transactionId)).ReturnsAsync(paymentDetails);

            // Act
            var result = await _getPaymentController.GetPaymentDetails(transactionId.ToString());

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.Equal(paymentDetails, okResult.Value);
        }

        [Fact]
        public async Task GetPaymentDetails_WithInvalidTransactionId_ReturnsNull()
        {
            // Arrange
            Guid paymentId = Guid.NewGuid();

            // Act
            var result = await _getPaymentController.GetPaymentDetails(paymentId.ToString());

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            okResult.Value.Should().BeNull();
        }
    }
}
