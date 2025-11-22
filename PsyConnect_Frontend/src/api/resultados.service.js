import axiosInstance from './axios.config';

const resultadosService = {
    // Evaluar test (genera resultado)
    evaluarTest: async (respuestaId) => {
        const response = await axiosInstance.post(`/Resultados/evaluar/${respuestaId}`);
        return response.data;
    },

    // Obtener resultado por ID
    obtenerResultado: async (resultadoId) => {
        const response = await axiosInstance.get(`/Resultados/${resultadoId}`);
        return response.data;
    },

    // Obtener todos los resultados de un estudiante
    obtenerResultadosEstudiante: async (estudianteId) => {
        const response = await axiosInstance.get(`/Resultados/estudiante/${estudianteId}`);
        return response.data;
    },
};

export default resultadosService;