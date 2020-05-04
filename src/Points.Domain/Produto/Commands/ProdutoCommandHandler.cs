using Points.Domain.Core.Notifications;
using Points.Domain.Handlers;
using Points.Domain.Interfaces;
using Points.Domain.Produto.Events;
using Points.Domain.Produto.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Produto.Commands
{
    public class ProdutoCommandHandler : CommandHandler, INotificationHandler<CadastrarProdutoCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IProdutoQueryRepository _produtoQueryRepository;
        private readonly IProdutoCommandRepository _produtoCommandRepository;

        public ProdutoCommandHandler(INotificationHandler<DomainNotification> notifications,
                                     IMediatorHandler mediator,
                                     IProdutoQueryRepository produtoQueryRepository,
                                     IProdutoCommandRepository produtoCommandRepository)
            : base(mediator, notifications)

        {
            _mediator = mediator;
            _produtoQueryRepository = produtoQueryRepository;
            _produtoCommandRepository = produtoCommandRepository;
        }

        public Task Handle(CadastrarProdutoCommand notification, CancellationToken cancellationToken)
        {
            var produto = new Produto(notification.Nome, notification.Pontuacao, notification.Categoria);

            if (ProdutoExistente(notification.Nome, notification.MessageType) != null)
                return Task.CompletedTask;

            _produtoCommandRepository.Adicionar(produto);

            if (!HasNotificationsError())
                _mediator.PublicarEvento(new ProdutoCadastradoEvent(produto));

            return Task.CompletedTask;
        }

        private Produto ProdutoExistente(string nome, string messageType)
        {
            var produto = _produtoQueryRepository.ObterPorNome(nome);

            if (produto != null)
            {
                NotificarErro(messageType, $"Produto já cadastrado com o nome: {nome}");
                return produto;
            }
            return null;
        }
    }
}
