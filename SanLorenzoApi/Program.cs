using Microsoft.EntityFrameworkCore;
using SanLorenzoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configuración de CORS para permitir peticiones desde Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",      // Angular en desarrollo local
                "https://TU-DOMINIO-AMPLIFY.amplifyapp.com"  // Angular ya desplegado (ajusta esta URL luego)
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
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
app.UseCors("AngularApp");
app.UseAuthorization();
app.MapControllers();
app.Run();