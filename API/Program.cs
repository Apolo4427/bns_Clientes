using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.API.AutoMapper;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.Validations.ClienteValidations;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.Queries.ClienteQueries;
using ModuloClientes.Core.Ports.Queries.EmpresaQueries;
using ModuloClientes.Core.Ports.Repositories;
using ModuloClientes.Infrastructure.Data;
using ModuloClientes.Infrastructure.Persistence.Handlers.EmpresaHandler;
using ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler;
using ModuloClientes.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Agregar DbContext

// Configuracion de servicios adicionales
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Validaciones Cliente
builder.Services.AddScoped<IValidator<ClienteCreateDto>, ClienteCreateValidation>();

// Repositorios 
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();

// Registra los command handlers Cliente
builder.Services.AddScoped<ICreateClienteCommandHandler, CreateClienteHandler>();
builder.Services.AddScoped<IUpdateClienteCommandHandler, UpdateClienteHandler>();
builder.Services.AddScoped<IDeleteClienteCommandHandler, DeleteClienteHandler>();
builder.Services.AddScoped<IVincularEmpresaCommandHandler, VincularEmpresaHandler>();
builder.Services.AddScoped<IAgregarOficioCommandHandler, AgregarOficioHandler>();
builder.Services.AddScoped<IEliminarOficioCommandHandler, EliminarOficioHandler>();
builder.Services.AddScoped<IUpdateOficiosCommandHandler, ReemplazarOficiosHandler>();
// Registra los query handlers Cliente
builder.Services.AddScoped<IGetClienteByIdQueryHandler, GetClienteByIdHandler>();
builder.Services.AddScoped<IListClientesQueryHandler, ListClientesHandler>();

//Registrar los commands handlers Empresa
builder.Services.AddScoped<ICreateEmpresaCommandHandler,CreateEmpresaHandler>();
builder.Services.AddScoped<IUpdateEmpresaCommandHandler, UpdateEmpresaHandler>();
builder.Services.AddScoped<IDeleteEmpresaCommandHandler, DeleteEmpresaHandler>();
// Registrar los query habndlers Empresa
builder.Services.AddScoped<IGetEmpresaByIdQueryHandler, GetEmpresaByIdHandler>();
builder.Services.AddScoped<IListEmpresasQueryHandler, ListEmpresasHandler>();


// Services

// Indicamos a que base de datos nos conectamos
builder.Services.AddDbContext<DbClientesContext>((options)=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
