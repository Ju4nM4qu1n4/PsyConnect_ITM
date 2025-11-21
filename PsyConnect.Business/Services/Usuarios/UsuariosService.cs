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
                throw new Exception("El email ya está registrado");

            
            ValidarContraseña(request.Contraseña);

           
            var usuario = new Usuario
            {
                Email = request.Email,
                Contraseña = EncriptarContraseña(request.Contraseña),
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                TipoUsuario = "Estudiante",
                Teléfono = request.Teléfono,
                FechaRegistro = DateTime.Now,
                Estado = true

            };

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

         
            var estudiante = new Estudiante
            {
                UsuarioID = usuario.UsuarioID,
                Matrícula = request.Matrícula,
                Carrera = request.Carrera,
                Semestre = request.Semestre,
                Género = request.Género,
                Dirección = request.Dirección
            };

            await _estudianteRepository.AddAsync(estudiante);
            await _estudianteRepository.SaveChangesAsync();

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> RegistrarPsicologoAsync(RegistrarPsicologoRequest request)
        {
           
            var usuarioExistente = await _usuarioRepository.GetUsuarioPorEmailAsync(request.Email);
            if (usuarioExistente != null)
                throw new Exception("El email ya está registrado");

          
            ValidarContraseña(request.Contraseña);

          
            var usuario = new Usuario
            {
                Email = request.Email,
                Contraseña = EncriptarContraseña(request.Contraseña),
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                TipoUsuario = "Psicólogo",
                Teléfono = request.Teléfono,
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
            if (usuario == null || !VerificarContraseña(request.Contraseña, usuario.Contraseña))
                throw new Exception("Email o contraseña inválidos");

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

        public async Task CambiarContraseñaAsync(int usuarioId, string contraseñaActual, string nuevaContraseña)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            if (!VerificarContraseña(contraseñaActual, usuario.Contraseña))
                throw new Exception("Contraseña actual inválida");

            ValidarContraseña(nuevaContraseña);

            usuario.Contraseña = EncriptarContraseña(nuevaContraseña);
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }

    
        private void ValidarContraseña(string contraseña)
        {
            if (string.IsNullOrWhiteSpace(contraseña) || contraseña.Length < 8)
                throw new Exception("Contraseña debe tener al menos 8 caracteres");
        }

        private string EncriptarContraseña(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }

        private bool VerificarContraseña(string contraseña, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(contraseña, hash);
        }

        private string GenerarToken(Usuario usuario)
        {
            
            return $"token_{usuario.UsuarioID}_{DateTime.Now.Ticks}";
        }
    }
}