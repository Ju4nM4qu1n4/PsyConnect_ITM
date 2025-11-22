import axiosInstance from './axios.config';

const authService = {
    // Login de usuario
    login: async (email, password) => {
        const response = await axiosInstance.post('/Usuarios/login', {
            email: email,
            contrasena: password,
        });
        return response.data;
    },

    // Registro de estudiante
    registrarEstudiante: async (datos) => {
        const response = await axiosInstance.post('/Usuarios/registro-estudiante', {
            email: datos.email,
            contrasena: datos.password,
            nombre: datos.nombre,
            apellido: datos.apellido,
            telefono: datos.telefono,
            matricula: datos.matricula,
            carrera: datos.carrera,
            semestre: parseInt(datos.semestre),
            genero: datos.genero,
            direccion: datos.direccion,
        });
        return response.data;
    },

    // Registro de psicologo
    registrarPsicologo: async (datos) => {
        const response = await axiosInstance.post('/Usuarios/registro-psicologo', {
            email: datos.email,
            contrasena: datos.password,
            nombre: datos.nombre,
            apellido: datos.apellido,
            telefono: datos.telefono,
            cedula: datos.cedula,
            especialidad: datos.especialidad,
            licencia: datos.licencia,
        });
        return response.data;
    },

    // Obtener perfil de usuario
    obtenerPerfil: async (usuarioId) => {
        const response = await axiosInstance.get(`/Usuarios/perfil/${usuarioId}`);
        return response.data;
    },

    // Cambiar contrasena
    cambiarContrasena: async (usuarioId, contrasenaActual, nuevaContrasena) => {
        const response = await axiosInstance.post('/Usuarios/cambiar-contrasena', {
            usuarioId: usuarioId,
            contrasenaActual: contrasenaActual,
            nuevaContrasena: nuevaContrasena,
        });
        return response.data;
    },
};

export default authService;