/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { Clock, AlertCircle, CheckCircle } from 'lucide-react';
import testsService from '../api/tests.service';
import resultadosService from '../api/resultados.service';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import Loading from '../components/common/Loading';

const RealizarTest = () => {
    const { respuestaId } = useParams();
    const navigate = useNavigate();
    const location = useLocation();
    const testId = location.state?.testId;

    const [test, setTest] = useState(null);
    const [respuestas, setRespuestas] = useState({});
    const [loading, setLoading] = useState(true);
    const [guardando, setGuardando] = useState(false);
    const [completando, setCompletando] = useState(false);
    const [error, setError] = useState('');
    const [preguntaActual, setPreguntaActual] = useState(0);
    const [tiempoInicio] = useState(new Date());

    useEffect(() => {
        if (testId) {
            cargarTest();
        } else {
            setError('ID de test no encontrado');
            setLoading(false);
        }
    }, [testId]);

    const cargarTest = async () => {
        try {
            setLoading(true);
            const response = await testsService.obtenerTest(testId);

            if (response.exitoso && response.datos) {
                setTest(response.datos);
                // Inicializar respuestas vacías
                const respuestasIniciales = {};
                if (response.datos.preguntas && response.datos.preguntas.length > 0) {
                    response.datos.preguntas.forEach(pregunta => {
                        respuestasIniciales[pregunta.preguntaID] = null;
                    });
                }
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

    const handleRespuestaChange = (preguntaId, valor) => {
        setRespuestas(prev => ({
            ...prev,
            [preguntaId]: parseInt(valor)
        }));
    };

    const preguntaAnterior = () => {
        if (preguntaActual > 0) {
            setPreguntaActual(prev => prev - 1);
        }
    };

    const preguntaSiguiente = async () => {
        const pregunta = test.preguntas[preguntaActual];

        if (respuestas[pregunta.preguntaID] === null) {
            setError('Por favor selecciona una respuesta antes de continuar');
            return;
        }

        setError('');

        // Guardar progreso cada 3 preguntas
        if ((preguntaActual + 1) % 3 === 0) {
            await guardarProgreso();
        }

        if (preguntaActual < test.preguntas.length - 1) {
            setPreguntaActual(prev => prev + 1);
        }
    };

    const guardarProgreso = async () => {
        try {
            setGuardando(true);

            // Convertir respuestas al formato esperado
            const respuestasArray = Object.entries(respuestas)
                .filter(([_, valor]) => valor !== null)
                .map(([preguntaId, valor]) => ({
                    preguntaId: parseInt(preguntaId),
                    opcionSeleccionada: valor
                }));

            if (respuestasArray.length > 0) {
                await testsService.guardarRespuestas(respuestaId, respuestasArray);
            }
        } catch (err) {
            console.error('Error al guardar progreso:', err);
            // No mostrar error al usuario para no interrumpir
        } finally {
            setGuardando(false);
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

            setCompletando(true);
            setError('');

            // Guardar todas las respuestas
            const respuestasArray = Object.entries(respuestas).map(([preguntaId, valor]) => ({
                preguntaId: parseInt(preguntaId),
                opcionSeleccionada: valor
            }));

            await testsService.guardarRespuestas(respuestaId, respuestasArray);

            // Completar el test
            await testsService.completarTest(respuestaId);

            // Evaluar y obtener resultado
            const resultadoResponse = await resultadosService.evaluarTest(respuestaId);

            if (resultadoResponse.exitoso && resultadoResponse.datos) {
                // Redirigir a la página de resultado
                navigate(`/tests/resultado/${resultadoResponse.datos.resultadoID}`, {
                    replace: true
                });
            } else {
                setError('Error al procesar los resultados');
            }
        } catch (err) {
            console.error(err);
            setError(err.response?.data?.detalle || 'Error al finalizar el test');
        } finally {
            setCompletando(false);
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

    if (loading) return <Loading fullScreen text="Cargando test..." />;

    if (!test || !test.preguntas || test.preguntas.length === 0) {
        return (
            <div className="flex items-center justify-center min-h-screen">
                <Card padding="lg" className="max-w-md text-center">
                    <AlertCircle className="w-16 h-16 text-red-500 mx-auto mb-4" />
                    <h2 className="text-2xl font-bold text-gray-800 mb-2">Error</h2>
                    <p className="text-gray-600 mb-4">{error || 'No se pudo cargar el test'}</p>
                    <Button onClick={() => navigate('/tests')}>Volver a Tests</Button>
                </Card>
            </div>
        );
    }

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
                            className="bg-primary-500 h-2 rounded-full transition-all duration-300"
                            style={{ width: `${progreso}%` }}
                        />
                    </div>
                </div>

                {guardando && (
                    <div className="flex items-center gap-2 text-sm text-blue-600">
                        <Loading size="sm" />
                        <span>Guardando progreso...</span>
                    </div>
                )}
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
                        {pregunta.textoPregunta}
                    </h2>

                    <div className="space-y-3">
                        {pregunta.opciones?.map((opcion) => (
                            <label
                                key={opcion.opcionID}
                                className={`
                                    flex items-center p-4 border-2 rounded-lg cursor-pointer
                                    transition-all duration-200
                                    ${respuestas[pregunta.preguntaID] === opcion.valor
                                        ? 'border-primary-500 bg-primary-50'
                                        : 'border-gray-300 hover:border-primary-300 bg-white'
                                    }
                                `}
                            >
                                <input
                                    type="radio"
                                    name={`pregunta-${pregunta.preguntaID}`}
                                    value={opcion.valor}
                                    checked={respuestas[pregunta.preguntaID] === opcion.valor}
                                    onChange={(e) => handleRespuestaChange(pregunta.preguntaID, e.target.value)}
                                    className="w-4 h-4 text-primary-500 focus:ring-primary-500"
                                />
                                <span className="ml-3 text-gray-800">{opcion.textoOpcion}</span>
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
                            loading={completando}
                            className="flex items-center gap-2"
                        >
                            <CheckCircle className="w-5 h-5" />
                            Finalizar Test
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

            {/* Advertencia si intenta salir */}
            <div className="mt-4 p-4 bg-yellow-50 border border-yellow-200 rounded-lg">
                <p className="text-sm text-yellow-800">
                    ⚠ No cierres esta ventana ni salgas de la página. Tu progreso se guarda automáticamente.
                </p>
            </div>
        </div>
    );
};

export default RealizarTest;