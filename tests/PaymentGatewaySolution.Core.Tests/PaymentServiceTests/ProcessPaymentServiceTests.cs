namespace PaymentGatewaySolution.Core.Tests.PaymentServiceTests
{
    public class ProcessPaymentServiceTests
    {
        #region "Private Readonly Fields"
        private readonly IFixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IProcessPaymentService _processPaymentService;
        private readonly IAddPaymentDetailsRepository _addPaymentDetailsRepository;
        private readonly IBankSimulator _bankSimulator;
        private readonly Mock<IAddPaymentDetailsRepository> _addPaymentDetailsRepositoryMock;
        private readonly Mock<IBankSimulator> _bankSimulatorMock;
        private readonly ILogger<ProcessPaymentService> _logger;
        #endregion


        #region "Constructor"
        public ProcessPaymentServiceTests(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;
            _addPaymentDetailsRepositoryMock = new Mock<IAddPaymentDetailsRepository>();
            _addPaymentDetailsRepository = _addPaymentDetailsRepositoryMock.Object;
            _bankSimulatorMock = new Mock<IBankSimulator>();
            _bankSimulator = _bankSimulatorMock.Object;
            _logger = Mock.Of<ILogger<ProcessPaymentService>>();
            _processPaymentService = new ProcessPaymentService(_addPaymentDetailsRepository, _bankSimulator, _logger);
        }
        #endregion

        #region "TestCases"

        [Fact]
        public async Task ProcessPaymentAsync_WithValidPaymentRequest_ReturnsSuccessfulPaymentResponse()
        {
            // Arrange
            var paymentRequest = _fixture.Create<AddPaymentRequestDTO>();
            var bankResponse = new BankResponse
            {
                Successful = true,
                Message = "Payment processed successfully"
            };
            _bankSimulatorMock.Setup(x => x.ProcessPaymentAsync(paymentRequest)).ReturnsAsync(bankResponse);

            Payment payment = paymentRequest.ToPayment();
            payment.TransactionID = Guid.NewGuid();
            payment.Successful = bankResponse.Successful;
            payment.Message = bankResponse.Message;


            _addPaymentDetailsRepositoryMock.Setup(temp => temp.AddPayment(It.IsAny<Payment>())).ReturnsAsync(payment);

            //Input
            _testOutputHelper.WriteLine("Input:");
            _testOutputHelper.WriteLine(paymentRequest.ToString());

            //Expected Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Expected Output:");
            _testOutputHelper.WriteLine(bankResponse.ToString());

            // Act
            var result = await _processPaymentService.ProcessPayment(paymentRequest);

            //Actual Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Actual Output:");
            _testOutputHelper.WriteLine(result.ToString());

            // Assert
            result.Should().NotBeNull();
            result.Successful.Should().BeTrue();
            result.Message.Should().Be(bankResponse.Message);
            _addPaymentDetailsRepositoryMock.Verify(x => x.AddPayment(It.IsAny<Payment>()), Times.Once);
        }

        [Fact]
        public async Task ProcessPaymentAsync_WithInvalidPaymentRequest_ReturnsInvalidRequestPaymentResponse()
        {
            // Arrange
            var paymentRequest = _fixture.Build<AddPaymentRequestDTO>()
                .With(x => x.Currency, string.Empty)
                .Create();

            //Input
            _testOutputHelper.WriteLine("Input:");
            _testOutputHelper.WriteLine(paymentRequest.ToString());

            //Expected Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Expected Output:");
            _testOutputHelper.WriteLine("Throw Argument Exception Error");

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _processPaymentService.ProcessPayment(paymentRequest);
            });
        }

        [Fact]
        public async Task ProcessPaymentAsync_WithBankProcessingError_ReturnsUnSuccessfulPaymentResponse()
        {
            // Arrange
            var paymentRequest = _fixture.Create<AddPaymentRequestDTO>();
            var bankResponse = new BankResponse
            {
                Successful = false,
                Message = "Insufficient funds"
            };
            _bankSimulatorMock.Setup(x => x.ProcessPaymentAsync(paymentRequest)).ReturnsAsync(bankResponse);

            Payment payment = paymentRequest.ToPayment();
            payment.TransactionID = Guid.NewGuid();
            payment.Successful = bankResponse.Successful;
            payment.Message = bankResponse.Message;
            _addPaymentDetailsRepositoryMock.Setup(temp => temp.AddPayment(It.IsAny<Payment>())).ReturnsAsync(payment);

            //Input
            _testOutputHelper.WriteLine("Input:");
            _testOutputHelper.WriteLine(paymentRequest.ToString());

            //Expected Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Expected Output:");
            _testOutputHelper.WriteLine(bankResponse.ToString());

            // Act
            var result = await _processPaymentService.ProcessPayment(paymentRequest);

            //Actual Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Actual Output:");
            _testOutputHelper.WriteLine(result.ToString());

            // Assert
            result.Should().NotBeNull();
            result.Successful.Should().BeFalse();
            result.Message.Should().Be(bankResponse.Message);
            _addPaymentDetailsRepositoryMock.Verify(x => x.AddPayment(It.IsAny<Payment>()), Times.Once);
        }


        #endregion
    }
}
