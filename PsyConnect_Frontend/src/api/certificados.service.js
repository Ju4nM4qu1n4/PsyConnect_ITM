import axiosInstance from './axios.config';

const certificadosService = {
    // Generar certificado de test
    generarCertificadoTest: async (respuestaId) => {
        const response = await axiosInstance.post(`/Certificados/generar-test/${respuestaId}`);
        return response.data;
    },

    // Generar certificado de cita
    generarCertificadoCita: async (citaId) => {
        const response = await axiosInstance.post(`/Certificados/generar-cita/${citaId}`);
        return response.data;
    },

    // Obtener certificados de un estudiante
    obtenerCertificadosEstudiante: async (estudianteId) => {
        const response = await axiosInstance.get(`/Certificados/estudiante/${estudianteId}`);
        return response.data;
    },

    // Marcar certificado como descargado
    marcarComoDescargado: async (certificadoId) => {
        const response = await axiosInstance.post(`/Certificados/${certificadoId}/descargar`);
        return response.data;
    },
};

export default certificadosService;