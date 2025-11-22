import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import Loading from '../components/common/Loading';

const PrivateRoute = ({ children }) => {
    const { isAuthenticated, loading } = useAuth();
    const location = useLocation();

    // Mostrar loading mientras verifica autenticación
    if (loading) {
        return <Loading fullScreen size="lg" text="Verificando sesión..." />;
    }

    // Si no está autenticado, redirigir al login
    if (!isAuthenticated) {
        // Guardar la ruta a la que intentaba acceder
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    // Si está autenticado, mostrar el componente
    return children;
};

export default PrivateRoute;