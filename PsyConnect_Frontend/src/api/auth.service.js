import axiosInstance from './axios.config';

const authService = {
    // Login de usuario
    login: async (email, password) => {
        const response = await axiosInstance.post('/Usuarios/login', {
            email: email,
            contraseña: password,
        });
        return response.data;
    },

    // Registro de estudiante
    registrarEstudiante: async (datos) => {
        const response = await axiosInstance.post('/Usuarios/registro-estudiante', {
            email: datos.email,
            contraseña: datos.password,
            nombre: datos.nombre,
            apellido: datos.apellido,
            teléfono: datos.telefono,
            matrícula: datos.matricula,
            carrera: datos.carrera,
            semestre: parseInt(datos.semestre),
            género: datos.genero,
            dirección: datos.direccion,
        });
        return response.data;
    },

    // Registro de psicólogo
    registrarPsicologo: async (datos) => {
        const response = await axiosInstance.post('/Usuarios/registro-psicologo', {
            email: datos.email,
            contraseña: datos.password,
            nombre: datos.nombre,
            apellido: datos.apellido,
            teléfono: datos.telefono,
            cédula: datos.cedula,
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

    // Cambiar contraseña
    cambiarContraseña: async (usuarioId, contraseñaActual, nuevaContraseña) => {
        const response = await axiosInstance.post('/Usuarios/cambiar-contraseña', {
            usuarioId: usuarioId,
            contrasenaActual: contraseñaActual,
            nuevaContrasena: nuevaContraseña,
        });
        return response.data;
    },
};

export default authService;