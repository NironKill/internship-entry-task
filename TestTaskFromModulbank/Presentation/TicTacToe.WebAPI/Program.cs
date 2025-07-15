using Serilog;
using System.Text.Json.Serialization;
using TicTacToe.Application;
using TicTacToe.Persistence;
using TicTacToe.Persistence.Common;
using TicTacToe.WebAPI.Handlers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
    config.Enrich.FromLogContext();
});

builder.Services.AddHealthChecks();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
});

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    string basePath = AppContext.BaseDirectory;

    string xmlPath = Path.Combine(basePath, "Documentation.xml");
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddExceptionHandler<HandlerException>();
builder.Services.AddProblemDetails();

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/api/health");

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await Preparation.Initialize(context);
}

app.Run();