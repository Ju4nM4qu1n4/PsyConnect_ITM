import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from '../context/AuthProvider';
import PrivateRoute from './PrivateRoute';

// Importar páginas
import Login from '../pages/Login';
import RegistroEstudiante from '../pages/RegistroEstudiante';
import Dashboard from '../pages/Dashboard';

// Layout con Navbar
import Layout from '../components/Layout';

const AppRoutes = () => {
    return (
        <BrowserRouter>
            <AuthProvider>
                <Routes>
                    {/* Rutas públicas */}
                    <Route path="/login" element={<Login />} />
                    <Route path="/registro" element={<RegistroEstudiante />} />

                    {/* Rutas privadas (con Navbar) */}
                    <Route path="/" element={
                        <PrivateRoute>
                            <Layout />
                        </PrivateRoute>
                    }>
                        <Route index element={<Dashboard />} />
                    </Route>

                    {/* Ruta por defecto */}
                    <Route path="*" element={<Navigate to="/" replace />} />
                </Routes>
            </AuthProvider>
        </BrowserRouter>
    );
};

export default AppRoutes;