import axiosInstance from './axios.config';

const citasService = {
    // Agendar nueva cita
    agendarCita: async (datos) => {
        const response = await axiosInstance.post('/Citas/agendar', {
            estudianteId: datos.estudianteId,
            psicologoId: datos.psicologoId,
            modalidadId: datos.modalidadId,
            fechaHora: datos.fechaHora,
            ubicacion: datos.ubicacion || '',
            notas: datos.notas || '',
        });
        return response.data;
    },

    obtenerMisCitas: async () => {
        const response = await axiosInstance.get('/Citas/mis-citas');
        return response.data;
    },

    // Obtener citas de un estudiante
    obtenerCitasEstudiante: async (estudianteId) => {
        const response = await axiosInstance.get(`/Citas/estudiante/${estudianteId}`);
        return response.data;
    },

    // Obtener próximas citas
    obtenerProximasCitas: async () => {
        const response = await axiosInstance.get('/Citas/proximas');
        return response.data;
    },

    // Cancelar cita - CAMBIADO DE POST A DELETE
    cancelarCita: async (citaId) => {
        const response = await axiosInstance.delete(`/Citas/${citaId}`);
        return response.data;
    },

    // Obtener cita por ID
    obtenerCita: async (citaId) => {
        const response = await axiosInstance.get(`/Citas/${citaId}`);
        return response.data;
    },
};

export default citasService;