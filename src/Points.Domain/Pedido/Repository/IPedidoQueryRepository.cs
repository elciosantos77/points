using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Points.Domain.Pedido.Repository
{
    public interface IPedidoQueryRepository
    {
        Pedido ObterPorId(Guid id);
        Task<List<Pedido>> ObterPorEmail(string email);
        Pedido ObterUltimoPedido(string email);
    }
}
