using Microsoft.EntityFrameworkCore;
using OrderManager.Core;
using OrderManager.Data;
using OrderManager.Models;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDataBase")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:5264"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
