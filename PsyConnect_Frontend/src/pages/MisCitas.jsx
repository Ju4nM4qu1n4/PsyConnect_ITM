import React, { useState, useEffect } from 'react';
import { Calendar, Clock, MapPin, Trash2 } from 'lucide-react';
import citasService from '../api/citas.service';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import Loading from '../components/common/Loading';

const MisCitas = () => {
    const { user } = useAuth();
    const [citas, setCitas] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        cargarCitas();
    }, []);

    const cargarCitas = async () => {
        try {
            setLoading(true);
            const response = await citasService.obtenerCitasEstudiante(user.usuarioID);
            setCitas(response.datos || []);

        } catch (err) {
            console.error(err);
            setError('Error al cargar citas');
        } finally {
            setLoading(false);
        }
    };

    const cancelarCita = async (citaId) => {
        if (window.confirm('¿Estás seguro de que deseas cancelar esta cita?')) {
            try {
                await citasService.cancelarCita(citaId);
                cargarCitas();
            } catch (err) {
                console.error(err);
                setError('Error al cancelar cita');
            }
        }
    };

    if (loading) return <Loading fullScreen />;

    return (
        <div className="space-y-6">
            <div>
                <h1 className="text-3xl font-bold text-gray-800">Mis Citas</h1>
                <p className="text-gray-600 mt-2">Gestiona tus citas con los psicólogos</p>
            </div>

            {error && (
                <div className="p-4 bg-red-50 border border-red-200 rounded-lg text-red-800">
                    {error}
                </div>
            )}

            {citas.length === 0 ? (
                <Card padding="lg" className="text-center py-12">
                    <Calendar className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                    <p className="text-gray-600 mb-4">No tienes citas agendadas</p>
                    <Button variant="primary">Agendar Primera Cita</Button>
                </Card>
            ) : (
                <div className="space-y-4">
                    {citas.map((cita) => (
                        <Card key={cita.citaID} padding="lg">
                            <div className="flex justify-between items-start">
                                <div className="flex-1">
                                    <div className="flex items-center gap-2 mb-2">
                                        <h3 className="text-lg font-semibold text-gray-800">
                                            Cita con Psicólogo
                                        </h3>
                                        <span className="bg-blue-100 text-blue-800 text-xs px-2 py-1 rounded">
                                            {cita.estadoNombre}
                                        </span>
                                    </div>

                                    <div className="space-y-2 text-sm text-gray-600">
                                        <div className="flex items-center gap-2">
                                            <Calendar className="w-4 h-4" />
                                            {new Date(cita.fechaHora).toLocaleDateString()}
                                        </div>
                                        <div className="flex items-center gap-2">
                                            <Clock className="w-4 h-4" />
                                            {new Date(cita.fechaHora).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                                        </div>
                                        {cita.ubicacion && (
                                            <div className="flex items-center gap-2">
                                                <MapPin className="w-4 h-4" />
                                                {cita.ubicacion}
                                            </div>
                                        )}
                                    </div>
                                </div>

                                <button
                                    onClick={() => cancelarCita(cita.citaID)}
                                    className="text-red-600 hover:text-red-800 p-2"
                                >
                                    <Trash2 className="w-5 h-5" />
                                </button>
                            </div>
                        </Card>
                    ))}
                </div>
            )}
        </div>
    );
};

export default MisCitas;