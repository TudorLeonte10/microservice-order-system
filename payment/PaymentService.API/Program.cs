using Microsoft.EntityFrameworkCore;
using PaymentService.API;
using PaymentService.Application.Abstractions;
using PaymentService.Application.Payments.Services;
using PaymentService.Domain.Repositories;
using PaymentService.Infrastructure.Clients;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Repositories;
using PaymentService.Infrastructure.Webhooks;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService.Application.Payments.Services.PaymentService>();
builder.Services.AddScoped<IGetPaymentByIdService, GetPaymentByIdService>();
builder.Services.AddScoped<PaymentService.Application.Abstractions.IStripeClient, PaymentService.Infrastructure.Clients.StripeClient>();
builder.Services.AddScoped<IStripeWebhookService, StripeWebhookService>();

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

builder.Services.AddHttpClient<IOrderServiceClient, OrderServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OrderService:BaseUrl"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Service API v1");
        options.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    dbContext.Database.EnsureCreated();
}


app.UseCors(op => op.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
