import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from '../context/AuthProvider';
import PrivateRoute from './PrivateRoute';

// Importar paginas
import Login from '../pages/Login';
import RegistroEstudiante from '../pages/RegistroEstudiante';
import Dashboard from '../pages/Dashboard';
import Tests from '../pages/Tests';
import RealizarTest from '../pages/RealizarTest';
import ResultadoTest from '../pages/ResultadoTest';
import AgendarCita from '../pages/AgendarCita';
import MisCitas from '../pages/MisCitas';
import MisResultados from '../pages/MisResultados';
import MisRecomendaciones from '../pages/MisRecomendaciones';
import MisCertificados from '../pages/MisCertificados';

// Layout con Navbar
import Layout from '../components/Layout';

const AppRoutes = () => {
    return (
        <BrowserRouter>
            <AuthProvider>
                <Routes>
                    {/* Rutas publicas */}
                    <Route path="/login" element={<Login />} />
                    <Route path="/registro" element={<RegistroEstudiante />} />

                    {/* Rutas privadas (con Navbar) */}
                    <Route path="/" element={
                        <PrivateRoute>
                            <Layout />
                        </PrivateRoute>
                    }>
                        <Route index element={<Dashboard />} />
                        <Route path="tests" element={<Tests />} />
                        <Route path="citas/agendar" element={<AgendarCita />} />
                        <Route path="citas" element={<MisCitas />} />
                        <Route path="mis-resultados" element={<MisResultados />} />
                        <Route path="recomendaciones" element={<MisRecomendaciones />} />
                        <Route path="certificados" element={<MisCertificados />} />
                    </Route>

                    {/* Ruta por defecto */}
                    <Route path="*" element={<Navigate to="/" replace />} />
                </Routes>
            </AuthProvider>
        </BrowserRouter>
    );
};

export default AppRoutes;
