using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System.Text.Json;

namespace Persistence
{
	public class DbInitializer(
		TalabatDbContext context,
		TalabatIdentityDbContext identityDbContext,
		UserManager<AppUser> userManager,
		RoleManager<IdentityRole> roleManager) : IDbInitializer
	{
		public async Task InitializeAsync()
		{
			try 
			{
				// Check if database doesnt exist create it and apply any pending migration
				if (context.Database.GetPendingMigrations().Any())
				{
					await context.Database.MigrateAsync();
				}
				// Read data from external source , deserialize , add into db 

				// Seeding ProductTypes from json file 
				if (!context.ProductTypes.Any())
				{
					var typesJson = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesJson);
					if (types is not null && types.Any())
					{
						await context.ProductTypes.AddRangeAsync(types);
						await context.SaveChangesAsync();
					}

				}
				// Seeding ProductBrands from json file 
				if (!context.ProductBrands.Any())
				{
					var brandsJson = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
					if (brands is not null && brands.Any())
					{
						await context.ProductBrands.AddRangeAsync(brands);
						await context.SaveChangesAsync();
					}

				}
				// Seeding Product from json file 
				if (!context.Products.Any())
				{
					var productsJson = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productsJson);
					if (products is not null && products.Any())
					{
						await context.Products.AddRangeAsync(products);
						await context.SaveChangesAsync();
					}

				}
			}
			catch(Exception ) { throw; }

		}

		public async Task InitializeIdentityAsync()
		{
			if (identityDbContext.Database.GetPendingMigrations().Any())
			{
				await identityDbContext.Database.MigrateAsync();
			}
			if (!roleManager.Roles.Any())
			{
				await roleManager.CreateAsync(new IdentityRole()
				{
					Name = "SuperAdmin"
				});
				await roleManager.CreateAsync(new IdentityRole()
				{
					Name = "Admin"
				});
			}

			if (!userManager.Users.Any())
			{
				var superAdminUser = new AppUser()
				{
					DisplayName = "Super Admin",
					Email = "SuperAdmin@gmail.com",
					UserName = "SuperAdmin",
					PhoneNumber = "0123456789"
				};
				var adminUser = new AppUser()
				{
					DisplayName = "Admin",
					Email = "Admin@gmail.com",
					UserName = "Admin",
					PhoneNumber = "0123456789"
				};
				await userManager.CreateAsync(superAdminUser, "P@ssW0rd");
				await userManager.CreateAsync(adminUser, "P@ssW0rd");
				await userManager.AddToRoleAsync(superAdminUser,"SuperAdmin");
				await userManager.AddToRoleAsync(adminUser,"Admin");
			}
		}
	}
}
