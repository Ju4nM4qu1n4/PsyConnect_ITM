import React, { useState, useEffect } from 'react';
import { TrendingUp, AlertCircle } from 'lucide-react';
import resultadosService from '../api/resultados.service';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Loading from '../components/common/Loading';

const MisResultados = () => {
    const { user } = useAuth();
    const [resultados, setResultados] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        cargarResultados();
    }, []);

    const cargarResultados = async () => {
        try {
            const response = await resultadosService.obtenerResultadosEstudiante(user.usuarioID);
            setResultados(response.datos || []);
        } catch (err) {
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    const getNivelColor = (nivel) => {
        const colors = {
            'Bajo': 'bg-green-100 text-green-800',
            'Medio': 'bg-yellow-100 text-yellow-800',
            'Alto': 'bg-orange-100 text-orange-800',
            'Crítico': 'bg-red-100 text-red-800',
        };
        return colors[nivel] || 'bg-gray-100 text-gray-800';
    };

    if (loading) return <Loading fullScreen />;

    return (
        <div className="space-y-6">
            <div>
                <h1 className="text-3xl font-bold text-gray-800">Mis Resultados</h1>
                <p className="text-gray-600 mt-2">Visualiza tus evaluaciones psicológicas</p>
            </div>

            {resultados.length === 0 ? (
                <Card padding="lg" className="text-center py-12">
                    <TrendingUp className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                    <p className="text-gray-600">No tienes resultados disponibles aún</p>
                </Card>
            ) : (
                <div className="space-y-4">
                    {resultados.map((resultado) => (
                        <Card key={resultado.resultadoID} padding="lg">
                            <div className="flex justify-between items-start mb-4">
                                <h3 className="text-lg font-semibold text-gray-800">Evaluación</h3>
                                <span className={`px-3 py-1 rounded-full text-sm font-medium ${getNivelColor(resultado.nivel)}`}>
                                    {resultado.nivel}
                                </span>
                            </div>

                            <p className="text-gray-600 text-sm mb-4">{resultado.interpretacion}</p>

                            <div className="bg-blue-50 border border-blue-200 rounded-lg p-4 flex gap-3">
                                <AlertCircle className="w-5 h-5 text-blue-600 flex-shrink-0" />
                                <p className="text-sm text-blue-800">{resultado.recomendacion}</p>
                            </div>

                            <p className="text-xs text-gray-500 mt-4">
                                Evaluado: {new Date(resultado.fechaEvaluacion).toLocaleDateString()}
                            </p>
                        </Card>
                    ))}
                </div>
            )}
        </div>
    );
};

export default MisResultados;