using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Bloom.API.Services;
using System.Text.Json.Serialization;
using Bloom.API.Services.Interfaces;
using Bloom.Persistence;
using Bloom.Persistence.Repositories;
using Bloom.Persistence.Repositories.Interfaces;
using Bloom.Persistence.Settings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); ;
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bloom API", Version = "v1" });
});

builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<ITradingService, TradingService>();
builder.Services.AddScoped<ITradeExecutionService, TradeExecutionService>();
builder.Services.AddScoped<IIndicatorRuleService, IndicatorRuleService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IIndicatorRuleRepository, IndicatorRuleRepository>();
builder.Services.AddScoped<ITickerIndicatorRepository, TickerIndicatorRepository>();
builder.Services.AddScoped<ITradeLogRepository, TradeLogRepository>();
builder.Services.AddScoped<IPortfolioTransactionRepository, PortfolioTransactionRepository>();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<BloomDbContext>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();