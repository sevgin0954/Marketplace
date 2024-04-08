using AutoMapper;
using AutoMapperRegistrar;
using Marketplace.API.Filters;
using Marketplace.API.Services;
using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Services;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceLayerRegistrar;
using System.Reflection;
using System.Text;

[assembly: ApiController]

namespace Marketplace.API
{
    public class Program
	{
		const string CORS_POLICY_NAME = "CustomPolicy";

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
			var pesistenceAssembly = typeof(MarketplaceDbContext).Assembly;
			var assembliesToMap = new Assembly[] { currentAssembly, queryAsembly, pesistenceAssembly };

			var mappingsFrom = MappingFinder.GetTypesWithMapFrom(assembliesToMap);
			var mappingsTo = MappingFinder.GetTypesWithMapTo(assembliesToMap);
			var customMappings = MappingFinder.GetTypesWitCustomMapping(assembliesToMap);
			var twoDirectionMappings = MappingFinder.GetTypesWithMapBothDirections(assembliesToMap);

			var mappingRegisterar = new MappingRegisterar(configExpression);

			if (mappingsFrom != null && mappingsFrom.Count > 0)
				mappingRegisterar.RegisterMappings(mappingsFrom);

			if (mappingsTo != null && mappingsTo.Count > 0)
				mappingRegisterar.RegisterMappings(mappingsTo);

			if (customMappings != null && customMappings.Count > 0) 
				mappingRegisterar.RegisterCustomMappings(customMappings);

			if (twoDirectionMappings != null && twoDirectionMappings.Count > 0)
				mappingRegisterar.RegisterMappings(twoDirectionMappings);

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
				options.Filters.Add(typeof(MapFromJwtTokenActionFilter));
			}).AddXmlDataContractSerializerFormatters();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddMediatR(typeof(Program), typeof(GetAllProductsQuery), typeof(CreateProductCommand));
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(CORS_POLICY_NAME, builder =>
				{
					builder
					// TODO: Allow only one origin
						.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			builder.Services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					var jwtIssuer = configuration.GetSection(ConfigurationConstants.AuthIssuerKey).Value;
					var jwtKey = configuration.GetSection(ConfigurationConstants.AuthClientSecretKey).Value;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtIssuer,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
					};
				});

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
			builder.Services.AddScoped<IProductService, ProductService>();

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

			app.UseAuthentication();
			app.UseRouting();
			app.UseCors(CORS_POLICY_NAME);
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}