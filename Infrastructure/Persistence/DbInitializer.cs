using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Text.Json;

namespace Persistence
{
	public class DbInitializer : IDbInitializer
	{
		private readonly TalabatDbContext _context;

		public DbInitializer(TalabatDbContext context)
		{
			_context = context;
		}
		public async Task InitializeAsync()
		{
			try 
			{
				// Check if database doesnt exist create it and apply any pending migration
				if (_context.Database.GetPendingMigrations().Any())
				{
					await _context.Database.MigrateAsync();
				}
				// Read data from external source , deserialize , add into db 

				// Seeding ProductTypes from json file 
				if (!_context.ProductTypes.Any())
				{
					var typesJson = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesJson);
					if (types is not null && types.Any())
					{
						await _context.ProductTypes.AddRangeAsync(types);
						await _context.SaveChangesAsync();
					}

				}
				// Seeding ProductBrands from json file 
				if (!_context.ProductBrands.Any())
				{
					var brandsJson = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
					if (brands is not null && brands.Any())
					{
						await _context.ProductBrands.AddRangeAsync(brands);
						await _context.SaveChangesAsync();
					}

				}
				// Seeding Product from json file 
				if (!_context.Products.Any())
				{
					var productsJson = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productsJson);
					if (products is not null && products.Any())
					{
						await _context.Products.AddRangeAsync(products);
						await _context.SaveChangesAsync();
					}

				}
			}
			catch(Exception ) { throw; }

		}
	}
}
