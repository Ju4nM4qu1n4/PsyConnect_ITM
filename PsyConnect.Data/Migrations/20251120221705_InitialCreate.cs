using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsyConnect.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosCita",
                columns: table => new
                {
                    EstadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCita", x => x.EstadoID);
                });

            migrationBuilder.CreateTable(
                name: "EstadosRespuestaTest",
                columns: table => new
                {
                    EstadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosRespuestaTest", x => x.EstadoID);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadesCita",
                columns: table => new
                {
                    ModalidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadesCita", x => x.ModalidadID);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadesTest",
                columns: table => new
                {
                    ModalidadTestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadesTest", x => x.ModalidadTestID);
                });

            migrationBuilder.CreateTable(
                name: "TiposTest",
                columns: table => new
                {
                    TipoTestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTest", x => x.TipoTestID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Carrera = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteID);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Psicologos",
                columns: table => new
                {
                    PsicologoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Licencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoraInicioJornada = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFinJornada = table.Column<TimeSpan>(type: "time", nullable: false),
                    SedeAsignada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "Fraternidad")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psicologos", x => x.PsicologoID);
                    table.ForeignKey(
                        name: "FK_Psicologos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    HistoricoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteID = table.Column<int>(type: "int", nullable: true),
                    TipoActividad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActividad = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.HistoricoID);
                    table.ForeignKey(
                        name: "FK_Historicos_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    CitaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteID = table.Column<int>(type: "int", nullable: false),
                    PsicologoID = table.Column<int>(type: "int", nullable: false),
                    ModalidadID = table.Column<int>(type: "int", nullable: false),
                    EstadoID = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false, defaultValue: 60),
                    Ubicacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EnlaceTeams = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NotasEstudiante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObservacionesPsicologo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.CitaID);
                    table.ForeignKey(
                        name: "FK_Citas_EstadosCita_EstadoID",
                        column: x => x.EstadoID,
                        principalTable: "EstadosCita",
                        principalColumn: "EstadoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_ModalidadesCita_ModalidadID",
                        column: x => x.ModalidadID,
                        principalTable: "ModalidadesCita",
                        principalColumn: "ModalidadID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Psicologos_PsicologoID",
                        column: x => x.PsicologoID,
                        principalTable: "Psicologos",
                        principalColumn: "PsicologoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    ReporteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PsicologoID = table.Column<int>(type: "int", nullable: true),
                    AdministradorID = table.Column<int>(type: "int", nullable: true),
                    TipoReporte = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadCitas = table.Column<int>(type: "int", nullable: true),
                    CantidadTests = table.Column<int>(type: "int", nullable: true),
                    TestsMasUtilizados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelesRiesgo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    RutaArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.ReporteID);
                    table.ForeignKey(
                        name: "FK_Reportes_Psicologos_PsicologoID",
                        column: x => x.PsicologoID,
                        principalTable: "Psicologos",
                        principalColumn: "PsicologoID");
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_AdministradorID",
                        column: x => x.AdministradorID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTestID = table.Column<int>(type: "int", nullable: false),
                    ModalidadTestID = table.Column<int>(type: "int", nullable: false),
                    NombreTest = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PsicologoID = table.Column<int>(type: "int", nullable: false),
                    CantidadPreguntas = table.Column<int>(type: "int", nullable: false),
                    TiempoEstimado = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestID);
                    table.ForeignKey(
                        name: "FK_Tests_ModalidadesTest_ModalidadTestID",
                        column: x => x.ModalidadTestID,
                        principalTable: "ModalidadesTest",
                        principalColumn: "ModalidadTestID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_Psicologos_PsicologoID",
                        column: x => x.PsicologoID,
                        principalTable: "Psicologos",
                        principalColumn: "PsicologoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_TiposTest_TipoTestID",
                        column: x => x.TipoTestID,
                        principalTable: "TiposTest",
                        principalColumn: "TipoTestID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreguntasTest",
                columns: table => new
                {
                    PreguntaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Opcion Multiple"),
                    Puntaje = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasTest", x => x.PreguntaID);
                    table.ForeignKey(
                        name: "FK_PreguntasTest_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasTest",
                columns: table => new
                {
                    RespuestaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteID = table.Column<int>(type: "int", nullable: false),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    EstadoID = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PuntajeTotal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasTest", x => x.RespuestaID);
                    table.ForeignKey(
                        name: "FK_RespuestasTest_EstadosRespuestaTest_EstadoID",
                        column: x => x.EstadoID,
                        principalTable: "EstadosRespuestaTest",
                        principalColumn: "EstadoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestasTest_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestasTest_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpcionesRespuesta",
                columns: table => new
                {
                    OpcionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreguntaID = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionesRespuesta", x => x.OpcionID);
                    table.ForeignKey(
                        name: "FK_OpcionesRespuesta_PreguntasTest_PreguntaID",
                        column: x => x.PreguntaID,
                        principalTable: "PreguntasTest",
                        principalColumn: "PreguntaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificados",
                columns: table => new
                {
                    CertificadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteID = table.Column<int>(type: "int", nullable: false),
                    RespuestaTestID = table.Column<int>(type: "int", nullable: true),
                    CitaID = table.Column<int>(type: "int", nullable: true),
                    TipoCertificado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    RutaArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Descargado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FechaDescarga = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados", x => x.CertificadoID);
                    table.ForeignKey(
                        name: "FK_Certificados_Citas_CitaID",
                        column: x => x.CitaID,
                        principalTable: "Citas",
                        principalColumn: "CitaID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Certificados_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_RespuestasTest_RespuestaTestID",
                        column: x => x.RespuestaTestID,
                        principalTable: "RespuestasTest",
                        principalColumn: "RespuestaID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ResultadosInterpretacion",
                columns: table => new
                {
                    ResultadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespuestaID = table.Column<int>(type: "int", nullable: false),
                    Interpretacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recomendacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaEvaluacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PsicologoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosInterpretacion", x => x.ResultadoID);
                    table.ForeignKey(
                        name: "FK_ResultadosInterpretacion_Psicologos_PsicologoID",
                        column: x => x.PsicologoID,
                        principalTable: "Psicologos",
                        principalColumn: "PsicologoID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ResultadosInterpretacion_RespuestasTest_RespuestaID",
                        column: x => x.RespuestaID,
                        principalTable: "RespuestasTest",
                        principalColumn: "RespuestaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesRespuestaTest",
                columns: table => new
                {
                    DetalleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespuestaID = table.Column<int>(type: "int", nullable: false),
                    PreguntaID = table.Column<int>(type: "int", nullable: false),
                    OpcionSeleccionada = table.Column<int>(type: "int", nullable: true),
                    ValorRespuesta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesRespuestaTest", x => x.DetalleID);
                    table.ForeignKey(
                        name: "FK_DetallesRespuestaTest_OpcionesRespuesta_OpcionSeleccionada",
                        column: x => x.OpcionSeleccionada,
                        principalTable: "OpcionesRespuesta",
                        principalColumn: "OpcionID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DetallesRespuestaTest_PreguntasTest_PreguntaID",
                        column: x => x.PreguntaID,
                        principalTable: "PreguntasTest",
                        principalColumn: "PreguntaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesRespuestaTest_RespuestasTest_RespuestaID",
                        column: x => x.RespuestaID,
                        principalTable: "RespuestasTest",
                        principalColumn: "RespuestaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecomendacionesPersonalizadas",
                columns: table => new
                {
                    RecomendacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteID = table.Column<int>(type: "int", nullable: false),
                    PsicologoID = table.Column<int>(type: "int", nullable: false),
                    ResultadoID = table.Column<int>(type: "int", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoRecurso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Vigente = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecomendacionesPersonalizadas", x => x.RecomendacionID);
                    table.ForeignKey(
                        name: "FK_RecomendacionesPersonalizadas_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecomendacionesPersonalizadas_Psicologos_PsicologoID",
                        column: x => x.PsicologoID,
                        principalTable: "Psicologos",
                        principalColumn: "PsicologoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecomendacionesPersonalizadas_ResultadosInterpretacion_ResultadoID",
                        column: x => x.ResultadoID,
                        principalTable: "ResultadosInterpretacion",
                        principalColumn: "ResultadoID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_CitaID",
                table: "Certificados",
                column: "CitaID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_EstudianteID",
                table: "Certificados",
                column: "EstudianteID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_RespuestaTestID",
                table: "Certificados",
                column: "RespuestaTestID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_EstadoID",
                table: "Citas",
                column: "EstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_EstudianteID",
                table: "Citas",
                column: "EstudianteID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_FechaHora",
                table: "Citas",
                column: "FechaHora");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_ModalidadID",
                table: "Citas",
                column: "ModalidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PsicologoID",
                table: "Citas",
                column: "PsicologoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRespuestaTest_OpcionSeleccionada",
                table: "DetallesRespuestaTest",
                column: "OpcionSeleccionada");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRespuestaTest_PreguntaID",
                table: "DetallesRespuestaTest",
                column: "PreguntaID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRespuestaTest_RespuestaID",
                table: "DetallesRespuestaTest",
                column: "RespuestaID");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosCita_Nombre",
                table: "EstadosCita",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstadosRespuestaTest_Nombre",
                table: "EstadosRespuestaTest",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Matricula",
                table: "Estudiantes",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_UsuarioID",
                table: "Estudiantes",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_EstudianteID",
                table: "Historicos",
                column: "EstudianteID");

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadesCita_Nombre",
                table: "ModalidadesCita",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadesTest_Nombre",
                table: "ModalidadesTest",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpcionesRespuesta_PreguntaID",
                table: "OpcionesRespuesta",
                column: "PreguntaID");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasTest_TestID",
                table: "PreguntasTest",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Psicologos_Cedula",
                table: "Psicologos",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Psicologos_UsuarioID",
                table: "Psicologos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionesPersonalizadas_EstudianteID",
                table: "RecomendacionesPersonalizadas",
                column: "EstudianteID");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionesPersonalizadas_PsicologoID",
                table: "RecomendacionesPersonalizadas",
                column: "PsicologoID");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionesPersonalizadas_ResultadoID",
                table: "RecomendacionesPersonalizadas",
                column: "ResultadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_AdministradorID",
                table: "Reportes",
                column: "AdministradorID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_PsicologoID",
                table: "Reportes",
                column: "PsicologoID");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasTest_EstadoID",
                table: "RespuestasTest",
                column: "EstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasTest_EstudianteID",
                table: "RespuestasTest",
                column: "EstudianteID");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasTest_TestID",
                table: "RespuestasTest",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosInterpretacion_PsicologoID",
                table: "ResultadosInterpretacion",
                column: "PsicologoID");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosInterpretacion_RespuestaID",
                table: "ResultadosInterpretacion",
                column: "RespuestaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ModalidadTestID",
                table: "Tests",
                column: "ModalidadTestID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_PsicologoID",
                table: "Tests",
                column: "PsicologoID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TipoTestID",
                table: "Tests",
                column: "TipoTestID");

            migrationBuilder.CreateIndex(
                name: "IX_TiposTest_Nombre",
                table: "TiposTest",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropTable(
                name: "DetallesRespuestaTest");

            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropTable(
                name: "RecomendacionesPersonalizadas");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "OpcionesRespuesta");

            migrationBuilder.DropTable(
                name: "ResultadosInterpretacion");

            migrationBuilder.DropTable(
                name: "EstadosCita");

            migrationBuilder.DropTable(
                name: "ModalidadesCita");

            migrationBuilder.DropTable(
                name: "PreguntasTest");

            migrationBuilder.DropTable(
                name: "RespuestasTest");

            migrationBuilder.DropTable(
                name: "EstadosRespuestaTest");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "ModalidadesTest");

            migrationBuilder.DropTable(
                name: "Psicologos");

            migrationBuilder.DropTable(
                name: "TiposTest");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
