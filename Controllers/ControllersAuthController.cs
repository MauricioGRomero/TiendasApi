using Microsoft.AspNetCore.Mvc;
using TiendasAPI.Data;
using TiendasAPI.DTOs;
using TiendasAPI.Models;
using TiendasAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace TiendasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(UsuarioRegistroDTO dto)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo);
            if (existe) return BadRequest("Ya existe un usuario con ese correo.");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Contraseña = dto.Contraseña // Deberías hashearla si fuera producción
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado exitosamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == dto.Correo && u.Contraseña == dto.Contraseña);

            if (usuario == null)
                return Unauthorized("Correo o contraseña incorrectos.");

            var token = _jwtService.GenerarToken(usuario);
            return Ok(new { token });
        }
    }
}
