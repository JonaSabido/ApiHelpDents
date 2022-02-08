using System.Reflection.Metadata.Ecma335;
using System;
using AutoMapper;
using ApiHelpDents.Domain.Dtos.Responses;
using ApiHelpDents.Domain.Dtos.Requests;
using ApiHelpDents.Domain.Entities;

namespace ApiHelpDents.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(){

            //Responses
            CreateMap<Administrador, AdminResponse>()
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
            .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
            .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña));

            
            CreateMap<Asesor, AsesorResponse>()
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.ClaveUsuarioNavigation.Nombres))
            .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.ClaveUsuarioNavigation.Apellidos))
            .ForMember(dest => dest.Especialidad, opt => opt.MapFrom(src => src.ClaveEspNavigation.NombreEsp))
            .ForMember(dest => dest.Turno, opt => opt.MapFrom(src => src.ClaveTurnoNavigation.NombreTurno))
            .ForMember(dest => dest.Costo, opt => opt.MapFrom(src => src.Costo))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForMember(dest => dest.Facebook, opt => opt.MapFrom(src => src.Facebook))
            .ForMember(dest => dest.Instagram, opt => opt.MapFrom(src => src.Instagram))
            .ForMember(dest => dest.Youtube, opt => opt.MapFrom(src => src.Youtube))
            .ForMember(dest => dest.Twitter, opt => opt.MapFrom(src => src.Twitter))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            CreateMap<Comentario, ComentarioResponse>()
            .ForMember(dest => dest.IdAsesor, opt => opt.MapFrom(src => src.ClaveAsesor))
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.ClaveUsuarioNavigation.Nombres))
            .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.ClaveUsuarioNavigation.Apellidos))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            CreateMap<Comentario, ComentarioResponse2>()
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.ClaveUsuarioNavigation.Nombres))
            .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.ClaveUsuarioNavigation.Apellidos))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            CreateMap<Especialidad, EspecialidadResponse>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreEsp));

            CreateMap<Turno, TurnoResponse>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreTurno));

            CreateMap<Usuario, UsuarioResponse>()
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
            .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
            .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña));

            //Creates
            CreateMap<AdminCreateRequest, Administrador>()
            .ForPath(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
            .ForPath(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
            .ForPath(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
            .ForPath(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña));

            CreateMap<AsesorCreateRequest, Asesor>()
            .ForPath(dest => dest.ClaveUsuario, opt => opt.MapFrom(src => src.ClaveUsuario))
            .ForPath(dest => dest.ClaveEsp, opt => opt.MapFrom(src => src.ClaveEsp))
            .ForPath(dest => dest.ClaveTurno, opt => opt.MapFrom(src => src.ClaveTurno))
            .ForPath(dest => dest.Costo, opt => opt.MapFrom(src => src.Costo))
            .ForPath(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForPath(dest => dest.Facebook, opt => opt.MapFrom(src => src.Facebook))
            .ForPath(dest => dest.Instagram, opt => opt.MapFrom(src => src.Instagram))
            .ForPath(dest => dest.Youtube, opt => opt.MapFrom(src => src.Youtube))
            .ForPath(dest => dest.Twitter, opt => opt.MapFrom(src => src.Twitter))
            .ForPath(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            CreateMap<AsesorFilterRequest, Asesor>()
            .ForPath(dest => dest.ClaveUsuarioNavigation.Nombres, opt => opt.MapFrom(src => src.Nombres))
            .ForPath(dest => dest.ClaveEspNavigation.NombreEsp, opt => opt.MapFrom(src => src.Especialidad))
            .ForPath(dest => dest.ClaveTurnoNavigation.NombreTurno, opt => opt.MapFrom(src => src.Turno))
            .ForPath(dest => dest.Costo, opt => opt.MapFrom(src => src.Costo));

            CreateMap<ComentarioCreateRequest, Comentario>()
            .ForPath(dest => dest.ClaveUsuario, opt => opt.MapFrom(src => src.ClaveUsuario))
            .ForPath(dest => dest.ClaveAsesor, opt => opt.MapFrom(src => src.ClaveAsesor))
            .ForPath(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));
            
            CreateMap<EspecialidadCreateRequest, Especialidad>()
            .ForPath(dest => dest.NombreEsp, opt => opt.MapFrom(src => src.NombreEsp));

            CreateMap<TurnoCreateRequest, Turno>()
            .ForPath(dest => dest.NombreTurno, opt => opt.MapFrom(src => src.NombreTurno));
            
            CreateMap<UsuarioCreateRequest, Usuario>()
            .ForPath(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
            .ForPath(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
            .ForPath(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
            .ForPath(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña));

            //Updates
            CreateMap<AdminUpdateRequest, Administrador>()
            .ForPath(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
            .ForPath(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
            .ForPath(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña));

            CreateMap<AsesorUpdateRequest, Asesor>()
            .ForPath(dest => dest.ClaveEsp, opt => opt.MapFrom(src => src.ClaveEsp))
            .ForPath(dest => dest.ClaveTurno, opt => opt.MapFrom(src => src.ClaveTurno))
            .ForPath(dest => dest.Costo, opt => opt.MapFrom(src => src.Costo))
            .ForPath(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ForPath(dest => dest.Facebook, opt => opt.MapFrom(src => src.Facebook))
            .ForPath(dest => dest.Instagram, opt => opt.MapFrom(src => src.Instagram))
            .ForPath(dest => dest.Youtube, opt => opt.MapFrom(src => src.Youtube))
            .ForPath(dest => dest.Twitter, opt => opt.MapFrom(src => src.Twitter))
            .ForPath(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            CreateMap<UsuarioUpdateRequest, Usuario>()
            .ForPath(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
            .ForPath(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
            .ForPath(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña));
            

            
        }
    }
}