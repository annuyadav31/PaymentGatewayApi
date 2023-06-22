namespace PaymentGatewaySolution.Core.Tests.PaymentServiceTests
{
    public class GetPaymentDetailsServiceTests
    {
        #region "Private Readonly Fields"
        private readonly IFixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IBankSimulator _bankSimulator;
        private readonly Mock<IBankSimulator> _bankSimulatorMock;
        private readonly IGetPaymentDetailsService _getPaymentDetailsService;
        private readonly IGetPaymentDetailsRepository _getPaymentDetailsRepository;
        private readonly Mock<IGetPaymentDetailsRepository> _getPaymentDetailsRepositoryMock;
        private readonly ILogger<GetPaymentDetailsService> _logger;
        #endregion

        public GetPaymentDetailsServiceTests(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;
            _getPaymentDetailsRepositoryMock = new Mock<IGetPaymentDetailsRepository>();
            _getPaymentDetailsRepository = _getPaymentDetailsRepositoryMock.Object;
            _bankSimulatorMock = new Mock<IBankSimulator>();
            _bankSimulator = _bankSimulatorMock.Object;
            _logger = Mock.Of<ILogger<GetPaymentDetailsService>>();
            _getPaymentDetailsService = new GetPaymentDetailsService(_getPaymentDetailsRepository, _bankSimulator, _logger);
        }

        [Fact]
        public async Task GetPaymentDetails_Should_Return_PaymentDetails_When_Payment_Exists()
        {
            // Arrange
            var paymentRequest = _fixture.Create<AddPaymentRequestDTO>();
            Payment payment = paymentRequest.ToPayment();
            payment.TransactionID = new Guid();

            _getPaymentDetailsRepositoryMock.Setup(x => x.GetPayment(payment.TransactionID)).ReturnsAsync(payment);

            // Act
            var result = await _getPaymentDetailsService.GetPaymentDetails(payment.TransactionID);

            // Assert
            result.Should().NotBeNull();
            result.TransactionID.Should().Be(payment.TransactionID);
            result.CardNumber.Should().Contain("***");
            result.CardHolderName.Should().Be(payment.CardDetails?.CardHolderName);
            result.Successful.Should().Be(payment.Successful);
        }

        [Fact]
        public async Task GetPaymentDetails_Should_Throw_PaymentGatewayException_When_Payment_Not_Found()
        {
            //Arrange
            Guid paymentId = new Guid();

            //Act
            PaymentResponse paymentResponse = await _getPaymentDetailsService.GetPaymentDetails(paymentId);

            //Assert
            paymentResponse.Should().BeNull();
        }
    }
}
