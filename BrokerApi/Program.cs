using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Service.Metodos;


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
builder.Services.AddScoped<IRegistroEstadoService,RegistroEstadoService>();

//Coneccion con el Front
var proveedor = builder.Services.BuildServiceProvider();
var configuracion = proveedor.GetRequiredService<IConfiguration>();

builder.Services.AddCors(opciones =>
{
    var frontendURL = configuracion.GetValue<string>("frontend_url"); //acceso a mi app de react

    opciones.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
    });

});

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
