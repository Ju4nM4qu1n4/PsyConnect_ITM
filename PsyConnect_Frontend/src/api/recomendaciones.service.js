import axiosInstance from './axios.config';

const recomendacionesService = {
    // Asignar nueva recomendación (solo psicólogos)
    asignarRecomendacion: async (datos) => {
        const response = await axiosInstance.post('/Recomendaciones/asignar', {
            estudianteId: datos.estudianteId,
            psicologoId: datos.psicologoId,
            resultadoId: datos.resultadoId,
            título: datos.titulo,
            descripción: datos.descripcion,
            tipoRecurso: datos.tipoRecurso,
            url: datos.url,
        });
        return response.data;
    },

    // Obtener recomendaciones de un estudiante
    obtenerRecomendacionesEstudiante: async (estudianteId) => {
        const response = await axiosInstance.get(`/Recomendaciones/estudiante/${estudianteId}`);
        return response.data;
    },

    // Obtener recomendaciones vigentes
    obtenerRecomendacionesVigentes: async () => {
        const response = await axiosInstance.get('/Recomendaciones/vigentes');
        return response.data;
    },

    // Eliminar recomendación
    eliminarRecomendacion: async (recomendacionId) => {
        const response = await axiosInstance.delete(`/Recomendaciones/${recomendacionId}`);
        return response.data;
    },
};

export default recomendacionesService;