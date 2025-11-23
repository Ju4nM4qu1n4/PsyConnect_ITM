using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Models.DTOs.Usuarios;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace PsyConnect.Business.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IEstudianteRepository estudianteRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UsuarioDTO> RegistrarEstudianteAsync(RegistrarEstudianteRequest request)
        {
            var usuarioExistente = await _usuarioRepository.GetUsuarioPorEmailAsync(request.Email);
            if (usuarioExistente != null)
                throw new Exception("El email ya esta registrado");

            ValidarContrasena(request.Contrasena);

            var usuario = new Usuario
            {
                Email = request.Email,
                Contrasena = EncriptarContrasena(request.Contrasena),
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                TipoUsuario = "Estudiante",
                Telefono = request.Telefono,
                FechaRegistro = DateTime.Now,
                Estado = true
            };

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            var estudiante = new Estudiante
            {
                UsuarioID = usuario.UsuarioID,
                Matricula = request.Matricula,
                Carrera = request.Carrera,
                Semestre = request.Semestre,
                Genero = request.Genero,
                Direccion = request.Direccion
            };

            await _estudianteRepository.AddAsync(estudiante);
            await _estudianteRepository.SaveChangesAsync();

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> RegistrarPsicologoAsync(RegistrarPsicologoRequest request)
        {
            var usuarioExistente = await _usuarioRepository.GetUsuarioPorEmailAsync(request.Email);
            if (usuarioExistente != null)
                throw new Exception("El email ya esta registrado");

            ValidarContrasena(request.Contrasena);

            var usuario = new Usuario
            {
                Email = request.Email,
                Contrasena = EncriptarContrasena(request.Contrasena),
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                TipoUsuario = "Psicologo",
                Telefono = request.Telefono,
                FechaRegistro = DateTime.Now,
                Estado = true
            };

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<AuthResponse> AutenticarAsync(LoginRequest request)
        {
            var usuario = await _usuarioRepository.GetUsuarioPorEmailAsync(request.Email);
            if (usuario == null || !VerificarContrasena(request.Contrasena, usuario.Contrasena))
                throw new Exception("Email o contrasena invalidos");

            if (!usuario.Estado)
                throw new Exception("Usuario desactivado");

            usuario.UltimoAcceso = DateTime.Now;
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return new AuthResponse
            {
                UsuarioID = usuario.UsuarioID,
                Email = usuario.Email,
                Nombre = usuario.Nombre,
                TipoUsuario = usuario.TipoUsuario,
                Token = await GenerarToken(usuario) // ← AGREGADO await
            };
        }

        public async Task<UsuarioDTO> ObtenerPerfilAsync(int usuarioId)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task CambiarContrasenaAsync(int usuarioId, string contrasenaActual, string nuevaContrasena)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            if (!VerificarContrasena(contrasenaActual, usuario.Contrasena))
                throw new Exception("Contrasena actual invalida");

            ValidarContrasena(nuevaContrasena);

            usuario.Contrasena = EncriptarContrasena(nuevaContrasena);
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }

        private void ValidarContrasena(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena) || contrasena.Length < 8)
                throw new Exception("Contrasena debe tener al menos 8 caracteres");
        }

        private string EncriptarContrasena(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena);
        }

        private bool VerificarContrasena(string contrasena, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(contrasena, hash);
        }

        // ← NUEVO MÉTODO CON JWT REAL
        private async Task<string> GenerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim("TipoUsuario", usuario.TipoUsuario)
            };

            // Si es estudiante, agregar el EstudianteID
            if (usuario.TipoUsuario == "Estudiante")
            {
                var estudiante = await _estudianteRepository.GetEstudiantePorUsuarioAsync(usuario.UsuarioID);
                if (estudiante != null)
                {
                    claims.Add(new Claim("EstudianteId", estudiante.EstudianteID.ToString()));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? "ClaveSecretaSuperSegura123456789012345678901234567890"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "PsyConnect",
                audience: _configuration["Jwt:Audience"] ?? "PsyConnectUsers",
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}