using Checkout.Business;
using Checkout.Business.EventHandlers;
using Checkout.Core.Aggregates.Basket;
using Checkout.Core.Aggregates.Basket.Services;
using Checkout.Core.Events.Basket;
using Checkout.Core.Events.Interfaces;
using Checkout.Infrastructure.Common;
using Checkout.Infrastructure.Common.Data;
using Checkout.Infrastructure.Sync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CheckoutDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IEventPublisher, InMemoryEventPublisher>();
builder.Services.AddScoped<IEventStore, SqlEventStore>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IBasketReadRepository, BasketSqlReadRepository>();
builder.Services.AddScoped<IEventHandler<BasketCreated>, BasketCreatedEventHandler>();

builder.Services.AddScoped<IEventHandler<BasketArticleAdded>, BasketUpdatedEventHandler>();
builder.Services.AddScoped<IEventHandler<BasketStatusUpdated>, BasketStatusUpdatedEventHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
