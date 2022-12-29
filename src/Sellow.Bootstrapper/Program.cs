using Sellow.Modules.Shared.Infrastructure;
using Sellow.Modules.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args).AddSerilog();

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddInfrastructure();

var app = builder.Build();

app.UseHttpsRedirection()
    .UseAuthorization()
    .UseInfrastructure();
app.MapControllers();

app.Run();