using Marketplace.Persistence.Sales;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program), typeof(Marketplace.Query.PriceDto));
builder.Services.AddAutoMapper(typeof(Program));

var configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.Build();

var connectionString = configuration["ConnectionStrings:Marketplace"];
var isLoggingEnabled = true;
builder.Services.AddTransient(s => new SalesDbContext(connectionString, isLoggingEnabled, s.GetRequiredService<IMediator>()));

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
