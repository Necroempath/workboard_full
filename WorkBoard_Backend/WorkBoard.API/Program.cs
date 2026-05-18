
using WorkBoard.API.Extensions;
using WorkBoard.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

Console.WriteLine("??????start");

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddValidation()
    .AddAuthenticationServices(builder.Configuration)
    .AddApiServices()
    .AddSwaggerServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<WorkBoardDbContext>(); 
        
        context.Database.EnsureCreated();
        Console.WriteLine("--- DATABASE READY ---");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--- DB ERROR: {ex.Message} ---");
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerServices();
}

app.UseInfrastructure();

app.MapControllers();

await app.EnsureRoleSeededAsync();

app.Run();
