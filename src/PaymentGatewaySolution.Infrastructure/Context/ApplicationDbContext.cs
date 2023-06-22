using Microsoft.EntityFrameworkCore;
using PaymentGatewaySolution.Core.Domain.Models;

namespace PaymentGatewaySolution.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Payment>? Payments { get; set; }
        public DbSet<CardDetails>? CardDetails { get; set; }

        /// <summary>
        /// Constructor for Application DB Context
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
