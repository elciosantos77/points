using Moq;
using Points.Domain.Core.Notifications;
using Points.Domain.Interfaces;
using System;
using Xunit;
using Points.Domain.Pedido.Commands;
using Points.Domain.Pedido.Repository;
using Points.Domain.Validations;
using Points.Domain.Produto.Repository;
using Points.Domain.Usuario.Repository;
using Points.Domain.Saldo.Repository;
using Points.Domain.Endereco.Repository;
using Points.Domain.Pedido;
using System.Collections.Generic;
using Points.Domain.Pedido.Events;

namespace Points.Domain.Tests.Commands
{
    public class PedidoCommandHandlerTest
    {
        PedidoCommandHandler pedidoCommandHandler;
        Mock<IMediatorHandler> _mediatorHandlerMock;
        Mock<IPedidoQueryRepository> _pedidoQueryRepositoryMock;
        Mock<IPedidoCommandRepository> _pedidoCommandRepositoryMock;
        Mock<IProdutoQueryRepository> _produtoQueryRepositoryMock;
        Mock<IUsuarioQueryRepository> _usuarioQueryRepositoryMock;
        Mock<ISaldoQueryRepository> _saldoQueryRepositoryMock;
        Mock<ISaldoCommandRepository> _saldoCommandRepositoryMock;
        Mock<IEnderecoQueryRepository> _enderecoQueryRepositoryMock;
        PedidoValidator _pedidoValidator;

        public PedidoCommandHandlerTest()
        {
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _pedidoQueryRepositoryMock = new Mock<IPedidoQueryRepository>();
            _pedidoCommandRepositoryMock = new Mock<IPedidoCommandRepository>();
            _pedidoCommandRepositoryMock = new Mock<IPedidoCommandRepository>();
            _produtoQueryRepositoryMock = new Mock<IProdutoQueryRepository>();
            _usuarioQueryRepositoryMock = new Mock<IUsuarioQueryRepository>();
            _saldoQueryRepositoryMock = new Mock<ISaldoQueryRepository>();
            _saldoCommandRepositoryMock = new Mock<ISaldoCommandRepository>();
            _enderecoQueryRepositoryMock = new Mock<IEnderecoQueryRepository>();
            _pedidoValidator = new PedidoValidator();

            pedidoCommandHandler = new PedidoCommandHandler(
               new DomainNotificationHandler(),
               _mediatorHandlerMock.Object,
               _pedidoQueryRepositoryMock.Object,
               _pedidoCommandRepositoryMock.Object,
               _produtoQueryRepositoryMock.Object,
               _usuarioQueryRepositoryMock.Object,
               _saldoQueryRepositoryMock.Object,
               _saldoCommandRepositoryMock.Object,
               _enderecoQueryRepositoryMock.Object,
               _pedidoValidator);
        }


        [Fact]
        public void RealizarPedidoCommand_Sucesso()
        {
            var email = "elcio-santos3@hotmail.com";
            var command = new RealizarPedidoCommand(email);

            var endereco = new Endereco.Endereco(email, "14801-320", "Rua Goiás", 23, "", "Centro", "Araraquara", "SP");
            _enderecoQueryRepositoryMock.Setup(x => x.ObterPorEmail(email)).Returns(endereco);

            var pedido = new Pedido.Pedido("elcio-santos3@hotmail.com", Enums.StatusEntrega.AGUARDANDOENVIO, DateTime.Now, new List<PedidoItem>(), endereco);
            _pedidoCommandRepositoryMock.Setup(x => x.Adicionar(pedido));

            var evento = new PedidoRealizadoEvent(pedido);
            _mediatorHandlerMock.Setup(x => x.PublicarEvento(evento));

            var result = pedidoCommandHandler.Handle(command, new System.Threading.CancellationToken());

            _pedidoQueryRepositoryMock.Verify();
            _enderecoQueryRepositoryMock.Verify();
        }
    }
}
