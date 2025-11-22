import React, { useState, useEffect } from 'react';
import { Award, Download } from 'lucide-react';
import certificadosService from '../api/certificados.service';
import { useAuth } from '../hooks/useAuth';
import Card from '../components/common/Card';
import Button from '../components/common/Button';
import Loading from '../components/common/Loading';

const MisCertificados = () => {
    const { user } = useAuth();
    const [certificados, setCertificados] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        cargarCertificados();
    }, []);

    const cargarCertificados = async () => {
        try {
            const response = await certificadosService.obtenerCertificadosEstudiante(user.usuarioID);
            setCertificados(response.datos || []);
        } catch (err) {
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    const descargarCertificado = async (certificadoId) => {
        try {
            await certificadosService.marcarComoDescargado(certificadoId);
            // Aquí irían los detalles para descargar el PDF
            alert('Descargando certificado...');
        } catch (err) {
            console.error(err);
            alert('Error al descargar certificado');
        }
    };

    if (loading) return <Loading fullScreen />;

    return (
        <div className="space-y-6">
            <div>
                <h1 className="text-3xl font-bold text-gray-800">Mis Certificados</h1>
                <p className="text-gray-600 mt-2">Descarga tus certificados de evaluación</p>
            </div>

            {certificados.length === 0 ? (
                <Card padding="lg" className="text-center py-12">
                    <Award className="w-16 h-16 text-gray-400 mx-auto mb-4" />
                    <p className="text-gray-600">No tienes certificados disponibles</p>
                </Card>
            ) : (
                <div className="space-y-4">
                    {certificados.map((cert) => (
                        <Card key={cert.certificadoID} padding="lg">
                            <div className="flex justify-between items-start">
                                <div>
                                    <h3 className="text-lg font-semibold text-gray-800 mb-2">
                                        Certificado de {cert.tipoCertificado}
                                    </h3>
                                    <p className="text-sm text-gray-600">
                                        Generado: {new Date(cert.fechaGeneracion).toLocaleDateString()}
                                    </p>
                                    {cert.fechaDescarga && (
                                        <p className="text-sm text-gray-600">
                                            Descargado: {new Date(cert.fechaDescarga).toLocaleDateString()}
                                        </p>
                                    )}
                                </div>

                                <Button
                                    variant="primary"
                                    size="sm"
                                    onClick={() => descargarCertificado(cert.certificadoID)}
                                    className="flex items-center gap-2"
                                >
                                    <Download className="w-4 h-4" />
                                    Descargar
                                </Button>
                            </div>
                        </Card>
                    ))}
                </div>
            )}
        </div>
    );
};

export default MisCertificados;