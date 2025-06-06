using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.API.AutoMapper;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.Validations.ClienteValidations;
using ModuloClientes.Core.Ports.IRepositories;
using ModuloClientes.Infrastructure.Data;
using ModuloClientes.Aplication.Handlers.ClienteHandler;
using ModuloClientes.Infrastructure.Persistence.Repository;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.API.Validations.EmpresaValidations;
using ModuloClientes.API.Validations.SeguroSaludValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuracion de servicios adicionales
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Validaciones Cliente
builder.Services.AddScoped<IValidator<ClienteCreateDto>, ClienteCreateValidation>();
builder.Services.AddScoped<IValidator<ClienteUpdateDto>, ClienteUpdateValidation>();
builder.Services.AddScoped<IValidator<SeguroSaludCreateDto>, SeguroSaludCreateValidation>();

// Validaciones Empresa
builder.Services.AddScoped<IValidator<EmpresaCreateDto>, EmpresaCreateValidation>();
builder.Services.AddScoped<IValidator<EmpresaUpdateDto>, EmpresaUpdateValidation>();

// Repositorios 
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<ISeguroSaludRepository, SeguroSaludRepository>();

// Registra los command handlers Cliente
// builder.Services.AddScoped<ICreateClienteCommandHandler, CreateClienteHandler>();
// builder.Services.AddScoped<IUpdateClienteCommandHandler, UpdateClienteHandler>();
// builder.Services.AddScoped<IDeleteClienteCommandHandler, DeleteClienteHandler>();
// builder.Services.AddScoped<IVincularEmpresaCommandHandler, VincularEmpresaHandler>();
// builder.Services.AddScoped<IAgregarOficioCommandHandler, AgregarOficioHandler>();
// builder.Services.AddScoped<IEliminarOficioCommandHandler, EliminarOficioHandler>();
// builder.Services.AddScoped<IUpdateOficiosCommandHandler, ReemplazarOficiosHandler>();
// Registra los query handlers Cliente
// builder.Services.AddScoped<IGetClienteByIdQueryHandler, GetClienteByIdHandler>();
// builder.Services.AddScoped<IListClientesQueryHandler, ListClientesHandler>();

//Registrar los commands handlers Empresa
// builder.Services.AddScoped<ICreateEmpresaCommandHandler,CreateEmpresaHandler>();
// builder.Services.AddScoped<IUpdateEmpresaCommandHandler, UpdateEmpresaHandler>();
// builder.Services.AddScoped<IDeleteEmpresaCommandHandler, DeleteEmpresaHandler>();
// Registrar los query habndlers Empresa
// builder.Services.AddScoped<IGetEmpresaByIdQueryHandler, GetEmpresaByIdHandler>();
// builder.Services.AddScoped<IListEmpresasQueryHandler, ListEmpresasHandler>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateClienteHandler>();
});

// Services

// Agregar DbContext
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
app.UseResponseCaching(); // cache 
app.MapControllers();


app.Run();
