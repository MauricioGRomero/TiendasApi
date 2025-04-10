using Microsoft.EntityFrameworkCore;
using TiendasAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TiendasAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la cadena de conexi�n a la base de datos
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
            ValidateLifetime = true,  // Validar la expiraci�n del token
            ClockSkew = TimeSpan.Zero  // Evitar retrasos en la expiraci�n
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware para autenticaci�n y autorizaci�n
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
