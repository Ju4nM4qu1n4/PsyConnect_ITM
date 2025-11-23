import axiosInstance from './axios.config';

const citasService = {
    // Agendar nueva cita
    agendarCita: async (datos) => {
        try {
            console.log('Datos enviados:', datos);
            const response = await axiosInstance.post('/Citas/agendar', {
                estudianteId: datos.estudianteId,
                psicologoId: datos.psicologoId,
                modalidadId: datos.modalidadId,
                fechaHora: datos.fechaHora,
                ubicacion: datos.ubicacion,
                notas: datos.notas,
            });
            return response.data;
        } catch (error) {
            console.error('Error completo:', error);
            console.error('Mensaje del servidor:', error.response?.data);
            console.error('Status:', error.response?.status);
            throw error;
        }
    },

    // Obtener cita por ID
    obtenerCita: async (citaId) => {
        const response = await axiosInstance.get(`/Citas/${citaId}`); // ? CORREGIDO
        return response.data;
    },

    // Obtener MIS citas (del estudiante actual autenticado)
    obtenerMisCitas: async () => {
        const response = await axiosInstance.get('/Citas/estudiante'); // ? NUEVO
        return response.data;
    },

    // Obtener citas de un estudiante específico por ID
    obtenerCitasEstudiante: async (estudianteId) => {
        const response = await axiosInstance.get(`/Citas/estudiante/${estudianteId}`); // ? CORREGIDO
        return response.data;
    },

    // Obtener citas de un psicólogo
    obtenerCitasPsicologo: async (psicologoId) => {
        const response = await axiosInstance.get(`/Citas/psicologo/${psicologoId}`); // ? CORREGIDO
        return response.data;
    },

    // Obtener citas próximas
    obtenerCitasProximas: async () => {
        const response = await axiosInstance.get('/Citas/proximas');
        return response.data;
    },

    // Cancelar cita
    cancelarCita: async (citaId) => {
        const response = await axiosInstance.delete(`/Citas/${citaId}`); // ? CORREGIDO
        return response.data;
    },

    // Actualizar estado de cita
    actualizarEstadoCita: async (citaId, nuevoEstado) => {
        const response = await axiosInstance.put(`/Citas/${citaId}/estado/${nuevoEstado}`); // ? CORREGIDO
        return response.data;
    },
};

export default citasService;