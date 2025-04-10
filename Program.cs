using Microsoft.EntityFrameworkCore;
using TiendasAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TiendasAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu_clave_secreta")),
            ValidateIssuer = false,  // Cambiar a true si usas un emisor
            ValidateAudience = false,  // Cambiar a true si usas una audiencia
            ValidateLifetime = true,  // Validar la expiración del token
            ClockSkew = TimeSpan.Zero  // Evitar retrasos en la expiración
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configuración del pipeline de solicitudes
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware para autenticación y autorización
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
