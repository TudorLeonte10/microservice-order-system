using Microsoft.EntityFrameworkCore;
using PaymentService.API;
using PaymentService.Application.Payments.Services;
using PaymentService.Domain.Repositories;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService.Application.Payments.Services.PaymentService>();
builder.Services.AddScoped<IGetPaymentByIdService, GetPaymentByIdService>();

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

app.UseCors(op => op.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
