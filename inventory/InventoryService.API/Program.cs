using InventoryService.API;
using InventoryService.Application;
using InventoryService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Service API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors(op => op.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();



