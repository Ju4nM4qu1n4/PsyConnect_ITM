import axios from 'axios';

// Crear instancia de axios con configuración base
const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_API_URL, // https://localhost:7197/api
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000, // 10 segundos
});

// Interceptor para agregar el token en cada petición
axiosInstance.interceptors.request.use(
    (config) => {
        // Obtener token del localStorage
        const token = localStorage.getItem('token');

        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Interceptor para manejar respuestas y errores
axiosInstance.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        // Si el token expiró o es inválido (401)
        if (error.response?.status === 401) {
            // Limpiar token y redirigir al login
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            window.location.href = '/login';
        }

        return Promise.reject(error);
    }
);

export default axiosInstance;