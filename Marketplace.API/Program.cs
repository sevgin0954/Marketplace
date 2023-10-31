using AutoMapper;
using AutoMapperRegistrar;
using Marketplace.Persistence;
using Marketplace.Persistence.Browsing;
using Marketplace.Persistence.Sales;
using Marketplace.Query;
using MediatR;
using System.Reflection;

namespace Marketplace.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			AddMappingsIfPresent();

			var builder = WebApplication.CreateBuilder(args);

			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			AddServices(builder, configuration);

			var app = builder.Build();

			AddMiddlewares(app);

			app.Run();
		}

		private static void AddMappingsIfPresent()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var mappingTypesFrom = MappingFinder.GetTypesWithMapFrom(assembly);
			var mappingTypesTo = MappingFinder.GetTypesWithMapTo(assembly);
			var customMappingTypes = MappingFinder.GetTypesWitCustomMapping(assembly);

			var config = new MapperConfigurationExpression();
			var mappingRegisterar = new MappingRegisterar(config);

			mappingRegisterar.RegisterMappings(mappingTypesFrom);
			mappingRegisterar.RegisterMappings(mappingTypesTo);

			if (customMappingTypes.Count > 0) 
				mappingRegisterar.RegisterCustomMappings(customMappingTypes);
		}

		private static void AddServices(WebApplicationBuilder builder, IConfigurationRoot configuration)
		{
			builder.Services.AddControllers(options =>
			{
				options.ReturnHttpNotAcceptable = true;
			}).AddXmlDataContractSerializerFormatters();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddMediatR(typeof(Program), typeof(PriceDto));
			builder.Services.AddAutoMapper(typeof(Program), typeof(MarketplaceDbContext), typeof(PriceDto));

			var isLoggingEnabled = true;

			var salesConnectionString = configuration.GetConnectionString("Sales");
			builder.Services
				.AddTransient(s => new SalesDbContext(salesConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

			var browsingConnectionString = configuration.GetConnectionString("Browsing");
			builder.Services
				.AddTransient(s => new BrowsingDbContext(browsingConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));
		}

		private static void AddMiddlewares(WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}