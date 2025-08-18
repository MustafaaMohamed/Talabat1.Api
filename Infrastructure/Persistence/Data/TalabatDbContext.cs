using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
	public class TalabatDbContext : DbContext
	{
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
