import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { FileText, Clock, Users, AlertCircle } from 'lucide-react';
import testsService from '../api/tests.service';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import Loading from '../components/common/Loading';

const Tests = () => {
    const { user } = useAuth();
    const navigate = useNavigate();
    const [tests, setTests] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [iniciandoTest, setIniciandoTest] = useState(null);

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

    const iniciarTest = async (testId) => {
        try {
            setIniciandoTest(testId);
            setError('');

            console.log('Iniciando test:', { usuarioID: user.usuarioID, testId });

            const response = await testsService.iniciarTest(user.usuarioID, testId);

            console.log('Respuesta del servidor:', response);

            // Verificar diferentes estructuras de respuesta
            if (response && response.exitoso) {
                const respuestaID = response.datos?.respuestaID || response.datos?.RespuestaID;

                if (respuestaID) {
                    console.log('Navegando a realizar test con respuestaID:', respuestaID);
                    navigate(`/tests/realizar/${respuestaID}`, {
                        state: { testId: testId }
                    });
                } else {
                    console.error('No se encontró respuestaID en:', response.datos);
                    setError('Error: No se recibió el ID de respuesta');
                }
            } else if (response && response.datos) {
                // Intentar con la estructura directa
                const respuestaID = response.datos.respuestaID || response.datos.RespuestaID;
                if (respuestaID) {
                    navigate(`/tests/realizar/${respuestaID}`, {
                        state: { testId: testId }
                    });
                } else {
                    setError('Error al iniciar el test: Respuesta inválida');
                }
            } else {
                console.error('Respuesta no válida:', response);
                setError('Error al iniciar el test: Respuesta del servidor inválida');
            }
        } catch (err) {
            console.error('Error completo:', err);
            console.error('Respuesta del error:', err.response);
            setError(err.response?.data?.detalle || err.message || 'Error al iniciar el test');
        } finally {
            setIniciandoTest(null);
        }
    };

    if (loading) return <Loading fullScreen text="Cargando tests..." />;

    return (
        <div className="space-y-6">
            <div>
                <h1 className="text-3xl font-bold text-gray-800 mb-2">Tests Psicológicos</h1>
                <p className="text-gray-600">Realiza tests para evaluar tu bienestar emocional</p>
            </div>

            {error && (
                <div className="p-4 bg-red-50 border border-red-200 rounded-lg flex items-start gap-3">
                    <AlertCircle className="w-5 h-5 text-red-600 flex-shrink-0 mt-0.5" />
                    <div className="flex-1">
                        <p className="text-sm text-red-800">{error}</p>
                    </div>
                    <button
                        onClick={() => setError('')}
                        className="text-red-600 hover:text-red-800"
                    >
                        ✕
                    </button>
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

                            <Button
                                variant="primary"
                                fullWidth
                                onClick={() => iniciarTest(test.testID)}
                                loading={iniciandoTest === test.testID}
                            >
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