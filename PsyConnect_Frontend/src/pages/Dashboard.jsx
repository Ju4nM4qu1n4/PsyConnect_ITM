import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { FileText, Calendar, Award, Lightbulb, TrendingUp, Clock } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import citasService from '../api/citas.service'; // Ajusta la ruta según tu estructura

const Dashboard = () => {
    const { user } = useAuth();
    const [loading, setLoading] = useState(true);
    const [stats, setStats] = useState({
        testsRealizados: 0,
        citasAgendadas: 0,
        recomendaciones: 0,
        certificados: 0
    });
    const [proximasCitas, setProximasCitas] = useState([]);

    useEffect(() => {
        cargarDatos();
    }, []);

    const cargarDatos = async () => {
        try {
            setLoading(true);

            console.log('Usuario:', user);
            // Obtener citas del estudiante
            const citas = await citasService.obtenerMisCitas();

            // Obtener próximas citas
            const citasProximas = await citasService.obtenerProximasCitas();

            setStats({
                testsRealizados: 0, // Por ahora estático, después lo conectas
                citasAgendadas: citas?.datos?.length || 0,
                recomendaciones: 0, // Por ahora estático
                certificados: 0 // Por ahora estático
            });

            setProximasCitas(citasProximas?.datos || []);
        } catch (error) {
            console.error('Error al cargar datos:', error);
        } finally {
            setLoading(false);
        }
    };

    const quickActions = [
        {
            title: 'Realizar Test',
            description: 'Completa tests psicologicos',
            icon: FileText,
            link: '/tests',
            color: 'bg-blue-500',
        },
        {
            title: 'Agendar Cita',
            description: 'Agenda con un psicologo',
            icon: Calendar,
            link: '/citas/agendar',
            color: 'bg-green-500',
        },
        {
            title: 'Mis Resultados',
            description: 'Ve tus evaluaciones',
            icon: TrendingUp,
            link: '/mis-resultados',
            color: 'bg-purple-500',
        },
        {
            title: 'Certificados',
            description: 'Descarga certificados',
            icon: Award,
            link: '/certificados',
            color: 'bg-yellow-500',
        },
    ];

    const statsData = [
        { label: 'Tests Realizados', value: stats.testsRealizados, icon: FileText, color: 'text-blue-600' },
        { label: 'Citas Agendadas', value: stats.citasAgendadas, icon: Calendar, color: 'text-green-600' },
        { label: 'Recomendaciones', value: stats.recomendaciones, icon: Lightbulb, color: 'text-yellow-600' },
        { label: 'Certificados', value: stats.certificados, icon: Award, color: 'text-purple-600' },
    ];

    const formatearFecha = (fecha) => {
        return new Date(fecha).toLocaleDateString('es-CO', {
            weekday: 'long',
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    return (
        <div className="space-y-8">
            {/* Bienvenida */}
            <div className="bg-gradient-to-r from-primary-500 to-secondary-500 rounded-lg p-8 text-white">
                <h1 className="text-3xl font-bold mb-2">
                    Bienvenido/a, {user?.nombre}!
                </h1>
                <p className="text-primary-100">
                    Esta es tu plataforma de bienestar emocional
                </p>
            </div>

            {/* Estadisticas */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
                {statsData.map((stat, index) => {
                    const Icon = stat.icon;
                    return (
                        <Card key={index} padding="md" hover>
                            <div className="flex items-center justify-between">
                                <div>
                                    <p className="text-sm text-gray-600 mb-1">{stat.label}</p>
                                    <p className="text-3xl font-bold text-gray-800">
                                        {loading ? '...' : stat.value}
                                    </p>
                                </div>
                                <div className={`p-3 rounded-full bg-gray-100 ${stat.color}`}>
                                    <Icon className="w-6 h-6" />
                                </div>
                            </div>
                        </Card>
                    );
                })}
            </div>

            {/* Acciones Rapidas */}
            <div>
                <h2 className="text-2xl font-bold text-gray-800 mb-4">Acciones Rapidas</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
                    {quickActions.map((action, index) => {
                        const Icon = action.icon;
                        return (
                            <Link key={index} to={action.link}>
                                <Card padding="md" hover className="h-full">
                                    <div className="flex flex-col items-center text-center">
                                        <div className={`${action.color} p-4 rounded-full mb-4`}>
                                            <Icon className="w-8 h-8 text-white" />
                                        </div>
                                        <h3 className="font-semibold text-gray-800 mb-2">
                                            {action.title}
                                        </h3>
                                        <p className="text-sm text-gray-600">
                                            {action.description}
                                        </p>
                                    </div>
                                </Card>
                            </Link>
                        );
                    })}
                </div>
            </div>

            {/* Proximas Citas */}
            <div>
                <div className="flex justify-between items-center mb-4">
                    <h2 className="text-2xl font-bold text-gray-800">Proximas Citas</h2>
                    <Link to="/citas">
                        <Button variant="ghost" size="sm">Ver todas</Button>
                    </Link>
                </div>
                <Card padding="lg">
                    {loading ? (
                        <div className="text-center py-8">
                            <p className="text-gray-600">Cargando...</p>
                        </div>
                    ) : proximasCitas.length > 0 ? (
                        <div className="space-y-4">
                            {proximasCitas.map((cita) => (
                                <div key={cita.citaID} className="flex items-center justify-between p-4 border rounded-lg hover:bg-gray-50">
                                    <div className="flex items-center space-x-4">
                                        <div className="bg-primary-100 p-3 rounded-full">
                                            <Calendar className="w-6 h-6 text-primary-600" />
                                        </div>
                                        <div>
                                            <p className="font-semibold text-gray-800">
                                                {formatearFecha(cita.fechaHora)}
                                            </p>
                                            <p className="text-sm text-gray-600">
                                                {cita.modalidad} - {cita.ubicacion}
                                            </p>
                                        </div>
                                    </div>
                                    <span className="px-3 py-1 bg-green-100 text-green-800 rounded-full text-sm">
                                        {cita.estado}
                                    </span>
                                </div>
                            ))}
                        </div>
                    ) : (
                        <div className="text-center py-8">
                            <Clock className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                            <p className="text-gray-600 mb-4">No tienes citas programadas</p>
                            <Link to="/citas/agendar">
                                <Button variant="primary">Agendar Cita</Button>
                            </Link>
                        </div>
                    )}
                </Card>
            </div>

            {/* Recomendaciones Recientes */}
            <div>
                <div className="flex justify-between items-center mb-4">
                    <h2 className="text-2xl font-bold text-gray-800">Recomendaciones</h2>
                    <Link to="/recomendaciones">
                        <Button variant="ghost" size="sm">Ver todas</Button>
                    </Link>
                </div>
                <Card padding="lg">
                    <div className="text-center py-8">
                        <Lightbulb className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                        <p className="text-gray-600">No tienes recomendaciones aun</p>
                    </div>
                </Card>
            </div>
        </div>
    );
};

export default Dashboard;