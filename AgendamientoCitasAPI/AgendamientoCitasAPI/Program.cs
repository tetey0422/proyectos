using Microsoft.EntityFrameworkCore;
using AgendamientoCitasAPI.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProyectoBD>(options => 
       options.UseMySql(
           builder.Configuration.GetConnectionString("MySqlConnection"),
           ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))
       )
   );
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
