import axiosInstance from './axios.config';

const testsService = {
    // Obtener test por ID con preguntas
    obtenerTest: async (testId) => {
        const response = await axiosInstance.get(`/Tests/${testId}`);
        return response.data;
    },

    // Obtener todos los tests activos
    obtenerTestsActivos: async () => {
        const response = await axiosInstance.get('/Tests/activos');
        return response.data;
    },

    // Obtener tests por tipo
    obtenerTestsPorTipo: async (tipoTestId) => {
        const response = await axiosInstance.get(`/Tests/tipo/${tipoTestId}`);
        return response.data;
    },

    // Iniciar un test (crear respuesta)
    iniciarTest: async (estudianteId, testId) => {
        const response = await axiosInstance.post(`/Tests/iniciar/${estudianteId}/${testId}`);
        return response.data;
    },

    // Guardar respuestas del test
    guardarRespuestas: async (respuestaId, respuestas) => {
        const response = await axiosInstance.post('/Tests/guardar-respuesta', {
            respuestaId,
            respuestas, // Array de { preguntaId, opcionSeleccionada }
        });
        return response.data;
    },

    // Completar test
    completarTest: async (respuestaId) => {
        const response = await axiosInstance.post(`/Tests/completar/${respuestaId}`);
        return response.data;
    },
};

export default testsService;
