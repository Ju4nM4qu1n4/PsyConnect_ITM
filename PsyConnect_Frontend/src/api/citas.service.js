import axiosInstance from './axios.config';

const citasService = {
    // Agendar nueva cita
    agendarCita: async (datos) => {
        const response = await axiosInstance.post('/Citas/agendar', {
            estudianteId: datos.estudianteId,
            psicologoId: datos.psicologoId,
            modalidadId: datos.modalidadId,
            fechaHora: datos.fechaHora,
            ubicacion: datos.ubicacion,
            notas: datos.notas,
        });
        return response.data;
    },

    // Obtener todas las citas de un estudiante
    obtenerCitasEstudiante: async (estudianteId) => {
        const response = await axiosInstance.get(`/Citas/estudiante/${estudianteId}`);
        return response.data;
    },

    // Obtener mis citas (del usuario autenticado)
    obtenerMisCitas: async () => {
        const response = await axiosInstance.get('/Citas/mis-citas');
        return response.data;
    },

    // Obtener próximas citas
    obtenerProximasCitas: async () => {
        const response = await axiosInstance.get('/Citas/proximas');
        return response.data;
    },

    // Obtener cita por ID
    obtenerCita: async (citaId) => {
        const response = await axiosInstance.get(`/Citas/${citaId}`);
        return response.data;
    },

    // Cancelar cita
    cancelarCita: async (citaId) => {
        const response = await axiosInstance.put(`/Citas/cancelar/${citaId}`);
        return response.data;
    },

    // Actualizar estado de cita (para psicólogos)
    actualizarEstado: async (citaId, estadoId, notasAdicionales) => {
        const response = await axiosInstance.put(`/Citas/${citaId}/estado`, {
            estadoId: estadoId,
            notasAdicionales: notasAdicionales,
        });
        return response.data;
    },

    // Confirmar asistencia
    confirmarAsistencia: async (citaId) => {
        const response = await axiosInstance.put(`/Citas/${citaId}/confirmar`);
        return response.data;
    },

    // Obtener citas pendientes
    obtenerCitasPendientes: async () => {
        const response = await axiosInstance.get('/Citas/pendientes');
        return response.data;
    },

    // Obtener historial de citas
    obtenerHistorial: async (estudianteId) => {
        const response = await axiosInstance.get(`/Citas/historial/${estudianteId}`);
        return response.data;
    },
};

export default citasService;