using AutoMapper;
using AutoMapperRegistrar;
using Marketplace.API.Services;
using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.SharedKernel;
using Marketplace.Domain.SharedKernel.Commands;
using Marketplace.Persistence;
using Marketplace.Persistence.Browsing;
using Marketplace.Persistence.IdentityAndAccess;
using Marketplace.Persistence.Sales;
using Marketplace.Query;
using Marketplace.Query.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceLayerRegistrar;
using System.Reflection;
using System.Text;

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

			//builder.Services
			//	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			//	.AddJwtBearer(options =>
			//	{
			//		var jwtIssuer = configuration.GetSection("Auth0:Issuer").Value;
			//		var jwtAudience = configuration.GetSection("Auth0:Audience").Value;
			//		var jwtKey = configuration.GetSection("Auth0:ClientSecret").Value;
			//		options.TokenValidationParameters = new TokenValidationParameters()
			//		{
			//			ValidateIssuer = true,
			//			ValidateAudience = true,
			//			ValidateLifetime = true,
			//			ValidateIssuerSigningKey = true,
			//			ValidIssuer = jwtIssuer,
			//			ValidAudience = jwtAudience,
			//			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
			//		};
			//	});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddMediatR(typeof(Program), typeof(GetAllProductsQuery), typeof(CreateProductCommand));

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

			builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

			var serviceRegistrar = new ServiceRegistrar(builder.Services);

			var persistenceAssembly = typeof(MarketplaceDbContext).Assembly;

			var iRepositoryType = typeof(IRepository<AggregateRoot, Id>);
			serviceRegistrar.RegisterScopedServices(persistenceAssembly, iRepositoryType);

			var iOfferRepositoryType = typeof(IRepository<Offer, OfferId>);
			serviceRegistrar.RegisterScopedServices(persistenceAssembly, iOfferRepositoryType);
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
			app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}