using AutoFixture;
using Moq;
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

        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly Mock<DbSet<Payment>> _mockPaymentsDbSet;
        private readonly Mock<DbSet<CardDetails>> _mockCardDetailsDbSet;
        private readonly AddPaymentDetailsRepository _repository;
        private readonly Fixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public AddPaymentRepositoryTests(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _mockPaymentsDbSet = CreateMockDbSet<Payment>();
            _mockCardDetailsDbSet = CreateMockDbSet<CardDetails>();
            _mockDbContext = CreateMockDbContext();
            _repository = new AddPaymentDetailsRepository(_mockDbContext.Object);
            _testOutputHelper = testOutputHelper;
        }

        private Mock<DbSet<TEntity>> CreateMockDbSet<TEntity>() where TEntity : class
        {
            var data = new List<TEntity>();
            var mockDbSet = new Mock<DbSet<TEntity>>();
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockDbSet.Setup(m => m.Add(It.IsAny<TEntity>())).Callback<TEntity>(item => data.Add(item));
            mockDbSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<TEntity>>())).Callback<IEnumerable<TEntity>>(items => data.AddRange(items));

            return mockDbSet;
        }

        private Mock<ApplicationDbContext> CreateMockDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PaymentGateway")
                .Options;

            var mockDbContext = new Mock<ApplicationDbContext>(options);
            mockDbContext.Setup(db => db.Set<Payment>()).Returns(_mockPaymentsDbSet.Object);
            mockDbContext.Setup(db => db.Set<CardDetails>()).Returns(_mockCardDetailsDbSet.Object);

            // Add necessary setup or configuration for the mock

            return mockDbContext;
        }

        [Fact]
        public async Task AddPayment_ShouldAddPaymentToDatabase()
        {
            // Arrange
            var payment = _fixture.Create<Payment>();

            //Input
            _testOutputHelper.WriteLine("Input:");
            _testOutputHelper.WriteLine(payment.ToString());


            // Act
            var result = await _repository.AddPayment(payment);
            //Actual Result to ITestOutputHelper
            _testOutputHelper.WriteLine("Actual Output:");
            _testOutputHelper.WriteLine(result.ToString());

            // Assert
            _mockDbContext.Verify(db => db.Payments.Add(payment), Times.Once);
            _mockDbContext.Verify(db => db.CardDetails.Add(It.IsAny<CardDetails>()), Times.Never);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
            result.Should().Be(payment);
        }

        [Fact]
        public async Task AddPayment_WithCardDetails_ShouldAddCardDetailsToDatabase()
        {
            // Arrange
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardDetails, _fixture.Create<CardDetails>())
                .Create();

            // Act
            var result = await _repository.AddPayment(payment);

            // Assert
            _mockDbContext.Verify(db => db.Payments.Add(payment), Times.Once);
            _mockDbContext.Verify(db => db.CardDetails.Add(payment.CardDetails), Times.Once);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
            result.Should().Be(payment);
        }

        [Fact]
        public async Task AddPayment_WithNullPayment_ShouldNotAddToDatabase()
        {
            // Act
            var result = await _repository.AddPayment(null);

            // Assert
            _mockDbContext.Verify(db => db.Payments.Add(It.IsAny<Payment>()), Times.Never);
            _mockDbContext.Verify(db => db.CardDetails.Add(It.IsAny<CardDetails>()), Times.Never);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Never);
            result.Should().BeNull();
        }
    }

}
