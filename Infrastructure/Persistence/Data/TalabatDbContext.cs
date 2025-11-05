using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
	public class TalabatDbContext : DbContext
	{
		public DbSet<Order> Orders{ get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }
		public TalabatDbContext(DbContextOptions<TalabatDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(TalabatDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}


	}
}
