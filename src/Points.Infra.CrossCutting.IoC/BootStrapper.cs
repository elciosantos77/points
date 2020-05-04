using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Points.Domain.Core.EventSourcing;
using Points.Domain.Core.Notifications;
using Points.Domain.Handlers;
using Points.Domain.Interfaces;
using Points.Infra.Data.NoSQL.EventSourcing;
using Points.Infra.Data.NoSQL.Repositories;
using Points.Domain.Usuario.Commands;
using Points.Domain.Usuario.Events;
using Points.Domain.Interfaces.Services;
using Points.Domain.Usuario.Services;
using Points.Domain.Endereco.Commands;
using Points.Domain.Endereco;
using Points.Domain.Endereco.Repository;
using Points.Domain.Produto.Commands;
using Points.Domain.Produto.Events;
using Points.Domain.Produto.Repository;
using Points.Domain.Extrato.Commands;
using Points.Domain.Extrato.Events;
using Points.Domain.Extrato.Repository;
using Points.Domain.Pedido.Commands;
using Points.Domain.Pedido.Events;
using Points.Domain.Pedido.Repository;
using Points.Domain.Usuario.Repository;
using Points.Domain.Saldo.Repository;
using Points.Domain.Validations;
using Points.Infra.Data.Writer;
using Points.Infra.Data.Reader;

namespace Points.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Command
            services.AddScoped<INotificationHandler<RegistrarUsuarioCommand>, UsuarioCommandHandler>();
            services.AddScoped<INotificationHandler<RegistrarEnderecoCommand>, EnderecoCommandHandler>();
            services.AddScoped<INotificationHandler<CadastrarProdutoCommand>, ProdutoCommandHandler>();
            services.AddScoped<INotificationHandler<CadastrarExtratoCommand>, ExtratoCommandHandler>();
            services.AddScoped<INotificationHandler<RealizarPedidoCommand>, PedidoCommandHandler>();

            // Services
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Validators
            services.AddTransient(typeof(PedidoValidator));

            // Bus
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Core
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Events
            services.AddScoped<INotificationHandler<UsuarioRegistradoEvent>, UsuarioEventHandler>();
            services.AddScoped<INotificationHandler<EnderecoRegistradoEvent>, EnderecoEventHandler>();
            services.AddScoped<INotificationHandler<ProdutoCadastradoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<ExtratoCadastradoEvent>, ExtratoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

            // Repository
            services.AddSingleton<IEnderecoQueryRepository, EnderecoQueryRepository>();
            services.AddSingleton<IEnderecoCommandRepository, EnderecoCommandRepository>();
            services.AddSingleton<IProdutoQueryRepository, ProdutoQueryRepository>();
            services.AddSingleton<IProdutoCommandRepository, ProdutoCommandRepository>();
            services.AddSingleton<IExtratoQueryRepository, ExtratoQueryRepository>();
            services.AddSingleton<IExtratoCommandRepository, ExtratoCommandRepository>();
            services.AddSingleton<IPedidoQueryRepository, PedidoQueryRepository>();
            services.AddSingleton<IPedidoCommandRepository, PedidoCommandRepository>();
            services.AddSingleton<IUsuarioQueryRepository, UsuarioQueryRepository>();
            services.AddSingleton<ISaldoQueryRepository, SaldoQueryRepository>();
            services.AddSingleton<ISaldoCommandRepository, SaldoCommandRepository>();

            services.AddScoped<IEventStore, EventStore>();
            services.AddSingleton<IEventStoreRepository, EventStoreRepository>();
        }
    }
}
