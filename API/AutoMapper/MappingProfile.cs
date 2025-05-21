using AutoMapper;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Enums;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Models.ValueObjects.SeguroSaludValueObjects;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;

namespace ModuloClientes.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //
            // 1) Primitivas → Value Objects
            //
            CreateMap<string, Name>()         .ConvertUsing(s => new Name(s));
            CreateMap<string, Surname>()      .ConvertUsing(s => new Surname(s));
            CreateMap<string, Email>()        .ConvertUsing(s => new Email(s));
            CreateMap<string, Phone>()        .ConvertUsing(s => new Phone(s));
            CreateMap<string, SSN>()          .ConvertUsing(s => new SSN(s));
            CreateMap<string, Address>()      .ConvertUsing(s => new Address(s));
            CreateMap<string, CompanyName>()  .ConvertUsing(s => new CompanyName(s));
            CreateMap<string, EIN>()          .ConvertUsing(s => new EIN(s));
            CreateMap<string, PlanName>()     .ConvertUsing(s => new PlanName(s));
            CreateMap<string, PolicyNumber>() .ConvertUsing(s => new PolicyNumber(s));

            //
            // 2) DTOs de creación/actualización → Commands
            //
            CreateMap<ClienteCreateDto, CreateClienteCommand>();             
            CreateMap<ClienteUpdateDto, UpdateClienteCommand>();             
            
            CreateMap<EmpresaCreateDto, CreateEmpresaCommand>();             
            CreateMap<EmpresaUpdateDto, UpdateEmpresaCommand>();             

            // Para SeguroSalud no existe CreateCommand, así que lo mapeamos directo a la entidad:
            // TODO: CREAR CreateCommand de SeguroSalud
            // CreateMap<SeguroSaludCreateDto, SeguroSalud>()
            //     .ConstructUsing(dto => new SeguroSalud(
            //         dto.Proveedor,
            //         dto.NombrePlan,
            //         dto.NumeroPoliza,
            //         dto.FechaInicio,
            //         dto.FechaFin,
            //         dto.PrimaMensual
            //     ));                                                     // :contentReference[oaicite:4]{index=4}

            //
            // 3) Commands de creación → Entidades
            //
            CreateMap<CreateClienteCommand, Cliente>()
                .ConstructUsing((src, ctx) => new Cliente(
                    ctx.Mapper.Map<Name>(src.Nombre),
                    ctx.Mapper.Map<Surname>(src.Apellido),
                    ctx.Mapper.Map<Email>(src.Correo),
                    ctx.Mapper.Map<Phone>(src.Telefono),
                    src.FechaNacimiento,
                    Enum.Parse<MaritalStatus>(src.EstadoCivil),
                    Enum.Parse<TaxStatus>(src.EstadoTributario),
                    ctx.Mapper.Map<SSN>(src.SocialSecurityNumber),
                    ctx.Mapper.Map<Address>(src.Direccion)
                ));                                                     

            CreateMap<CreateEmpresaCommand, Empresa>()
                .ConstructUsing((src, ctx) => new Empresa(
                    ctx.Mapper.Map<CompanyName>(src.Nombre),
                    ctx.Mapper.Map<EIN>(src.EIN),
                    ctx.Mapper.Map<Address>(src.Direccion),
                    ctx.Mapper.Map<Phone>(src.Telefono),
                    ctx.Mapper.Map<Email>(src.CorreoContacto),
                    src.FechaConstitucion
                ));                                                     

            //
            // 4) Entidades → Response DTOs
            //
            CreateMap<Cliente, ClienteResponseDto>()
                .ForMember(d => d.Nombre,            o => o.MapFrom(src => src.Nombre.Value))
                .ForMember(d => d.Apellido,          o => o.MapFrom(src => src.Apellido.Value))
                .ForMember(d => d.Correo,            o => o.MapFrom(src => src.Correo.Value))
                .ForMember(d => d.Telefono,          o => o.MapFrom(src => src.Telefono.Value))
                .ForMember(d => d.Direccion,         o => o.MapFrom(src => src.Direccion.Value))
                .ForMember(d => d.Oficio,            o => o.MapFrom(src => src.Oficios))
                .ForMember(d => d.Edad,              o => o.MapFrom(src => src.Edad))
                .ForMember(d => d.NombreSeguroSalud, o => o.MapFrom(c => c.SeguroSalud != null 
                                                                          ? c.SeguroSalud.NombrePlan.Value 
                                                                          : null))
                .ForMember(d => d.Empresas,          o => o.MapFrom(src => src.Empresas)); 

            CreateMap<Empresa, EmpresaResponseDto>()
                .ForMember(d => d.Nombre,           o => o.MapFrom(src => src.Nombre.Value))
                .ForMember(d => d.EIN,              o => o.MapFrom(src => src.Ein.Value))
                .ForMember(d => d.Direccion,        o => o.MapFrom(src => src.Direccion.Value))
                .ForMember(d => d.Telefono,         o => o.MapFrom(src => src.Telefono.Value))
                .ForMember(d => d.CorreoContacto,   o => o.MapFrom(src => src.CorreoContacto.Value))
                .ForMember(d => d.Clientes,         o => o.MapFrom(src => src.Clientes));    

            CreateMap<SeguroSalud, SeguroSaludResponseDto>()
                .ForMember(d => d.Proveedor,    o => o.MapFrom(src => src.Proveedor.Value))
                .ForMember(d => d.NombrePlan,   o => o.MapFrom(src => src.NombrePlan.Value))
                .ForMember(d => d.NumeroPoliza, o => o.MapFrom(src => src.NumeroPoliza.Value))
                .ForMember(d => d.Clientes,     o => o.MapFrom(src => src.Clientes));     

            // Relacionados (ClienteRelacion y EmpresaCliente)
            CreateMap<EmpresaCliente, EmpresaClienteResponseDto>()
                .ForMember(d => d.NombreCliente, o => o.MapFrom(ec => $"{ec.Cliente.Nombre.Value} {ec.Cliente.Apellido.Value}"))
                .ForMember(d => d.NombreEmpresa, o => o.MapFrom(ec => ec.Empresa.Nombre.Value));
            CreateMap<ClienteRelacion, ClienteRelacionResponseDto>()
                .ForMember(d => d.NombreCliente,    o => o.MapFrom(c => $"{c.Cliente.Nombre.Value} {c.Cliente.Apellido.Value}"))
                .ForMember(d => d.NombreRelacionado, o => o.MapFrom(c => $"{c.Relacionado.Nombre.Value} {c.Relacionado.Apellido.Value}"));
        }
    }
}

