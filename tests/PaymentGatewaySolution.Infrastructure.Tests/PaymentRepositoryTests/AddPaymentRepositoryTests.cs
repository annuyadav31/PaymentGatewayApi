using AutoFixture;
using PaymentGateway.Infrastructure.Repositories;
using PaymentGatewaySolution.Core.Domain.Models;
using PaymentGatewaySolution.Infrastructure.Context;
using FluentAssertions;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace PaymentGatewaySolution.Infrastructure.Tests.PaymentRepositoryTests
{
    public class AddPaymentRepositoryTests
    {
        private readonly Fixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public AddPaymentRepositoryTests(ITestOutputHelper testOutputHelper)
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
            if (await databaseContext.Payments.CountAsync() <= 0)
            {
              
                for (int i = 1; i <= 10; i++)
                {
                    var payment = _fixture.Create<Payment>();
                    databaseContext.Payments.Add(payment);
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void AddPayment_ShouldAddPaymentToDatabase()
        {

            // Arrange
            var payment = _fixture.Create<Payment>();
            var dbContext = await GetDatabaseContext();
            var Repository = new AddPaymentDetailsRepository(dbContext);

            //Input
            _testOutputHelper.WriteLine("Expected Output:");
            _testOutputHelper.WriteLine(payment.ToString());

            // Act
            var result = await Repository.AddPayment(payment);

            //Actual Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Actual Output:");
            _testOutputHelper.WriteLine(result.ToString());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Payment>();
            result.TransactionID.Should().Be(payment.TransactionID);
        }


        [Fact]
        public async void AddPayment_WithCardDetails_ShouldAddCardDetailsToDatabase()
        {

            // Arrange
            var payment = _fixture.Build<Payment>().With(p => p.CardDetails, _fixture.Create<CardDetails>()).Create();
            var dbContext = await GetDatabaseContext();
            var Repository = new AddPaymentDetailsRepository(dbContext);

            //Input
            _testOutputHelper.WriteLine("Expected Output:");
            _testOutputHelper.WriteLine(payment.ToString());

            // Act
            var result = await Repository.AddPayment(payment);

            //Actual Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Actual Output:");
            _testOutputHelper.WriteLine(result.ToString());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Payment>();
            result.CardDetails?.CardNumber.Should().Be(payment.CardDetails?.CardNumber);
        }

        [Fact]
        public async Task AddPayment_WithNullPayment_ShouldNotAddToDatabase()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var Repository = new AddPaymentDetailsRepository(dbContext);

            // Act
            var result = await Repository.AddPayment(null);

            // Assert
            result.Should().BeNull();
        }
    }

}
