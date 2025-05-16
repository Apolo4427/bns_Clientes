using AutoMapper;
using ModuloClientes.Core.Models;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;

namespace ModuloClientes.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() //TODO: VALIDAR TODOS LOS MAPEOS, Y TERMINARLOS
        {
            // Map entities to Response DTOs only
            CreateMap<Cliente, ClienteResponseDto>()
                .ForMember(dto=> dto.NombreSeguroSalud, map => map.MapFrom(
                    c => c.SeguroSalud != null ? c.SeguroSalud.NombrePlan : null ))
                .ForMember(dto => dto.Edad, map => map.MapFrom(
                    c => c.Edad))
                .ForMember(dto => dto.Oficio, map => map.MapFrom(
                    c => c.Oficios));
            CreateMap<ClienteRelacion, ClienteRelacionResponseDto>()
                .ForMember(dto => dto.NombreCliente, map => map.MapFrom(
                    c => $"{c.Cliente.Nombre} {c.Cliente.Apellido}"
                ))
                .ForMember(dto => dto.NombreRelacionado, map => map.MapFrom(
                    c => $"{c.Cliente.Nombre} {c.Cliente.Apellido}"
                ));
            CreateMap<Empresa, EmpresaResponseDto>();
            CreateMap<SeguroSalud, SeguroSaludResponseDto>();
            CreateMap<EmpresaCliente, EmpresaClienteResponseDto>()
                .ForMember(dto => dto.NombreCliente, map => map.MapFrom(ec => $"{ec.Cliente.Nombre} {ec.Cliente.Apellido}"))
                .ForMember(dto => dto.NombreEmpresa, map => map.MapFrom(ec => ec.Empresa.Nombre));
            
            // Map Create Command to Entitys
            CreateMap<CreateClienteCommand, Cliente>()  
                .ConstructUsing(c => new Cliente(
                    c.Nombre,
                    c.Apellido,
                    c.Correo,
                    c.Telefono,
                    c.FechaNacimiento,
                    c.EstadoCivil,
                    c.EstadoTributario,
                    c.SocialSecurityNumber,
                    c.Direccion
                ));
            
            CreateMap<CreateEmpresaCommand, Empresa>()
                .ConstructUsing(c => new Empresa(
                    c.Nombre,
                    c.EIN,
                    c.Direccion,
                    c.Telefono,
                    c.CorreoContacto,
                    c.FechaConstitucion
                 ));

            // Map Create Dto to Create Command
            CreateMap<ClienteCreateDto, CreateClienteCommand>();
            
            CreateMap<EmpresaCreateDto, CreateEmpresaCommand>();

            CreateMap<EmpresaClienteCreateDto, VincularEmpresaCommand>();
            
            CreateMap<SeguroSaludCreateDto, SeguroSalud>()
                .ConvertUsing(dto => new SeguroSalud(
                    dto.Proveedor,
                    dto.NombrePlan,
                    dto.NumeroPoliza,
                    dto.FechaInicio,
                    dto.FechaFin,
                    dto.PrimaMensual
                ));

            // Map Update Dto to Update Command
            CreateMap<ClienteUpdateDto, UpdateClienteCommand>();

        }
    }
}
