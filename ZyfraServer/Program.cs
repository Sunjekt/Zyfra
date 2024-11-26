using System.Text.Json.Serialization;
using ZyfraServer.Models;
using ZyfraServer.Data;
using Microsoft.AspNetCore.Identity;
using ZyfraServer.Intefaces.Services;
using ZyfraServer.Servieces;
using ZyfraServer.Interfaces.Repositories;
using ZyfraServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddDbContext<ModelsManager>();

builder.Services.AddScoped<IZyfraDataService, ZyfraDataService>();
builder.Services.AddScoped<IZyfraDataRepository, ZyfraDataRepository>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var modelsManager = scope.ServiceProvider.GetRequiredService<ModelsManager>();
    await ModelsManagerSeed.SeedAsync(modelsManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();
