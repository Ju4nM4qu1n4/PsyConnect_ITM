import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import Loading from '../components/common/Loading';

const PrivateRoute = ({ children }) => {
    const { isAuthenticated, loading } = useAuth();
    const location = useLocation();

    // Mostrar loading mientras verifica autenticacion
    if (loading) {
        return <Loading fullScreen size="lg" text="Verificando sesion..." />;
    }

    // Si no esta autenticado, redirigir al login
    if (!isAuthenticated) {
        // Guardar la ruta a la que intentaba acceder
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    // Si esta autenticado, mostrar el componente
    return children;
};

export default PrivateRoute;