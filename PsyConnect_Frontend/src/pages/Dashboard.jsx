import React from 'react';
import { Link } from 'react-router-dom';
import { FileText, Calendar, Award, Lightbulb, TrendingUp, Clock } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Button from '../components/common/Button';

const Dashboard = () => {
    const { user } = useAuth();

    const quickActions = [
        {
            title: 'Realizar Test',
            description: 'Completa tests psicológicos',
            icon: FileText,
            link: '/tests',
            color: 'bg-blue-500',
        },
        {
            title: 'Agendar Cita',
            description: 'Agenda con un psicólogo',
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

    const stats = [
        { label: 'Tests Realizados', value: '0', icon: FileText, color: 'text-blue-600' },
        { label: 'Citas Agendadas', value: '0', icon: Calendar, color: 'text-green-600' },
        { label: 'Recomendaciones', value: '0', icon: Lightbulb, color: 'text-yellow-600' },
        { label: 'Certificados', value: '0', icon: Award, color: 'text-purple-600' },
    ];

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

            {/* Estadísticas */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
                {stats.map((stat, index) => {
                    const Icon = stat.icon;
                    return (
                        <Card key={index} padding="md" hover>
                            <div className="flex items-center justify-between">
                                <div>
                                    <p className="text-sm text-gray-600 mb-1">{stat.label}</p>
                                    <p className="text-3xl font-bold text-gray-800">{stat.value}</p>
                                </div>
                                <div className={`p-3 rounded-full bg-gray-100 ${stat.color}`}>
                                    <Icon className="w-6 h-6" />
                                </div>
                            </div>
                        </Card>
                    );
                })}
            </div>

            {/* Acciones Rápidas */}
            <div>
                <h2 className="text-2xl font-bold text-gray-800 mb-4">Acciones Rápidas</h2>
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

            {/* Próximas Citas */}
            <div>
                <div className="flex justify-between items-center mb-4">
                    <h2 className="text-2xl font-bold text-gray-800">Próximas Citas</h2>
                    <Link to="/citas">
                        <Button variant="ghost" size="sm">Ver todas</Button>
                    </Link>
                </div>
                <Card padding="lg">
                    <div className="text-center py-8">
                        <Clock className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                        <p className="text-gray-600 mb-4">No tienes citas programadas</p>
                        <Link to="/citas/agendar">
                            <Button variant="primary">Agendar Cita</Button>
                        </Link>
                    </div>
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
                        <p className="text-gray-600">No tienes recomendaciones aún</p>
                    </div>
                </Card>
            </div>
        </div>
    );
};

export default Dashboard;