using AutoMapper;
using Points.Api.ViewModels.Produto;
using Points.Domain.Core.Notifications;
using Points.Domain.Interfaces;
using Points.Domain.Produto.Commands;
using Points.Domain.Produto.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Points.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : BaseController
    {
        readonly IMapper _mapper;
        private readonly IProdutoQueryRepository _produtoQueryRepository;
        public ProdutosController(
            IMapper mapper,
            IProdutoQueryRepository produtoQueryRepository,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus) : base(notifications, bus)
        {
            _mapper = mapper;
            _produtoQueryRepository = produtoQueryRepository;
        }

        [HttpPost("cadastrar")]
        public IActionResult Post([FromBody] ProdutoViewModel produto)
        {
            var command = _mapper.Map<CadastrarProdutoCommand>(produto);
            _bus.EnviarComando(command);

            var produtoDomain = _produtoQueryRepository.ObterPorNome(produto.Nome);
            return Response(_mapper.Map<ProdutoViewModel>(produtoDomain));
        }

        [HttpGet("listar-todos")]
        public ActionResult<List<ProdutoViewModel>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<ProdutoViewModel>>(_produtoQueryRepository.ObterTodos().Result));
        }
    }

}