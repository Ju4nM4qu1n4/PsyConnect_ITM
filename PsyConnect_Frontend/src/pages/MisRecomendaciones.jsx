import React, { useState, useEffect } from 'react';
import { Lightbulb, ExternalLink } from 'lucide-react';
import recomendacionesService from '../api/recomendaciones.service';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Loading from '../components/common/Loading';

const MisRecomendaciones = () => {
    const { user } = useAuth();
    const [recomendaciones, setRecomendaciones] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        cargarRecomendaciones();
    }, []);

    const cargarRecomendaciones = async () => {
        try {
            const response = await recomendacionesService.obtenerRecomendacionesEstudiante(user.usuarioID);
            setRecomendaciones(response.datos || []);
        } catch (err) {
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <Loading fullScreen />;

    return (
        <div className="space-y-6">
            <div>
                <h1 className="text-3xl font-bold text-gray-800">Mis Recomendaciones</h1>
                <p className="text-gray-600 mt-2">Recursos personalizados para tu bienestar</p>
            </div>

            {recomendaciones.length === 0 ? (
                <Card padding="lg" className="text-center py-12">
                    <Lightbulb className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                    <p className="text-gray-600">No tienes recomendaciones aún</p>
                </Card>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    {recomendaciones.map((rec) => (
                        <Card key={rec.recomendacionID} padding="lg" hover>
                            <div className="flex items-start justify-between mb-3">
                                <h3 className="text-lg font-semibold text-gray-800 flex-1">
                                    {rec.titulo}
                                </h3>
                                <span className="bg-purple-100 text-purple-800 text-xs px-2 py-1 rounded">
                                    {rec.tipoRecurso}
                                </span>
                            </div>

                            <p className="text-gray-600 text-sm mb-4">{rec.descripcion}</p>

                            {rec.url && (
                                <a
                                    href={rec.url}
                                    target="_blank"
                                    rel="noopener noreferrer"
                                    className="inline-flex items-center gap-2 text-primary-600 hover:text-primary-700 text-sm font-medium"
                                >
                                    Ver Recurso <ExternalLink className="w-4 h-4" />
                                </a>
                            )}
                        </Card>
                    ))}
                </div>
            )}
        </div>
    );
};

export default MisRecomendaciones;