using Marketplace.Persistence;
using Marketplace.Persistence.Browsing;
using Marketplace.Persistence.Sales;
using Marketplace.Query;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program), typeof(PriceDto));
builder.Services.AddAutoMapper(typeof(Program), typeof(MarketplaceDbContext), typeof(PriceDto));

var configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.Build();

var isLoggingEnabled = true;

var salesConnectionString = configuration.GetConnectionString("Sales");
builder.Services
    .AddTransient(s => new SalesDbContext(salesConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

var browsingConnectionString = configuration.GetConnectionString("Browsing");
builder.Services
    .AddTransient(s => new BrowsingDbContext(browsingConnectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

var app = builder.Build();

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

app.Run();