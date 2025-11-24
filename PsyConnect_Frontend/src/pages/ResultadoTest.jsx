import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { CheckCircle, AlertCircle, Download, Home, FileText } from 'lucide-react';
import resultadosService from '../api/resultados.service';
import certificadosService from '../api/certificados.service';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import Loading from '../components/common/Loading';

const ResultadoTest = () => {
    const { resultadoId } = useParams();
    const navigate = useNavigate();

    const [resultado, setResultado] = useState(null);
    const [loading, setLoading] = useState(true);
    const [generandoCertificado, setGenerandoCertificado] = useState(false);
    const [error, setError] = useState('');

    useEffect(() => {
        cargarResultado();
    }, [resultadoId]);

    const cargarResultado = async () => {
        try {
            setLoading(true);
            const response = await resultadosService.obtenerResultado(resultadoId);

            if (response.exitoso && response.datos) {
                setResultado(response.datos);
            } else {
                setError('No se pudo cargar el resultado');
            }
        } catch (err) {
            console.error(err);
            setError('Error al cargar el resultado');
        } finally {
            setLoading(false);
        }
    };

    const generarCertificado = async () => {
        try {
            setGenerandoCertificado(true);
            // Necesitamos el respuestaId del resultado
            const response = await certificadosService.generarCertificadoTest(resultado.respuestaID);

            if (response.exitoso) {
                alert('Certificado generado exitosamente. Ve a la sección de Certificados para descargarlo.');
                navigate('/certificados');
            }
        } catch (err) {
            console.error(err);
            alert('Error al generar certificado');
        } finally {
            setGenerandoCertificado(false);
        }
    };

    const getNivelColor = (nivel) => {
        const colors = {
            'Bajo': { bg: 'bg-green-100', text: 'text-green-800', border: 'border-green-500' },
            'Medio': { bg: 'bg-yellow-100', text: 'text-yellow-800', border: 'border-yellow-500' },
            'Alto': { bg: 'bg-orange-100', text: 'text-orange-800', border: 'border-orange-500' },
            'Crítico': { bg: 'bg-red-100', text: 'text-red-800', border: 'border-red-500' },
        };
        return colors[nivel] || { bg: 'bg-gray-100', text: 'text-gray-800', border: 'border-gray-500' };
    };

    const getNivelIcono = (nivel) => {
        if (nivel === 'Bajo') {
            return <CheckCircle className="w-16 h-16 text-green-500" />;
        }
        return <AlertCircle className="w-16 h-16 text-orange-500" />;
    };

    if (loading) return <Loading fullScreen text="Cargando resultados..." />;

    if (error || !resultado) {
        return (
            <div className="flex items-center justify-center min-h-screen">
                <Card padding="lg" className="max-w-md text-center">
                    <AlertCircle className="w-16 h-16 text-red-500 mx-auto mb-4" />
                    <h2 className="text-2xl font-bold text-gray-800 mb-2">Error</h2>
                    <p className="text-gray-600 mb-4">{error || 'No se pudo cargar el resultado'}</p>
                    <Button onClick={() => navigate('/mis-resultados')}>Ver Mis Resultados</Button>
                </Card>
            </div>
        );
    }

    const colorScheme = getNivelColor(resultado.nivel);

    return (
        <div className="max-w-4xl mx-auto space-y-6">
            {/* Encabezado de éxito */}
            <div className="text-center py-8">
                <div className="mb-4 flex justify-center">
                    {getNivelIcono(resultado.nivel)}
                </div>
                <h1 className="text-3xl font-bold text-gray-800 mb-2">
                    Test Completado
                </h1>
                <p className="text-gray-600">
                    Aquí están los resultados de tu evaluación
                </p>
            </div>

            {/* Tarjeta principal de resultado */}
            <Card padding="lg">
                <div className="text-center mb-6">
                    <h2 className="text-2xl font-bold text-gray-800 mb-4">
                        Tu Nivel: {resultado.nivel}
                    </h2>

                    {/* Badge grande del nivel */}
                    <div className={`inline-block px-8 py-4 rounded-lg border-2 ${colorScheme.bg} ${colorScheme.text} ${colorScheme.border}`}>
                        <div className="text-4xl font-bold">{resultado.puntajeTotal}</div>
                        <div className="text-sm">puntos</div>
                    </div>
                </div>

                <div className="border-t pt-6">
                    <h3 className="text-lg font-semibold text-gray-800 mb-3">
                        Interpretación
                    </h3>
                    <p className="text-gray-700 leading-relaxed mb-6">
                        {resultado.interpretacion}
                    </p>

                    <div className={`${colorScheme.bg} border ${colorScheme.border} rounded-lg p-6`}>
                        <div className="flex items-start gap-3">
                            <AlertCircle className={`w-6 h-6 ${colorScheme.text} flex-shrink-0 mt-1`} />
                            <div>
                                <h4 className={`font-semibold ${colorScheme.text} mb-2`}>
                                    Recomendación
                                </h4>
                                <p className={`${colorScheme.text} text-sm leading-relaxed`}>
                                    {resultado.recomendacion}
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </Card>

            {/* Información adicional */}
            <Card padding="lg">
                <h3 className="text-lg font-semibold text-gray-800 mb-4">
                    Información del Test
                </h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4 text-sm">
                    <div className="flex justify-between py-2 border-b">
                        <span className="text-gray-600">Fecha de evaluación:</span>
                        <span className="font-medium text-gray-800">
                            {new Date(resultado.fechaEvaluacion).toLocaleDateString('es-CO', {
                                year: 'numeric',
                                month: 'long',
                                day: 'numeric'
                            })}
                        </span>
                    </div>
                    <div className="flex justify-between py-2 border-b">
                        <span className="text-gray-600">Hora:</span>
                        <span className="font-medium text-gray-800">
                            {new Date(resultado.fechaEvaluacion).toLocaleTimeString('es-CO', {
                                hour: '2-digit',
                                minute: '2-digit'
                            })}
                        </span>
                    </div>
                    <div className="flex justify-between py-2 border-b">
                        <span className="text-gray-600">Puntaje total:</span>
                        <span className="font-medium text-gray-800">
                            {resultado.puntajeTotal} puntos
                        </span>
                    </div>
                    <div className="flex justify-between py-2 border-b">
                        <span className="text-gray-600">Nivel:</span>
                        <span className={`font-medium px-2 py-1 rounded ${colorScheme.bg} ${colorScheme.text}`}>
                            {resultado.nivel}
                        </span>
                    </div>
                </div>
            </Card>

            {/* ¿Qué sigue? */}
            <Card padding="lg" className="bg-blue-50 border border-blue-200">
                <h3 className="text-lg font-semibold text-gray-800 mb-3">
                    ¿Qué hacer ahora?
                </h3>
                <ul className="space-y-2 text-sm text-gray-700 mb-4">
                    <li className="flex items-start gap-2">
                        <span className="text-blue-600 mt-1">•</span>
                        <span>Descarga tu certificado de evaluación para tus registros</span>
                    </li>
                    <li className="flex items-start gap-2">
                        <span className="text-blue-600 mt-1">•</span>
                        <span>Si tu nivel es medio o alto, considera agendar una cita con un psicólogo</span>
                    </li>
                    <li className="flex items-start gap-2">
                        <span className="text-blue-600 mt-1">•</span>
                        <span>Revisa tus recomendaciones personalizadas en tu perfil</span>
                    </li>
                    <li className="flex items-start gap-2">
                        <span className="text-blue-600 mt-1">•</span>
                        <span>Puedes repetir el test en cualquier momento para ver tu progreso</span>
                    </li>
                </ul>
            </Card>

            {/* Acciones */}
            <div className="flex flex-col sm:flex-row gap-4">
                <Button
                    variant="primary"
                    onClick={generarCertificado}
                    loading={generandoCertificado}
                    className="flex items-center justify-center gap-2"
                >
                    <Download className="w-5 h-5" />
                    Generar Certificado
                </Button>

                <Button
                    variant="outline"
                    onClick={() => navigate('/citas/agendar')}
                    className="flex items-center justify-center gap-2"
                >
                    <FileText className="w-5 h-5" />
                    Agendar Cita
                </Button>

                <Button
                    variant="ghost"
                    onClick={() => navigate('/')}
                    className="flex items-center justify-center gap-2"
                >
                    <Home className="w-5 h-5" />
                    Ir al Inicio
                </Button>
            </div>

            {/* Nota de confidencialidad */}
            <div className="text-center text-sm text-gray-500 py-4">
                <p>
                    Tus resultados son confidenciales y están protegidos por nuestra política de privacidad.
                </p>
            </div>
        </div>
    );
};

export default ResultadoTest;