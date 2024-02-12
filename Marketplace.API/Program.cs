using AutoMapper;
using AutoMapperRegistrar;
using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.SharedKernel;
using Marketplace.Persistence;
using Marketplace.Persistence.Browsing;
using Marketplace.Persistence.IdentityAndAccess;
using Marketplace.Persistence.SagaData;
using Marketplace.Persistence.Sales;
using Marketplace.Query;
using Marketplace.Query.ProductQueries;
using MediatR;
using ServiceLayerRegistrar;
using ServiceLayerRegistrar.CustomGenericConstraints;
using System.Reflection;

namespace Marketplace.API
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var mappingConfigExpression = new MapperConfigurationExpression();
			AddMappings(mappingConfigExpression);

			var mappingConfig = new MapperConfiguration(mappingConfigExpression);
			AddServices(builder, configuration, mappingConfig);

			var app = builder.Build();

			AddMiddlewares(app);

			app.Run();
		}

		private static void AddMappings(MapperConfigurationExpression configExpression)
		{
			var currentAssembly = Assembly.GetExecutingAssembly();
			var queryAsembly = typeof(PriceDto).Assembly;

			var mappingTypesFrom = MappingFinder.GetTypesWithMapFrom(currentAssembly, queryAsembly);
			var mappingTypesTo = MappingFinder.GetTypesWithMapTo(currentAssembly, queryAsembly);
			var customMappingTypes = MappingFinder.GetTypesWitCustomMapping(currentAssembly, queryAsembly);

			var mappingRegisterar = new MappingRegisterar(configExpression);

			if (mappingTypesFrom != null && mappingTypesFrom.Count > 0)
				mappingRegisterar.RegisterMappings(mappingTypesFrom);

			if (mappingTypesTo != null && mappingTypesTo.Count > 0)
				mappingRegisterar.RegisterMappings(mappingTypesTo);

			if (customMappingTypes != null && customMappingTypes.Count > 0) 
				mappingRegisterar.RegisterCustomMappings(customMappingTypes);

			// TODO: AutomapperProfile doesnt work!
			configExpression.AddProfile<AutoMapperProfile>();
		}

		private static void AddServices(
			WebApplicationBuilder builder, 
			IConfigurationRoot configuration,
			MapperConfiguration mapperConfiguration)
		{
			builder.Services.AddControllers(options =>
			{
				options.ReturnHttpNotAcceptable = true;
			}).AddXmlDataContractSerializerFormatters();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddMediatR(typeof(Program), typeof(GetAllProductsQuery));

			var mapper = mapperConfiguration.CreateMapper();
			builder.Services.AddSingleton(mapper);

			var isLoggingEnabled = true;

			var identityAndBrowsingConnectionString = configuration.GetConnectionString("IdentityAndAccess");
			builder.Services
				.AddTransient(s => new IdentityAndAccessDbContext(identityAndBrowsingConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

			var salesConnectionString = configuration.GetConnectionString("Sales");
			builder.Services
				.AddTransient(s => new SalesDbContext(salesConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

			var browsingConnectionString = configuration.GetConnectionString("Browsing");
			builder.Services
				.AddTransient(s => new BrowsingDbContext(browsingConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

			var sagaDataConnectionString = configuration.GetConnectionString("SagaData");
			builder.Services
				.AddTransient(s => new SagaDataDbContext(sagaDataConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

			var serviceRegistrar = new ServiceCollectionRegistrar(builder.Services);

			var persistenceAssembly = typeof(MarketplaceDbContext).Assembly;

			var iRepositoryType = typeof(IRepository<Any, Id>);
			serviceRegistrar.RegisterScopedServices(persistenceAssembly, iRepositoryType);

			// TODO: Make custom generic constraints work for id too
			var iOfferRepositoryType = typeof(IRepository<Offer, OfferId>);
			serviceRegistrar.RegisterScopedServices(persistenceAssembly, iOfferRepositoryType);

			var iSagaDataRepositoryType = typeof(ISagaDataRepository<,>);
			serviceRegistrar.RegisterScopedServices(persistenceAssembly, iSagaDataRepositoryType);
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