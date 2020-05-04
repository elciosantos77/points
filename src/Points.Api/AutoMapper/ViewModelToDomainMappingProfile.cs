using AutoMapper;
using Points.Api.ViewModels;
using Points.Api.ViewModels.Extrato;
using Points.Api.ViewModels.Pedido;
using Points.Api.ViewModels.Produto;
using Points.Domain.Endereco.Commands;
using Points.Domain.Extrato.Commands;
using Points.Domain.Pedido;
using Points.Domain.Pedido.Commands;
using Points.Domain.Produto.Commands;
using Points.Domain.Usuario.Commands;
using Points.Infra.CrossCutting.Identity;

namespace Points.Api.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioRequestViewModel, RegistrarUsuarioCommand>()
                .ConstructUsing(vm => new RegistrarUsuarioCommand(vm.Nome, vm.Email, vm.Senha));

            CreateMap<LoginViewModel, ApplicationUser>()
                .ForMember(usu => usu.Email, opt => opt.MapFrom(vm => vm.Email))
                .ForMember(usu => usu.PasswordHash, opt => opt.MapFrom(vm => vm.Senha));

            CreateMap<EnderecoViewModel, RegistrarEnderecoCommand>()
                .ConstructUsing(vm => new RegistrarEnderecoCommand(vm.Email, vm.CEP, vm.Rua, vm.Numero, vm.Complemento, vm.Bairro, vm.Cidade, vm.Estado));

            CreateMap<ProdutoViewModel, CadastrarProdutoCommand>()
                .ConstructUsing(vm => new CadastrarProdutoCommand(vm.Nome, vm.Pontuacao, vm.Categoria));

            CreateMap<ExtratoViewModel, CadastrarExtratoCommand>()
                .ConstructUsing(vm => new CadastrarExtratoCommand(vm.Pontos, vm.Email, vm.Estabelecimento));

            CreateMap<PedidoRequestViewModel, RealizarPedidoCommand>()
                 .ConstructUsing((s, ctx) =>
                 {
                     var command = new RealizarPedidoCommand(s.Email);
                     s.Itens?.ForEach(tr => command.AdicionarItem(new PedidoItem(tr.ProdutoId, tr.Quantidade)));
                     return command;
                 });
        }
    }
}