using PsyConnect.Core.Entities.Certificados;
using PsyConnect.Core.Models.DTOs.Certificados;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Certificados
{
    public class CertificadoService : ICertificadoService
    {
        private readonly ICertificadoRepository _certificadoRepository;
        private readonly IRespuestaTestRepository _respuestaTestRepository;
        private readonly ICitaRepository _citaRepository;
        private readonly IMapper _mapper;

        public CertificadoService(
            ICertificadoRepository certificadoRepository,
            IRespuestaTestRepository respuestaTestRepository,
            ICitaRepository citaRepository,
            IMapper mapper)
        {
            _certificadoRepository = certificadoRepository;
            _respuestaTestRepository = respuestaTestRepository;
            _citaRepository = citaRepository;
            _mapper = mapper;
        }

        public async Task<CertificadoDTO> GenerarCertificadoTestAsync(int respuestaId)
        {
            var respuesta = await _respuestaTestRepository.GetByIdAsync(respuestaId);
            if (respuesta == null)
                throw new Exception("Respuesta no encontrada");

            if (respuesta.EstadoID != 2) 
                throw new Exception("Solo puedes generar certificado de tests completados");

            var certificado = new Certificado
            {
                EstudianteID = respuesta.EstudianteID,
                RespuestaTestID = respuestaId,
                TipoCertificado = "Test",
                FechaGeneracion = DateTime.Now,
                RutaArchivo = $"/certificados/test_{respuestaId}_{DateTime.Now.Ticks}.pdf",
                Descargado = false
            };

            await _certificadoRepository.AddAsync(certificado);
            await _certificadoRepository.SaveChangesAsync();

            return _mapper.Map<CertificadoDTO>(certificado);
        }

        public async Task<CertificadoDTO> GenerarCertificadoCitaAsync(int citaId)
        {
            var cita = await _citaRepository.GetByIdAsync(citaId);
            if (cita == null)
                throw new Exception("Cita no encontrada");

            if (cita.EstadoID != 4) 
                throw new Exception("Solo puedes generar certificado de citas completadas");

            var certificado = new Certificado
            {
                EstudianteID = cita.EstudianteID,
                CitaID = citaId,
                TipoCertificado = "Cita",
                FechaGeneracion = DateTime.Now,
                RutaArchivo = $"/certificados/cita_{citaId}_{DateTime.Now.Ticks}.pdf",
                Descargado = false
            };

            await _certificadoRepository.AddAsync(certificado);
            await _certificadoRepository.SaveChangesAsync();

            return _mapper.Map<CertificadoDTO>(certificado);
        }

        public async Task<IEnumerable<CertificadoDTO>> ObtenerCertificadosEstudianteAsync(int estudianteId)
        {
            var certificados = await _certificadoRepository.GetCertificadosPorEstudianteAsync(estudianteId);
            return _mapper.Map<IEnumerable<CertificadoDTO>>(certificados);
        }

        public async Task MarcarComoDescargadoAsync(int certificadoId)
        {
            var certificado = await _certificadoRepository.GetByIdAsync(certificadoId);
            if (certificado == null)
                throw new Exception("Certificado no encontrado");

            certificado.Descargado = true;
            certificado.FechaDescarga = DateTime.Now;
            _certificadoRepository.Update(certificado);
            await _certificadoRepository.SaveChangesAsync();
        }
    }
}