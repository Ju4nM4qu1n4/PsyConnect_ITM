/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Clock, AlertCircle, CheckCircle, ClipboardList } from 'lucide-react';
import testService from '../api/tests.service';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import Loading from '../components/common/Loading';

const RealizarTest = () => {
    const { user } = useAuth();
    const navigate = useNavigate();

    const [test, setTest] = useState(null);
    const [respuestas, setRespuestas] = useState({});
    const [loading, setLoading] = useState(true);
    const [enviando, setEnviando] = useState(false);
    const [error, setError] = useState('');
    const [preguntaActual, setPreguntaActual] = useState(0);
    const [tiempoInicio] = useState(new Date());
    const [mostrarResultado, setMostrarResultado] = useState(false);
    const [resultado, setResultado] = useState(null);

    const TEST_ID = 1; // ID del primer test

    useEffect(() => {
        cargarTest();
    }, []);

    const cargarTest = async () => {
        try {
            setLoading(true);
            const response = await testService.obtenerTest(TEST_ID);

            if (response.exitoso && response.datos) {
                setTest(response.datos);
                // Inicializar respuestas vacías
                const respuestasIniciales = {};
                response.datos.preguntas?.forEach(pregunta => {
                    respuestasIniciales[pregunta.preguntaID] = null;
                });
                setRespuestas(respuestasIniciales);
            } else {
                setError('No se pudo cargar el test');
            }
        } catch (err) {
            console.error(err);
            setError('Error al cargar el test');
        } finally {
            setLoading(false);
        }
    };

    const handleRespuestaChange = (preguntaId, opcionId) => {
        setRespuestas(prev => ({
            ...prev,
            [preguntaId]: opcionId
        }));
        setError(''); // Limpiar error al seleccionar
    };

    const preguntaAnterior = () => {
        if (preguntaActual > 0) {
            setPreguntaActual(prev => prev - 1);
            setError('');
        }
    };

    const preguntaSiguiente = () => {
        const pregunta = test.preguntas[preguntaActual];

        if (respuestas[pregunta.preguntaID] === null) {
            setError('Por favor selecciona una respuesta antes de continuar');
            return;
        }

        setError('');

        if (preguntaActual < test.preguntas.length - 1) {
            setPreguntaActual(prev => prev + 1);
        }
    };

    const finalizarTest = async () => {
        try {
            // Verificar que todas las preguntas estén respondidas
            const todasRespondidas = Object.values(respuestas).every(r => r !== null);

            if (!todasRespondidas) {
                setError('Por favor responde todas las preguntas antes de finalizar');
                return;
            }

            setEnviando(true);
            setError('');

            // Preparar el request en el formato que espera el backend
            const request = {
                testID: TEST_ID,
                estudianteID: user.estudianteID,
                respuestas: Object.entries(respuestas).map(([preguntaId, opcionId]) => ({
                    preguntaID: parseInt(preguntaId),
                    opcionID: opcionId
                }))
            };

            // Enviar todas las respuestas al backend
            const response = await testService.enviarRespuestas(request);

            if (response.exitoso && response.datos) {
                setResultado(response.datos);
                setMostrarResultado(true);
            } else {
                setError('Error al procesar los resultados');
            }
        } catch (err) {
            console.error(err);
            setError(err.response?.data?.detalle || 'Error al finalizar el test');
        } finally {
            setEnviando(false);
        }
    };

    const calcularProgreso = () => {
        const respondidas = Object.values(respuestas).filter(r => r !== null).length;
        const total = test?.preguntas?.length || 1;
        return Math.round((respondidas / total) * 100);
    };

    const tiempoTranscurrido = () => {
        const ahora = new Date();
        const diff = Math.floor((ahora - tiempoInicio) / 1000 / 60);
        return diff;
    };

    const getNivelColor = (nivel) => {
        switch (nivel?.toLowerCase()) {
            case 'bajo':
                return 'text-green-800 bg-green-100 border-green-300';
            case 'moderado':
                return 'text-yellow-800 bg-yellow-100 border-yellow-300';
            case 'alto':
                return 'text-red-800 bg-red-100 border-red-300';
            default:
                return 'text-gray-800 bg-gray-100 border-gray-300';
        }
    };

    if (loading) return <Loading fullScreen text="Cargando test..." />;

    if (!test || !test.preguntas || test.preguntas.length === 0) {
        return (
            <div className="flex items-center justify-center min-h-screen">
                <Card padding="lg" className="max-w-md text-center">
                    <AlertCircle className="w-16 h-16 text-red-500 mx-auto mb-4" />
                    <h2 className="text-2xl font-bold text-gray-800 mb-2">Error</h2>
                    <p className="text-gray-600 mb-4">{error || 'No se pudo cargar el test'}</p>
                    <Button onClick={() => navigate('/dashboard')}>Volver al Dashboard</Button>
                </Card>
            </div>
        );
    }

    // Pantalla de resultados
    if (mostrarResultado && resultado) {
        return (
            <div className="max-w-4xl mx-auto">
                <Card padding="lg">
                    <div className="text-center mb-6">
                        <CheckCircle className="w-16 h-16 text-green-600 mx-auto mb-4" />
                        <h2 className="text-2xl font-bold text-gray-800 mb-2">
                            Test Completado
                        </h2>
                        <p className="text-gray-600">
                            Gracias por completar el test. Aquí están tus resultados.
                        </p>
                    </div>

                    <div className="space-y-4">
                        <div className="text-center">
                            <div className="inline-block">
                                <div className="text-sm text-gray-600 mb-2">Puntaje Total</div>
                                <div className="text-4xl font-bold text-blue-600">
                                    {resultado.puntajeTotal}
                                </div>
                                <div className="text-sm text-gray-500 mt-1">de 30 puntos</div>
                            </div>
                        </div>

                        <div className={`p-4 rounded-lg border-2 ${getNivelColor(resultado.nivel)}`}>
                            <div className="flex items-center gap-2 mb-2">
                                <AlertCircle className="w-5 h-5" />
                                <span className="font-semibold text-lg">
                                    Nivel: {resultado.nivel}
                                </span>
                            </div>
                        </div>

                        <div className="bg-gray-50 p-6 rounded-lg">
                            <h3 className="font-semibold text-gray-800 mb-3">Interpretación</h3>
                            <p className="text-gray-700 mb-4">{resultado.interpretacion}</p>

                            <h3 className="font-semibold text-gray-800 mb-3">Recomendaciones</h3>
                            <p className="text-gray-700">{resultado.recomendacion}</p>
                        </div>

                        <div className="flex gap-4 justify-center pt-4">
                            <Button
                                variant="outline"
                                onClick={() => navigate('/dashboard')}
                            >
                                Volver al Dashboard
                            </Button>
                            <Button
                                variant="primary"
                                onClick={() => navigate('/agendar-cita')}
                            >
                                Agendar Cita con Psicólogo
                            </Button>
                        </div>
                    </div>
                </Card>
            </div>
        );
    }

    // Pantalla del test (pregunta por pregunta)
    const pregunta = test.preguntas[preguntaActual];
    const progreso = calcularProgreso();
    const esUltimaPregunta = preguntaActual === test.preguntas.length - 1;

    return (
        <div className="max-w-4xl mx-auto">
            {/* Header con información del test */}
            <div className="mb-6">
                <div className="flex justify-between items-center mb-4">
                    <div>
                        <h1 className="text-2xl font-bold text-gray-800">{test.nombreTest}</h1>
                        <p className="text-gray-600 text-sm mt-1">{test.descripcion}</p>
                    </div>
                    <div className="flex items-center gap-2 text-gray-600">
                        <Clock className="w-5 h-5" />
                        <span className="font-medium">{tiempoTranscurrido()} min</span>
                    </div>
                </div>

                {/* Barra de progreso */}
                <div className="mb-2">
                    <div className="flex justify-between text-sm text-gray-600 mb-1">
                        <span>Pregunta {preguntaActual + 1} de {test.preguntas.length}</span>
                        <span>{progreso}% completado</span>
                    </div>
                    <div className="w-full bg-gray-200 rounded-full h-2">
                        <div
                            className="bg-blue-500 h-2 rounded-full transition-all duration-300"
                            style={{ width: `${progreso}%` }}
                        />
                    </div>
                </div>
            </div>

            {/* Pregunta actual */}
            <Card padding="lg">
                {error && (
                    <div className="mb-6 p-4 bg-red-50 border border-red-200 rounded-lg flex items-start gap-3">
                        <AlertCircle className="w-5 h-5 text-red-600 flex-shrink-0 mt-0.5" />
                        <p className="text-sm text-red-800">{error}</p>
                    </div>
                )}

                <div className="mb-8">
                    <h2 className="text-xl font-semibold text-gray-800 mb-6">
                        {pregunta.numero}. {pregunta.texto}
                    </h2>

                    <div className="space-y-3">
                        {pregunta.opciones?.map((opcion) => (
                            <label
                                key={opcion.opcionID}
                                className={`
                                    flex items-center p-4 border-2 rounded-lg cursor-pointer
                                    transition-all duration-200
                                    ${respuestas[pregunta.preguntaID] === opcion.opcionID
                                        ? 'border-blue-500 bg-blue-50'
                                        : 'border-gray-300 hover:border-blue-300 bg-white'
                                    }
                                `}
                            >
                                <input
                                    type="radio"
                                    name={`pregunta-${pregunta.preguntaID}`}
                                    checked={respuestas[pregunta.preguntaID] === opcion.opcionID}
                                    onChange={() => handleRespuestaChange(pregunta.preguntaID, opcion.opcionID)}
                                    className="w-4 h-4 text-blue-500 focus:ring-blue-500"
                                />
                                <span className="ml-3 text-gray-800">{opcion.texto}</span>
                            </label>
                        ))}
                    </div>
                </div>

                {/* Botones de navegación */}
                <div className="flex justify-between gap-4">
                    <Button
                        variant="outline"
                        onClick={preguntaAnterior}
                        disabled={preguntaActual === 0}
                    >
                        ← Anterior
                    </Button>

                    {esUltimaPregunta ? (
                        <Button
                            variant="primary"
                            onClick={finalizarTest}
                            disabled={enviando}
                            className="flex items-center gap-2"
                        >
                            {enviando ? (
                                <>
                                    <div className="w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin" />
                                    Procesando...
                                </>
                            ) : (
                                <>
                                    <CheckCircle className="w-5 h-5" />
                                    Finalizar Test
                                </>
                            )}
                        </Button>
                    ) : (
                        <Button
                            variant="primary"
                            onClick={preguntaSiguiente}
                        >
                            Siguiente →
                        </Button>
                    )}
                </div>
            </Card>

            {/* Advertencia */}
            <div className="mt-4 p-4 bg-yellow-50 border border-yellow-200 rounded-lg">
                <p className="text-sm text-yellow-800">
                    ⚠️ No cierres esta ventana. Todas tus respuestas se enviarán al finalizar el test.
                </p>
            </div>
        </div>
    );
};

export default RealizarTest;