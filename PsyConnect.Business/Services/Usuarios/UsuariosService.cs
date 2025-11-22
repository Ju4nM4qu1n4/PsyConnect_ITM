using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Models.DTOs.Usuarios;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IEstudianteRepository estudianteRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
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
                Token = GenerarToken(usuario)
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

        private string GenerarToken(Usuario usuario)
        {
            
            return $"token_{usuario.UsuarioID}_{DateTime.Now.Ticks}";
        }
    }
}