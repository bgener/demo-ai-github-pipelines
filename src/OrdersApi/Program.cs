using OrdersApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { Status = "Healthy" }))
   .WithName("HealthCheck");

app.MapPost("/orders/discount", (Order order, OrderService service) =>
{
    var discount = service.CalculateDiscount(order);
    return Results.Ok(new { order.Total, Discount = discount });
})
.WithName("CalculateDiscount");

app.Run();
