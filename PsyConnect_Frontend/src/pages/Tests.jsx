import React, { useState, useEffect } from 'react';
import { FileText, Clock, Users } from 'lucide-react';
import testsService from '../api/tests.service';
//import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import Loading from '../components/common/Loading';

const Tests = () => {
    //const { user } = useAuth();
    const [tests, setTests] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        cargarTests();
    }, []);

    const cargarTests = async () => {
        try {
            setLoading(true);
            const response = await testsService.obtenerTestsActivos();
            setTests(response.datos || []);
        } catch (err) {
            setError('Error al cargar los tests');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <Loading fullScreen />;

    return (
        <div className="space-y-6">
            <div>
                <h1 className="text-3xl font-bold text-gray-800 mb-2">Tests Psicológicos</h1>
                <p className="text-gray-600">Realiza tests para evaluar tu bienestar emocional</p>
            </div>

            {error && (
                <div className="p-4 bg-red-50 border border-red-200 rounded-lg text-red-800">
                    {error}
                </div>
            )}

            {tests.length === 0 ? (
                <Card padding="lg" className="text-center py-12">
                    <FileText className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                    <p className="text-gray-600">No hay tests disponibles en este momento</p>
                </Card>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    {tests.map((test) => (
                        <Card key={test.testID} padding="lg" hover>
                            <div className="flex justify-between items-start mb-4">
                                <h3 className="text-lg font-semibold text-gray-800 flex-1">
                                    {test.nombreTest}
                                </h3>
                                <span className="bg-blue-100 text-blue-800 text-xs px-2 py-1 rounded">
                                    {test.tipoTestNombre}
                                </span>
                            </div>

                            <p className="text-gray-600 text-sm mb-4">{test.descripcion}</p>

                            <div className="flex items-center gap-4 text-sm text-gray-600 mb-4">
                                <div className="flex items-center gap-2">
                                    <FileText className="w-4 h-4" />
                                    {test.cantidadPreguntas} preguntas
                                </div>
                                <div className="flex items-center gap-2">
                                    <Clock className="w-4 h-4" />
                                    {test.tiempoEstimado} min
                                </div>
                                <div className="flex items-center gap-2">
                                    <Users className="w-4 h-4" />
                                    {test.modalidadNombre}
                                </div>
                            </div>

                            <Button variant="primary" fullWidth>
                                Realizar Test
                            </Button>
                        </Card>
                    ))}
                </div>
            )}
        </div>
    );
};

export default Tests;