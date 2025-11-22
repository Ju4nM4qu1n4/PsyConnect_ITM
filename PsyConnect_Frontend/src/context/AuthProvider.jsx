import React, { useState, useEffect } from 'react';
import { AuthContext } from './AuthContext';
import authService from '../api/auth.service';

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(null);
    const [loading, setLoading] = useState(true);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    // Verificar si hay sesion activa al cargar la app
    useEffect(() => {
        checkAuth();
    }, []);

    // Funcion para verificar autenticacion
    const checkAuth = () => {
        try {
            const storedToken = localStorage.getItem('token');
            const storedUser = localStorage.getItem('user');

            if (storedToken && storedUser) {
                setToken(storedToken);
                setUser(JSON.parse(storedUser));
                setIsAuthenticated(true);
            }
        } catch (error) {
            console.error('Error al verificar autenticacion:', error);
            logout();
        } finally {
            setLoading(false);
        }
    };

    // Funcion de login
    const login = async (email, password) => {
        try {
            const response = await authService.login(email, password);

            if (response.datos && response.datos.token) {
                const userData = {
                    usuarioID: response.datos.usuarioID,
                    email: response.datos.email,
                    nombre: response.datos.nombre,
                    apellido: response.datos.apellido,
                    tipoUsuario: response.datos.tipoUsuario,
                };

                localStorage.setItem('token', response.datos.token);
                localStorage.setItem('user', JSON.stringify(userData));

                setToken(response.datos.token);
                setUser(userData);
                setIsAuthenticated(true);

                return { success: true, data: userData };
            } else {
                throw new Error('Respuesta invalida del servidor');
            }
        } catch (error) {
            console.error('Error en login:', error);
            return {
                success: false,
                error: error.response?.data?.detalle || 'Error al iniciar sesion'
            };
        }
    };

    // Funcion de registro de estudiante
    const registrarEstudiante = async (datos) => {
        try {
            const response = await authService.registrarEstudiante(datos);

            if (response.exitoso) {
                return { success: true, data: response.datos };
            } else {
                throw new Error(response.mensaje || 'Error en el registro');
            }
        } catch (error) {
            console.error('Error en registro:', error);
            return {
                success: false,
                error: error.response?.data?.detalle || 'Error al registrar usuario'
            };
        }
    };

    // Funcion de logout
    const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        setToken(null);
        setUser(null);
        setIsAuthenticated(false);
    };

    // Funcion para actualizar el perfil del usuario
    const updateUser = (updatedUser) => {
        setUser(updatedUser);
        localStorage.setItem('user', JSON.stringify(updatedUser));
    };

    const value = {
        user,
        token,
        loading,
        isAuthenticated,
        login,
        logout,
        registrarEstudiante,
        updateUser,
        checkAuth,
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthProvider;
