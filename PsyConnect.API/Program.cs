using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using PsyConnect.Data.Repositories.Implementations;
using PsyConnect.Business.Services.Usuarios;
using PsyConnect.Business.Services.Citas;
using PsyConnect.Business.Services.Tests;
using PsyConnect.Business.Services.Resultados;
using PsyConnect.Business.Services.Recomendaciones;
using PsyConnect.Business.Services.Certificados;
using PsyConnect.Business.Mappings;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PsyConnectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IRespuestaTestRepository, RespuestaTestRepository>();
builder.Services.AddScoped<IResultadoRepository, ResultadoRepository>();
builder.Services.AddScoped<IRecomendacionRepository, RecomendacionRepository>();
builder.Services.AddScoped<ICertificadoRepository, CertificadoRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IResultadoService, ResultadoService>();
builder.Services.AddScoped<IRecomendacionService, RecomendacionService>();
builder.Services.AddScoped<ICertificadoService, CertificadoService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// ============================================
// CONFIGURACIÓN JWT - NUEVO
// ============================================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });

    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins(
                   "http://localhost:3000",
                   "http://localhost:5173",
                   "https://localhost:5173"
               )
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

// ============================================
// AGREGAR AUTENTICACIÓN - NUEVO
// ============================================
app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();