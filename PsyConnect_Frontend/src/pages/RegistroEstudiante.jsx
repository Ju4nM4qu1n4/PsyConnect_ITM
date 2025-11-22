import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { User, Mail, Lock, Phone, BookOpen, Hash, MapPin, Users } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';
import Button from '../components/common/Button';
import Input from '../components/common/Input';
import Card from '../components/common/Card';

const RegistroEstudiante = () => {
    const navigate = useNavigate();
    const { registrarEstudiante } = useAuth();

    const [formData, setFormData] = useState({
        nombre: '',
        apellido: '',
        email: '',
        password: '',
        confirmPassword: '',
        telefono: '',
        matricula: '',
        carrera: '',
        semestre: '',
        genero: '',
        direccion: '',
    });

    const [errors, setErrors] = useState({});
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
        // Limpiar error del campo al escribir
        if (errors[e.target.name]) {
            setErrors({ ...errors, [e.target.name]: '' });
        }
    };

    const validateForm = () => {
        const newErrors = {};

        if (!formData.nombre.trim()) newErrors.nombre = 'El nombre es requerido';
        if (!formData.apellido.trim()) newErrors.apellido = 'El apellido es requerido';
        if (!formData.email.trim()) newErrors.email = 'El email es requerido';
        if (!formData.password) newErrors.password = 'La contraseña es requerida';
        if (formData.password.length < 8) newErrors.password = 'La contraseña debe tener al menos 8 caracteres';
        if (formData.password !== formData.confirmPassword) {
            newErrors.confirmPassword = 'Las contraseñas no coinciden';
        }
        if (!formData.matricula.trim()) newErrors.matricula = 'La matrícula es requerida';
        if (!formData.carrera.trim()) newErrors.carrera = 'La carrera es requerida';
        if (!formData.semestre) newErrors.semestre = 'El semestre es requerido';
        if (!formData.genero) newErrors.genero = 'El género es requerido';

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validateForm()) {
            return;
        }

        setLoading(true);

        try {
            const result = await registrarEstudiante(formData);

            if (result.success) {
                setSuccess(true);
                setTimeout(() => {
                    navigate('/login');
                }, 2000);
            } else {
                setErrors({ submit: result.error || 'Error al registrar usuario' });
            }
        } catch {
            setErrors({ submit: 'Error al conectar con el servidor' });
        } finally {
            setLoading(false);
        }
    };

    if (success) {
        return (
            <div className="min-h-screen bg-gradient-to-br from-primary-50 to-secondary-50 flex items-center justify-center p-4">
                <Card padding="lg" className="max-w-md text-center">
                    <div className="text-6xl mb-4 text-green-500">✓</div>
                    <h2 className="text-2xl font-bold text-gray-800 mb-2">
                        ¡Registro Exitoso!
                    </h2>
                    <p className="text-gray-600 mb-4">
                        Tu cuenta ha sido creada correctamente.
                    </p>
                    <p className="text-sm text-gray-500">
                        Redirigiendo al login...
                    </p>
                </Card>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gradient-to-br from-primary-50 to-secondary-50 py-8 px-4">
            <div className="max-w-3xl mx-auto">
                {/* Header */}
                <div className="text-center mb-8">
                    <Link to="/login" className="inline-block mb-4">
                        <h1 className="text-3xl font-bold text-primary-600">
                            PsyConnect
                        </h1>
                    </Link>
                    <h2 className="text-2xl font-bold text-gray-800">
                        Registro de Estudiante
                    </h2>
                    <p className="text-gray-600 mt-2">
                        Completa el formulario para crear tu cuenta
                    </p>
                </div>

                {/* Formulario */}
                <Card padding="lg">
                    {errors.submit && (
                        <div className="mb-6 p-4 bg-red-50 border border-red-200 rounded-lg">
                            <p className="text-sm text-red-800">{errors.submit}</p>
                        </div>
                    )}

                    <form onSubmit={handleSubmit}>
                        {/* Información Personal */}
                        <div className="mb-6">
                            <h3 className="text-lg font-semibold text-gray-700 mb-4">
                                Información Personal
                            </h3>
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <Input
                                    label="Nombre"
                                    name="nombre"
                                    value={formData.nombre}
                                    onChange={handleChange}
                                    icon={User}
                                    error={errors.nombre}
                                    required
                                />
                                <Input
                                    label="Apellido"
                                    name="apellido"
                                    value={formData.apellido}
                                    onChange={handleChange}
                                    icon={User}
                                    error={errors.apellido}
                                    required
                                />
                            </div>
                        </div>

                        {/* Contacto */}
                        <div className="mb-6">
                            <h3 className="text-lg font-semibold text-gray-700 mb-4">
                                Contacto
                            </h3>
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <Input
                                    label="Correo Electrónico"
                                    type="email"
                                    name="email"
                                    value={formData.email}
                                    onChange={handleChange}
                                    icon={Mail}
                                    error={errors.email}
                                    required
                                />
                                <Input
                                    label="Teléfono"
                                    type="tel"
                                    name="telefono"
                                    value={formData.telefono}
                                    onChange={handleChange}
                                    placeholder="3001234567"
                                    icon={Phone}
                                />
                            </div>
                        </div>

                        {/* Información Académica */}
                        <div className="mb-6">
                            <h3 className="text-lg font-semibold text-gray-700 mb-4">
                                Información Académica
                            </h3>
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <Input
                                    label="Matrícula"
                                    name="matricula"
                                    value={formData.matricula}
                                    onChange={handleChange}
                                    icon={Hash}
                                    error={errors.matricula}
                                    required
                                />
                                <Input
                                    label="Carrera"
                                    name="carrera"
                                    value={formData.carrera}
                                    onChange={handleChange}
                                    placeholder="Ej: Ingeniería de Sistemas"
                                    icon={BookOpen}
                                    error={errors.carrera}
                                    required
                                />
                                <div>
                                    <label className="block text-sm font-medium text-gray-700 mb-1">
                                        Semestre <span className="text-red-500">*</span>
                                    </label>
                                    <select
                                        name="semestre"
                                        value={formData.semestre}
                                        onChange={handleChange}
                                        className="block w-full rounded-lg border border-gray-300 px-3 py-2 focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
                                        required
                                    >
                                        <option value="">Seleccionar...</option>
                                        {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(sem => (
                                            <option key={sem} value={sem}>{sem}</option>
                                        ))}
                                    </select>
                                    {errors.semestre && (
                                        <p className="mt-1 text-sm text-red-600">{errors.semestre}</p>
                                    )}
                                </div>
                                <div>
                                    <label className="block text-sm font-medium text-gray-700 mb-1">
                                        Género <span className="text-red-500">*</span>
                                    </label>
                                    <select
                                        name="genero"
                                        value={formData.genero}
                                        onChange={handleChange}
                                        className="block w-full rounded-lg border border-gray-300 px-3 py-2 focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
                                        required
                                    >
                                        <option value="">Seleccionar...</option>
                                        <option value="Masculino">Masculino</option>
                                        <option value="Femenino">Femenino</option>
                                        <option value="Otro">Otro</option>
                                        <option value="Prefiero no decirlo">Prefiero no decirlo</option>
                                    </select>
                                    {errors.genero && (
                                        <p className="mt-1 text-sm text-red-600">{errors.genero}</p>
                                    )}
                                </div>
                            </div>
                            <div className="mt-4">
                                <Input
                                    label="Dirección"
                                    name="direccion"
                                    value={formData.direccion}
                                    onChange={handleChange}
                                    icon={MapPin}
                                />
                            </div>
                        </div>

                        {/* Seguridad */}
                        <div className="mb-6">
                            <h3 className="text-lg font-semibold text-gray-700 mb-4">
                                Seguridad
                            </h3>
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <Input
                                    label="Contraseña"
                                    type="password"
                                    name="password"
                                    value={formData.password}
                                    onChange={handleChange}
                                    placeholder="Mínimo 8 caracteres"
                                    icon={Lock}
                                    error={errors.password}
                                    required
                                />
                                <Input
                                    label="Confirmar Contraseña"
                                    type="password"
                                    name="confirmPassword"
                                    value={formData.confirmPassword}
                                    onChange={handleChange}
                                    icon={Lock}
                                    error={errors.confirmPassword}
                                    required
                                />
                            </div>
                        </div>

                        {/* Botones */}
                        <div className="flex flex-col sm:flex-row gap-4">
                            <Button
                                type="submit"
                                variant="primary"
                                size="lg"
                                fullWidth
                                loading={loading}
                            >
                                Crear Cuenta
                            </Button>
                            <Link to="/login" className="w-full">
                                <Button variant="outline" size="lg" fullWidth>
                                    Ya tengo cuenta
                                </Button>
                            </Link>
                        </div>
                    </form>
                </Card>
            </div>
        </div>
    );
};

export default RegistroEstudiante;