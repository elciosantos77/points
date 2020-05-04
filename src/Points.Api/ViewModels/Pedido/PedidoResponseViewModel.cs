using Points.Domain.Endereco;
using Points.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Points.Api.ViewModels.Pedido
{
    public class PedidoResponseViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public string StatusEntrega { get; set; }
        public DateTime Data { get; set; }
        public ICollection<PedidoItemResponseViewModel> Itens { get; set; }

    }
}
