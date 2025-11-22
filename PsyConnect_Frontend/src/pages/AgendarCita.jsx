import React, { useState } from 'react';
import { Calendar, Clock, MapPin, AlertCircle } from 'lucide-react';
import citasService from '../api/citas.service';
import { useAuth } from '../hooks/useAuth';
import Button from '../components/common/Button';
import Input from '../components/common/Input';
import Card from '../components/common/Card';
import { useNavigate } from 'react-router-dom';

const AgendarCita = () => {
    const navigate = useNavigate();
    const { user } = useAuth();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState(false);

    const [formData, setFormData] = useState({
        psicologoId: '',
        modalidadId: '1',
        fechaHora: '',
        ubicacion: '',
        notas: '',
    });

    const modalidades = [
        { id: 1, nombre: 'Presencial' },
        { id: 2, nombre: 'Virtual' },
    ];

    const psicologos = [
        { id: 1, nombre: 'Dr. Carlos López', especialidad: 'Ansiedad' },
        { id: 2, nombre: 'Dra. María García', especialidad: 'Depresión' },
    ];

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
        setError('');
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        try {
            await citasService.agendarCita({
                estudianteId: user.usuarioID,
                psicologoId: parseInt(formData.psicologoId),
                modalidadId: parseInt(formData.modalidadId),
                fechaHora: formData.fechaHora,
                ubicacion: formData.ubicacion,
                notas: formData.notas,
            });

            setSuccess(true);
            setTimeout(() => navigate('/citas'), 2000);
        } catch (err) {
            setError('Error al agendar cita');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    if (success) {
        return (
            <div className="flex items-center justify-center min-h-screen">
                <Card padding="lg" className="max-w-md text-center">
                    <div className="text-6xl mb-4 text-green-500">✓</div>
                    <h2 className="text-2xl font-bold text-gray-800 mb-2">¡Cita Agendada!</h2>
                    <p className="text-gray-600">Tu cita ha sido confirmada correctamente</p>
                </Card>
            </div>
        );
    }

    return (
        <div className="max-w-2xl mx-auto">
            <div className="mb-6">
                <h1 className="text-3xl font-bold text-gray-800">Agendar Cita</h1>
                <p className="text-gray-600 mt-2">Solicita una cita con un psicólogo</p>
            </div>

            <Card padding="lg">
                {error && (
                    <div className="mb-6 p-4 bg-red-50 border border-red-200 rounded-lg flex items-start gap-3">
                        <AlertCircle className="w-5 h-5 text-red-600 flex-shrink-0 mt-0.5" />
                        <p className="text-sm text-red-800">{error}</p>
                    </div>
                )}

                <form onSubmit={handleSubmit}>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-1">
                                Psicólogo <span className="text-red-500">*</span>
                            </label>
                            <select
                                name="psicologoId"
                                value={formData.psicologoId}
                                onChange={handleChange}
                                className="w-full rounded-lg border border-gray-300 px-3 py-2"
                                required
                            >
                                <option value="">Seleccionar...</option>
                                {psicologos.map(p => (
                                    <option key={p.id} value={p.id}>
                                        {p.nombre} - {p.especialidad}
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-1">
                                Modalidad <span className="text-red-500">*</span>
                            </label>
                            <select
                                name="modalidadId"
                                value={formData.modalidadId}
                                onChange={handleChange}
                                className="w-full rounded-lg border border-gray-300 px-3 py-2"
                            >
                                {modalidades.map(m => (
                                    <option key={m.id} value={m.id}>{m.nombre}</option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <Input
                        label="Fecha y Hora"
                        type="datetime-local"
                        name="fechaHora"
                        value={formData.fechaHora}
                        onChange={handleChange}
                        icon={Calendar}
                        required
                    />

                    {formData.modalidadId === '1' && (
                        <Input
                            label="Ubicación"
                            name="ubicacion"
                            value={formData.ubicacion}
                            onChange={handleChange}
                            icon={MapPin}
                            placeholder="Consultorio, Piso, etc."
                        />
                    )}

                    <div className="mb-4">
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Notas (opcional)
                        </label>
                        <textarea
                            name="notas"
                            value={formData.notas}
                            onChange={handleChange}
                            className="w-full rounded-lg border border-gray-300 px-3 py-2"
                            rows="4"
                            placeholder="Cuéntanos qué te preocupa..."
                        />
                    </div>

                    <Button
                        type="submit"
                        variant="primary"
                        fullWidth
                        loading={loading}
                    >
                        Agendar Cita
                    </Button>
                </form>
            </Card>
        </div>
    );
};

export default AgendarCita;