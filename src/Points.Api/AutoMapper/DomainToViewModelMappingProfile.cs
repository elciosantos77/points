using AutoMapper;
using Points.Api.Extensions;
using Points.Api.ViewModels;
using Points.Api.ViewModels.Extrato;
using Points.Api.ViewModels.Pedido;
using Points.Api.ViewModels.Produto;
using Points.Domain.Endereco;
using Points.Domain.Enums;
using Points.Domain.Extrato;
using Points.Domain.Pedido;
using Points.Domain.Produto;
using Points.Infra.CrossCutting.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Points.Api.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<UsuarioResponseViewModel, ApplicationUser>()
                 .ForMember(usu => usu.Id, opt => opt.MapFrom(vm => vm.Id))
                 .ForMember(usu => usu.Nome, opt => opt.MapFrom(vm => vm.Nome))
                 .ForMember(usu => usu.Email, opt => opt.MapFrom(vm => vm.Email))
                 .ReverseMap();

            CreateMap<Pedido, PedidoResponseViewModel>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(vm => vm.Email, opt => opt.MapFrom(p => p.Email))
                .ForMember(vm => vm.Data, opt => opt.MapFrom(p => p.Data))
                .ForMember(vm => vm.Endereco, opt => opt.MapFrom(p => p.Endereco))
                .ForMember(vm => vm.StatusEntrega, c => c.MapFrom(p => p.StatusEntrega.GetDisplayName()))
                .ForMember(vm => vm.Itens, opt => opt.MapFrom(p => p.Itens));
               
            ;
        }
    }
}