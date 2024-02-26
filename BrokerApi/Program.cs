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

builder.Services.AddDbContext<ApiDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BrokerConnection"),
        b => b.MigrationsAssembly("BrokerApi"))); // Especifica aquí tu ensamblado de migraciones


//builder.Services.AddAutoMapper(typeof(ApiDb));

builder.Services.AddScoped<Service.Interface.IBancoService, Service.Metodos.BancoService>();
builder.Services.AddScoped<ICuentaService, Service.Metodos.CuentaService>();
builder.Services.AddScoped<ITransaccionService, Service.Metodos.TransaccionService>();
builder.Services.AddScoped<IRegistroEstadoService,RegistroEstadoService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
    policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    //------
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
