using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BrokerDBContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("BrokerConnection")));

//builder.Services.AddAutoMapper(typeof(BrokerDBContext));

builder.Services.AddScoped<Service.Interface.IBancoService, Service.Metodos.BancoService>();
builder.Services.AddScoped<ICuentaService, Service.Metodos.CuentaService>();
builder.Services.AddScoped<ITransaccionService, Service.Metodos.TransaccionService>();


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
