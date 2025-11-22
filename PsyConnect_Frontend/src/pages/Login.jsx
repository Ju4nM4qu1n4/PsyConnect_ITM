import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Mail, Lock, AlertCircle } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';
import Button from '../components/common/Button';
import Input from '../components/common/Input';
import Card from '../components/common/Card';

const Login = () => {
    const navigate = useNavigate();
    const { login, isAuthenticated } = useAuth();

    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });

    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    // Si ya está autenticado, redirigir al dashboard
    React.useEffect(() => {
        if (isAuthenticated) {
            navigate('/');
        }
    }, [isAuthenticated, navigate]);

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
        setError(''); // Limpiar error al escribir
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setLoading(true);

        // Validación básica
        if (!formData.email || !formData.password) {
            setError('Por favor completa todos los campos');
            setLoading(false);
            return;
        }

        try {
            const result = await login(formData.email, formData.password);

            if (result.success) {
                navigate('/');
            } else {
                setError(result.error || 'Credenciales inválidas');
            }
        } catch{
            setError('Error al conectar con el servidor');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-primary-50 to-secondary-50 flex items-center justify-center p-4">
            <div className="w-full max-w-md">
                {/* Logo y Título */}
                <div className="text-center mb-8">
                    <h1 className="text-4xl font-bold text-primary-600 mb-2">
                        PsyConnect
                    </h1>
                    <p className="text-gray-600">
                        Plataforma de Salud Mental Estudiantil
                    </p>
                </div>

                {/* Card de Login */}
                <Card padding="lg">
                    <h2 className="text-2xl font-bold text-gray-800 mb-6 text-center">
                        Iniciar Sesión
                    </h2>

                    {/* Mensaje de Error */}
                    {error && (
                        <div className="mb-4 p-4 bg-red-50 border border-red-200 rounded-lg flex items-start gap-3">
                            <AlertCircle className="w-5 h-5 text-red-600 flex-shrink-0 mt-0.5" />
                            <p className="text-sm text-red-800">{error}</p>
                        </div>
                    )}

                    {/* Formulario */}
                    <form onSubmit={handleSubmit}>
                        <Input
                            label="Correo Electrónico"
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            placeholder="tu@email.com"
                            icon={Mail}
                            required
                        />

                        <Input
                            label="Contraseña"
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            placeholder="••••••••"
                            icon={Lock}
                            required
                        />

                        <Button
                            type="submit"
                            variant="primary"
                            size="lg"
                            fullWidth
                            loading={loading}
                            className="mt-6"
                        >
                            Iniciar Sesión
                        </Button>
                    </form>

                    {/* Línea divisoria */}
                    <div className="relative my-6">
                        <div className="absolute inset-0 flex items-center">
                            <div className="w-full border-t border-gray-300"></div>
                        </div>
                        <div className="relative flex justify-center text-sm">
                            <span className="px-2 bg-white text-gray-500">¿No tienes cuenta?</span>
                        </div>
                    </div>

                    {/* Link a Registro */}
                    <Link to="/registro">
                        <Button variant="outline" size="lg" fullWidth>
                            Registrarse como Estudiante
                        </Button>
                    </Link>
                </Card>

                {/* Footer */}
                <p className="text-center text-sm text-gray-600 mt-6">
                    © 2025 PsyConnect ITM - Todos los derechos reservados
                </p>
            </div>
        </div>
    );
};

export default Login;