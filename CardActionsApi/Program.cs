using CardActionsApi.Models;
using CardActionsApi.Providers;
using CardActionsApi.Providers.Actions;
using CardActionsApi.Services.Actions;
using CardActionsApi.Services.Card;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddSingleton<ISpecificationsProvider<CardDetails>, ActionsSpecificationsProvider>();


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