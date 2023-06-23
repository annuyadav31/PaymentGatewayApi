using AutoFixture;
using PaymentGateway.Infrastructure.Repositories;
using PaymentGatewaySolution.Core.Domain.Models;
using PaymentGatewaySolution.Infrastructure.Context;
using FluentAssertions;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;
using PaymentGatewaySolution.Infrastructure.Repositories;

namespace PaymentGatewaySolution.Infrastructure.Tests.PaymentRepositoryTests
{
    public class GetPaymentDetailsRepositoryTests
    {
        private readonly Fixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public GetPaymentDetailsRepositoryTests(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;
        }

        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [Fact]
        public async void GetPaymentDetails_TransactionIdFound()
        {

            // Arrange
            var payment = _fixture.Create<Payment>();
            var dbContext = await GetDatabaseContext();
            var Repository = new AddPaymentDetailsRepository(dbContext);
            var Repository2 = new GetPaymentDetailsRepository(dbContext);

            //Input
            _testOutputHelper.WriteLine("Expected Output:");
            _testOutputHelper.WriteLine(payment.ToString());

            // Act
            var addedPayment = await Repository.AddPayment(payment);
            var paymentDetails = await Repository2.GetPayment(payment.TransactionID);

            //Actual Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Actual Output:");
            _testOutputHelper.WriteLine(paymentDetails.ToString());

            // Assert
            paymentDetails.Should().NotBeNull();
            paymentDetails.Should().BeOfType<Payment>();
            paymentDetails.TransactionID.Should().Be(payment.TransactionID);
        }


        [Fact]
        public async void GetPaymentDetails_TransactionIdNotFound()
        {

            // Arrange
            Guid transactionId = Guid.NewGuid();
            var dbContext = await GetDatabaseContext();
            var Repository2 = new GetPaymentDetailsRepository(dbContext);

            //Input
            _testOutputHelper.WriteLine("Input:");
            _testOutputHelper.WriteLine(transactionId.ToString());

            // Act
            var paymentDetails = await Repository2.GetPayment(transactionId);

            // Assert
            paymentDetails.Should().BeNull();
        }
    }
}
