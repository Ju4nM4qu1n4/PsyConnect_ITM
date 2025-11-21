using AutoMapper;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Entities.Citas;
using PsyConnect.Core.Entities.Tests;
using PsyConnect.Core.Entities.Respuestas;
using PsyConnect.Core.Entities.Resultados;
using PsyConnect.Core.Entities.Recomendaciones;
using PsyConnect.Core.Entities.Certificados;
using PsyConnect.Core.Models.DTOs.Usuarios;
using PsyConnect.Core.Models.DTOs.Citas;
using PsyConnect.Core.Models.DTOs.Tests;
using PsyConnect.Core.Models.DTOs.Resultados;
using PsyConnect.Core.Models.DTOs.Recomendaciones;
using PsyConnect.Core.Models.DTOs.Certificados;
using PsyConnect.Core.Models.Requests;

namespace PsyConnect.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<Usuario, UsuarioDTO>()
                .ReverseMap();

            CreateMap<Estudiante, EstudianteDTO>()
                .ReverseMap();

            CreateMap<Psicólogo, PsicologoDTO>()
                .ReverseMap();

            CreateMap<RegistrarEstudianteRequest, Usuario>();
            CreateMap<RegistrarPsicologoRequest, Usuario>();

           

            CreateMap<Cita, CitaDTO>()
                .ForMember(dest => dest.ModalidadNombre, opt => opt.MapFrom(src => src.ModalidadCita.Nombre))
                .ForMember(dest => dest.EstadoNombre, opt => opt.MapFrom(src => src.EstadoCita.Nombre))
                .ReverseMap();

            CreateMap<AgendarCitaRequest, Cita>();

        

            CreateMap<Test, TestDTO>()
                .ForMember(dest => dest.TipoTestNombre, opt => opt.MapFrom(src => src.TipoTest.Nombre))
                .ForMember(dest => dest.ModalidadNombre, opt => opt.MapFrom(src => src.ModalidadTest.Nombre))
                .ForMember(dest => dest.Preguntas, opt => opt.MapFrom(src => src.PreguntasTest))
                .ReverseMap();

            CreateMap<PreguntaTest, PreguntaTestDTO>()
                .ForMember(dest => dest.Opciones, opt => opt.MapFrom(src => src.OpcionesRespuesta))
                .ReverseMap();

            CreateMap<OpcionRespuesta, OpcionRespuestaDTO>()
                .ReverseMap();

            CreateMap<RespuestaTest, RespuestaTestDTO>()
                .ForMember(dest => dest.EstadoNombre, opt => opt.MapFrom(src => src.EstadoRespuestaTest.Nombre))
                .ReverseMap();

            

            CreateMap<ResultadoInterpretacion, ResultadoDTO>()
                .ReverseMap();

       

            CreateMap<RecomendacionPersonalizada, RecomendacionDTO>()
                .ReverseMap();

            CreateMap<AsignarRecomendacionRequest, RecomendacionPersonalizada>();

       

            CreateMap<Certificado, CertificadoDTO>()
                .ReverseMap();
        }
    }
}