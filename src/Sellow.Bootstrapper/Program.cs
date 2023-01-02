using Sellow.Modules.Auth.Api;
using Sellow.Modules.EmailSending.Core;
using Sellow.Modules.Shared.Infrastructure;
using Sellow.Modules.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args).AddSerilog();

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddInfrastructure()
    .AddAuthModule()
    .AddEmailSendingModule();

var app = builder.Build();

app.UseHttpsRedirection()
    .UseInfrastructure()
    .UseAuthModule();

app.MapControllers();

app.Run();