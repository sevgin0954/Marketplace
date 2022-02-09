using SalesPersistence = Marketplace.Infrastructure.Sales.BuyerPersistence;
using Marketplace.Infrastructure.Sales.ProductPersistence;
using Marketplace.Infrastructure.Sales.SellerPersistence;
using ShippingPesistence = Marketplace.Infrastructure.Shipping.BuyerPersistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Marketplace.Infrastructure.Shipping.OrderPersistence;

namespace Marketplace.UI
{
	public class Startup
	{
		const string CONNECTION_STRING_NAME = "Marketplace";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Startup));

			services.AddControllersWithViews();

			services.AddScoped(
				_ => new SalesPersistence.BuyerDbContext(Configuration.GetConnectionString(CONNECTION_STRING_NAME))
			);
			services.AddScoped(
				_ => new ProductDbContext(Configuration.GetConnectionString(CONNECTION_STRING_NAME))
			);
			services.AddScoped(
				_ => new SellerDbContext(Configuration.GetConnectionString(CONNECTION_STRING_NAME))
			);
			services.AddScoped(
				_ => new ShippingPesistence.BuyerDbContext(Configuration.GetConnectionString(CONNECTION_STRING_NAME))
			);
			services.AddScoped(
				_ => new OrderDbContext(Configuration.GetConnectionString(CONNECTION_STRING_NAME))
			);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}